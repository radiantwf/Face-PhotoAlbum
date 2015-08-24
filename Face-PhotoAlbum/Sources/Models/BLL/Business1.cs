using Face_PhotoAlbum.Common;
using Face_PhotoAlbum.Model;
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

namespace Face_PhotoAlbum.Business {
    public class Business1 {
        public void Run() {
            ProcessPhoto();
            ProcessFace();
            ProcessComparision();
            ProcessAlbum();
        }

        public void ProcessPhoto() {
            var searchPattern = new Regex(@"$(?<=\.(jpg|png|bmp))", RegexOptions.IgnoreCase);
            var files = Directory.EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos"), "*.*", SearchOption.AllDirectories).Where(p => searchPattern.IsMatch(p));

            IList<T_PhotoInfo> delRows = new List<T_PhotoInfo>();

            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            context.T_PhotoInfo.ToList().ForEach(p => {
                string fileName = Path.Combine(p.FilePath, p.FileName);
                if (!files.Any(name => name.ToLower() == fileName.ToLower())) {
                    p.Status = 99;
                    p.UpdateTime = DateTime.Now;
                }
            });

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

        public void ProcessFace() {
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            var delPhotoInfoRows = context.T_PhotoInfo.Where(p => p.Status == 99);
            delPhotoInfoRows.ToList()
                .ForEach(photoInfoRow => context.T_Face.Where(faceRow => faceRow.PhotoNum == photoInfoRow.PhotoNum && faceRow.Status != 99).ToList()
                .ForEach(faceRow => faceRow.Status = 99));

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
                if (ret != HiwitLib.HIWIT_ERR_NONE) throw new ArgumentException("FaceDetect fail:" + ret);
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

        public void ProcessComparision() {
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
                        if (ret != HiwitLib.HIWIT_ERR_NONE) throw new ArgumentException("FaV fail:" + ret);

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

        public void ProcessAlbum() {
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            context.T_FaceComparison.Where(row => row.Score >= 0.45 && row.Status == 0).OrderByDescending(row => row.Score)
                .ToList().ForEach(row => {
                    int PhotoNum1 = row.PhotoNum1;
                    int SequenceNum1 = row.SequenceNum1;
                    int PhotoNum2 = row.PhotoNum2;
                    int SequenceNum2 = row.SequenceNum2;

                    var face = context.T_Face.Where(p => p.PhotoNum == PhotoNum2 && p.SequenceNum == SequenceNum2).First();
                });
            return;
        }
    }
}
