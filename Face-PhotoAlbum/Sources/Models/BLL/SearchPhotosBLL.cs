using Face_PhotoAlbum.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Face_PhotoAlbum.Models {
    public class SearchPhotosBLL {
        const float CONFIRM_MATCH_THRESHOLD = 0.6f;
        const float POSSIBLE_MATCH_THRESHOLD = 0.4f;

        public void Run() {
            ProcessPhoto();
            ProcessFace();
            ProcessComparision();
            ProcessAlbum();
            DeleteInvalidData();
        }
        private void DeleteInvalidData() {
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            foreach (var row in context.T_PhotoInfo.Where(p => p.Status == 99)) {
                context.T_PhotoInfo.Remove(row);
            }
            foreach (var row in context.T_Face.Where(p => p.Status == 99)) {
                context.T_Face.Remove(row);
            }
            foreach (var row in context.T_FaceComparison.Where(p => p.Status == 99)) {
                context.T_FaceComparison.Remove(row);
            }
            context.SaveChanges();
        }

        private void ProcessPhoto() {
            var searchPattern = new Regex(@"$(?<=\.(jpg|png|bmp))", RegexOptions.IgnoreCase);
            var files = Directory.EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos"), "*.*", SearchOption.AllDirectories).Where(p => searchPattern.IsMatch(p));

            IList<T_PhotoInfo> delRows = new List<T_PhotoInfo>();

            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            foreach (T_PhotoInfo p in context.T_PhotoInfo.Where(p => p.Status != 99)) {
                string fileName = Path.Combine(p.FilePath, p.FileName);
                if (!files.Any(name => name.ToLower() == fileName.ToLower())) {
                    p.Status = 99;
                    p.UpdateTime = DateTime.Now;
                }
            }

            int photoNum = 0;
            if (context.T_PhotoInfo.Count() > 0)
                photoNum = context.T_PhotoInfo.Max(p => p.PhotoNum);

            files.ToList().ForEach(file => {
                string md5 = MD5Process.GetMD5HashFromFile(file);
                var rows = context.T_PhotoInfo.Where(p => (p.FilePath + "\\" + p.FileName).ToLower() == file.ToLower() && p.Status != 99);

                int count = rows.Count();
                if (count != 0) {
                    var row = rows.First();
                    if (row.MD5 != md5) {
                        row.MD5 = md5;
                        row.Status = 0;
                        row.UpdateTime = DateTime.Now;
                    }
                }
                else if (count == 0) {
                    var newRow = new T_PhotoInfo();
                    newRow.FileName = Path.GetFileName(file);
                    newRow.FilePath = Path.GetDirectoryName(file);
                    newRow.MD5 = md5;
                    newRow.UpdateTime = DateTime.Now;
                    newRow.Status = 0;
                    newRow.PhotoNum = photoNum + 1;
                    context.T_PhotoInfo.Add(newRow);
                }
            });
            context.SaveChanges();
        }

        private void ProcessFace() {
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            foreach (var row in context.T_PhotoInfo.Where(p => p.Status == 99).Join(context.T_Face.Where(q => q.Status != 99)
                , p => new { PhotoNum = p.PhotoNum }
                , q => new { PhotoNum = q.PhotoNum }
                , (p, q) => new { T_FaceComparison = p, T_Face = q })) {
                row.T_Face.Status = 99;
            }


            int ret = HiwitLib.InitSDK(AppDomain.CurrentDomain.BaseDirectory);

            var photoInfoRows = context.T_PhotoInfo.Where(p => p.Status == 0);
            photoInfoRows.ToList().ForEach(photoInfoRow => {
                string photoFilePath = Path.Combine(photoInfoRow.FilePath, photoInfoRow.FileName);

                byte[] byteImage = File.ReadAllBytes(photoFilePath);
                int faceNumber = 10;
                HiwitLib.HIWITFaceRegion[] faceRegion = new HiwitLib.HIWITFaceRegion[faceNumber];
                for (int i = 0; i < faceNumber; i++)
                    faceRegion[i].faceVertex = new HiwitLib.HIWITPoint[4];
                ret = HiwitLib.FaceDetect(byteImage, faceRegion, ref faceNumber, 0, 0, null);
                if (ret != HiwitLib.HIWIT_ERR_NONE)
                    throw new ArgumentException("FaceDetect fail:" + ret);
                if (faceNumber == 0) {
                    photoInfoRow.Status = 2;
                    photoInfoRow.UpdateTime = DateTime.Now;
                }
                else {
                    BitmapImage sourceImage = new BitmapImage();
                    sourceImage.BeginInit();
                    sourceImage.UriSource = new Uri(photoFilePath);
                    sourceImage.EndInit();
                    int imageWidth = sourceImage.PixelWidth;
                    int imageHeight = sourceImage.PixelHeight;
                    int bitsPerPixel = sourceImage.Format.BitsPerPixel;

                    for (int i = 0; i < faceNumber; i++) {
                        int left = (int)faceRegion[i].faceVertex.Min(p => p.x);
                        int right = (int)faceRegion[i].faceVertex.Max(p => p.x);
                        int top = (int)faceRegion[i].faceVertex.Min(p => p.y);
                        int bottom = (int)faceRegion[i].faceVertex.Max(p => p.y);

                        left = left < 0 ? 0 : (left > imageWidth ? imageWidth : left);
                        right = right < 0 ? 0 : (right > imageWidth ? imageWidth : right);
                        top = top < 0 ? 0 : (top > imageHeight ? imageHeight : top);
                        bottom = bottom < 0 ? 0 : (bottom > imageHeight ? imageHeight : bottom);

                        var newRow = new T_Face();
                        newRow.PhotoNum = photoInfoRow.PhotoNum;
                        newRow.SequenceNum = i;
                        newRow.RectLeft = left;
                        newRow.RectRight = right;
                        newRow.RectTop = top;
                        newRow.RectBottom = bottom;
                        newRow.ConfirmAlbumNum = -1;
                        newRow.PossibleAlbumNum = "-1";

                        //定义切割矩形
                        var cut = new Int32Rect(left, top, right - left, bottom - top);
                        //计算Stride
                        var stride = sourceImage.Format.BitsPerPixel * cut.Width / 8;
                        //声明字节数组
                        byte[] data = new byte[cut.Height * stride];
                        //调用CopyPixels
                        sourceImage.CopyPixels(cut, data, stride, 0);
                        BitmapSource desImage = BitmapSource.Create(cut.Width, cut.Height, 96, 96, sourceImage.Format, null, data, stride);

                        var bitmapEncoder = new PngBitmapEncoder();
                        bitmapEncoder.Frames.Add(BitmapFrame.Create(desImage));
                        string faceFileNmae = Guid.NewGuid().ToString() + ".png";
                        newRow.FaceFileName = faceFileNmae;
                        newRow.FaceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Faces");

                        using (FileStream file = new FileStream(Path.Combine(newRow.FaceFilePath, newRow.FaceFileName), FileMode.Create))
                            bitmapEncoder.Save(file);

                        HiwitLib.HIWITFaceInfo faceInfo = new HiwitLib.HIWITFaceInfo();
                        faceInfo.faceContour = new HiwitLib.HIWITPoint[21];
                        faceInfo.leftEyeBrowContour = new HiwitLib.HIWITPoint[8];
                        faceInfo.leftEyeContour = new HiwitLib.HIWITPoint[8];
                        faceInfo.rightEyeContour = new HiwitLib.HIWITPoint[8];
                        faceInfo.rightEyeBrowContour = new HiwitLib.HIWITPoint[8];
                        faceInfo.mouthContour = new HiwitLib.HIWITPoint[22];
                        faceInfo.noseContour = new HiwitLib.HIWITPoint[13];
                        ret = HiwitLib.FaceAlignmentFromFaceRegion(byteImage, faceRegion[i], ref faceInfo);
                        if (ret == HiwitLib.HIWIT_ERR_NONE) {
                            int size = Marshal.SizeOf(faceInfo);
                            byte[] byteFaceInfo = new byte[size];
                            StructComversion.StructToBytes(faceInfo, byteFaceInfo, 0, size);
                            newRow.Status = 0;
                            newRow.FeatureData = byteFaceInfo;
                        }
                        else {
                            newRow.FeatureData = new byte[0];
                            newRow.Status = 2;
                        }
                        newRow.UpdateTime = DateTime.Now;
                        context.T_Face.Add(newRow);
                    }
                    photoInfoRow.Status = 1;
                    context.SaveChanges();
                }
            });


            HiwitLib.UnInitSDK();
        }

        private void ProcessComparision() {
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            List<T_Face> comparingRows = context.T_Face.Where(p => p.Status == 0).ToList();
            List<T_Face> comparedRows = context.T_Face.Where(p => p.Status == 1).ToList();

            int ret = HiwitLib.InitSDK(AppDomain.CurrentDomain.BaseDirectory);
            comparingRows.ForEach(comparingRow => {
                //string probeFileName = Path.Combine(comparingRow.FaceFilePath, comparingRow.FaceFileName);
                var photoInfo = context.T_PhotoInfo.Where(p => p.PhotoNum == comparingRow.PhotoNum).First();
                string probeFileName = Path.Combine(photoInfo.FilePath, photoInfo.FileName);

                byte[] probeImage = File.ReadAllBytes(probeFileName);
                HiwitLib.HIWITFaceInfo? probeInfo = comparingRow.FeatureData.Length == 0 ? null : (HiwitLib.HIWITFaceInfo?)StructComversion.BytesToStruct(comparingRow.FeatureData, 0, typeof(HiwitLib.HIWITFaceInfo));

                comparedRows.ForEach(comparedRow => {
                    if (comparedRow.PhotoNum != comparingRow.PhotoNum) {
                        //string galleryFileName = Path.Combine(comparedRow.FaceFilePath, comparedRow.FaceFileName);
                        photoInfo = context.T_PhotoInfo.Where(p => p.PhotoNum == comparedRow.PhotoNum).First();
                        string galleryFileName = Path.Combine(photoInfo.FilePath, photoInfo.FileName);
                        byte[] galleryImage = File.ReadAllBytes(galleryFileName);
                        HiwitLib.HIWITFaceInfo? galleryInfo = comparedRow.FeatureData.Length == 0 ? null : (HiwitLib.HIWITFaceInfo?)StructComversion.BytesToStruct(comparedRow.FeatureData, 0, typeof(HiwitLib.HIWITFaceInfo));


                        float score = 0;
                        ret = HiwitLib.FaceVerify(probeImage, galleryImage, ref score, probeInfo, galleryInfo);
                        if (ret != HiwitLib.HIWIT_ERR_NONE)
                            throw new ArgumentException("FaV fail:" + ret);

                        context.T_FaceComparison.Where(p => (p.PhotoNum1 == comparingRow.PhotoNum && p.SequenceNum1 == comparingRow.SequenceNum && p.PhotoNum2 == comparedRow.PhotoNum && p.PhotoNum2 == comparedRow.SequenceNum)
                        || (p.PhotoNum1 == comparedRow.PhotoNum && p.SequenceNum1 == comparedRow.SequenceNum && p.PhotoNum2 == comparingRow.PhotoNum && p.PhotoNum2 == comparingRow.SequenceNum)).ToList()
                            .ForEach(p => context.T_FaceComparison.Remove(p));

                        var newRow = new T_FaceComparison();
                        newRow.PhotoNum1 = comparingRow.PhotoNum;
                        newRow.SequenceNum1 = comparingRow.SequenceNum;
                        newRow.PhotoNum2 = comparedRow.PhotoNum;
                        newRow.SequenceNum2 = comparedRow.SequenceNum;
                        newRow.Score = score;
                        newRow.UpdateTime = DateTime.Now;
                        context.T_FaceComparison.Add(newRow);
                    }
                });
                comparingRow.Status = 1;
                comparedRows.Add(comparingRow);
                context.SaveChanges();
            });
            HiwitLib.UnInitSDK();
        }

        private void ProcessAlbum() {
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            SetConfirmFaceAlbum(context);
            CreateNewFaceAlbum(context);
            SetPossibleAlbumNum(context);
            SetNotMatchData(context);
        }

        private void SetConfirmFaceAlbum(FacePhotoAlbumContext context) {
            var pRows = context.T_FaceComparison.Where(p => p.Score >= CONFIRM_MATCH_THRESHOLD && p.Status == 0).OrderByDescending(p => p.Score);
            var qRows = context.T_Face.Where(face => face.Status == 1 && face.ConfirmAlbumNum != -1);
            var ret = pRows.Join(qRows
              , p => new { PhotoNum = p.PhotoNum1, SequenceNum = p.SequenceNum1 }
              , q => new { PhotoNum = q.PhotoNum, SequenceNum = q.SequenceNum }
              , (p, q) => new { T_FaceComparison = p, T_Face = q, AnotherPhotoNum = p.PhotoNum2, AnotherSequenceNum = p.SequenceNum2 }).Union(pRows.Join(qRows
              , p => new { PhotoNum = p.PhotoNum2, SequenceNum = p.SequenceNum2 }
              , q => new { PhotoNum = q.PhotoNum, SequenceNum = q.SequenceNum }
              , (p, q) => new { T_FaceComparison = p, T_Face = q, AnotherPhotoNum = p.PhotoNum1, AnotherSequenceNum = p.SequenceNum1 }));
            if (ret.Count() == 0)
                return;

            ret.ToList().ForEach(p => {
                var writingRow = context.T_Face.Where(q => q.PhotoNum == p.AnotherPhotoNum && q.SequenceNum == p.AnotherSequenceNum).First();
                writingRow.ConfirmAlbumNum = p.T_Face.ConfirmAlbumNum;
                writingRow.PossibleAlbumNum = "-1";
                writingRow.UpdateTime = DateTime.Now;
                p.T_FaceComparison.Status = 1;
                p.T_FaceComparison.UpdateTime = DateTime.Now;
            });
            context.SaveChanges();

            SetConfirmFaceAlbum(context);
        }
        private void CreateNewFaceAlbum(FacePhotoAlbumContext context) {
            var pRows = context.T_FaceComparison.Where(p => p.Score >= CONFIRM_MATCH_THRESHOLD && p.Status == 0).OrderByDescending(p => p.Score);
            var qRows = context.T_Face.Where(face => face.Status == 1 && face.ConfirmAlbumNum == -1);
            var ret = pRows.Join(qRows
              , p => new { PhotoNum = p.PhotoNum1, SequenceNum = p.SequenceNum1 }
              , q => new { PhotoNum = q.PhotoNum, SequenceNum = q.SequenceNum }
              , (p, q) => new { T_FaceComparison = p, T_Face = q, AnotherPhotoNum = p.PhotoNum2, AnotherSequenceNum = p.SequenceNum2 }).Union(pRows.Join(qRows
              , p => new { PhotoNum = p.PhotoNum2, SequenceNum = p.SequenceNum2 }
              , q => new { PhotoNum = q.PhotoNum, SequenceNum = q.SequenceNum }
              , (p, q) => new { T_FaceComparison = p, T_Face = q, AnotherPhotoNum = p.PhotoNum1, AnotherSequenceNum = p.SequenceNum1 }));
            if (ret.Count() == 0)
                return;
            var row = ret.First();
            string AlbumName = string.Empty;
            for (int i = 1; ; i++) {
                AlbumName = "自定义" + i.ToString();
                if (context.T_AlbumLabel.Where(p => p.AlbumLabel == AlbumName).Count() == 0)
                    break;
            }
            T_AlbumLabel albumLabelRow = new T_AlbumLabel();
            albumLabelRow.AlbumNum = context.T_AlbumLabel.Max(p => p.AlbumNum) + 1;
            albumLabelRow.AlbumLabel = AlbumName;
            albumLabelRow.UpdateTime = DateTime.Now;
            albumLabelRow.CoverImage = File.ReadAllBytes(Path.Combine(row.T_Face.FaceFilePath, row.T_Face.FaceFileName));
            context.T_AlbumLabel.Add(albumLabelRow);

            var writingRow = context.T_Face.Where(q => q.PhotoNum == row.T_Face.PhotoNum && q.SequenceNum == row.T_Face.SequenceNum).First();
            writingRow.ConfirmAlbumNum = albumLabelRow.AlbumNum;
            writingRow.PossibleAlbumNum = "-1";
            writingRow.UpdateTime = DateTime.Now;

            context.SaveChanges();

            SetConfirmFaceAlbum(context);
            CreateNewFaceAlbum(context);
        }
        private void SetPossibleAlbumNum(FacePhotoAlbumContext context) {
            var pRows = context.T_FaceComparison.Where(p => p.Score < CONFIRM_MATCH_THRESHOLD && p.Score >= POSSIBLE_MATCH_THRESHOLD && p.Status == 0).OrderByDescending(p => p.Score);
            var qRows = context.T_Face.Where(face => face.Status == 1 && face.ConfirmAlbumNum != -1);
            var ret = pRows.Join(qRows
              , p => new { PhotoNum = p.PhotoNum1, SequenceNum = p.SequenceNum1 }
              , q => new { PhotoNum = q.PhotoNum, SequenceNum = q.SequenceNum }
              , (p, q) => new { T_FaceComparison = p, T_Face = q, AnotherPhotoNum = p.PhotoNum2, AnotherSequenceNum = p.SequenceNum2 }).Union(pRows.Join(qRows
              , p => new { PhotoNum = p.PhotoNum2, SequenceNum = p.SequenceNum2 }
              , q => new { PhotoNum = q.PhotoNum, SequenceNum = q.SequenceNum }
              , (p, q) => new { T_FaceComparison = p, T_Face = q, AnotherPhotoNum = p.PhotoNum1, AnotherSequenceNum = p.SequenceNum1 }));
            if (ret.Count() == 0)
                return;

            foreach (var p in ret) {
                var writingRow = context.T_Face.Where(q => q.PhotoNum == p.AnotherPhotoNum && q.SequenceNum == p.AnotherSequenceNum).First();
                if (writingRow.ConfirmAlbumNum != -1)
                    continue;
                if (writingRow.PossibleAlbumNum == "-1") {
                    writingRow.PossibleAlbumNum = p.T_Face.ConfirmAlbumNum.ToString();
                }
                else {
                    if (!writingRow.PossibleAlbumNum.Split(',').Contains(p.T_Face.ConfirmAlbumNum.ToString())) {
                        writingRow.PossibleAlbumNum += "," + p.T_Face.ConfirmAlbumNum.ToString();
                    }
                }
                writingRow.UpdateTime = DateTime.Now;
                p.T_FaceComparison.Status = 1;
                p.T_FaceComparison.UpdateTime = DateTime.Now;
                context.SaveChanges();
            }
        }

        private void SetNotMatchData(FacePhotoAlbumContext context) {
            var pRows = context.T_FaceComparison.Where(p => p.Score < POSSIBLE_MATCH_THRESHOLD);
            foreach (var p in pRows) {
                p.Status = 1;
                p.UpdateTime = DateTime.Now;
            }
            context.SaveChanges();
        }
    }
}
