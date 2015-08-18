using System;
using System.Runtime.InteropServices;

namespace Face_PhotoAlbum.Sources.Common {
    public class HiwitLib {
        const string APPIP = "d3527692bc91420089c8caf23c8f68f3";
        const string APPKEY = "MDE5MTE4MzE=";

        public static int HIWIT_ERR_NONE = 0;	    //没有错误
        public static int HIWIT_ERR_AUTH = -1;		//在线验证错误
        public static int HIWIT_ERR_LICE = -2;		//授权验证错误public static intt t
        public static int HIWIT_ERR_ILLEGAL = -4;		//非法调用
        public static int HIWIT_ERR_CFG = -5;		//配置文件未找到
        public static int HIWIT_ERR_UNINIT = -7;		//SDK未初始化public static intt t
        public static int HIWIT_ERR_NET = -10;		//网络错误	
        public static int HIWIT_ERR_SERVER = -11;		//服务端错误public static intt t
        public static int HIWIT_ERR_WS = -12;		//本地cfg文件地址配置错误或服务器无法连接public static intt t
        public static int HIWIT_ERR_ANGLE = -17;		//posangle 存在非法值，三个参数范围为yaw [-30, 30]度， pitch [-90, 90]度，roll[-15,15]度
        public static int HIWIT_ERR_PIC_TOO_LARGE = -18;		//图片超过3M
        public static int HIWIT_ERR_PIC_FAIL = -19;		//图像存在错误或格式不支持，（目前支持jpg、PNG、bmp)
        public static int HIWIT_ERR_PIC_TOO_SMALL = -21;		//虹膜图像最小必须大于240*180，人脸识别两眼之间像素最小值限制（20），指纹图片像素大于500public static intt t
        public static int HIWIT_ERR_FACEVERTEX = -24;		//faceVertex输入四点非正方形或超出图像区域
        public static int HIWIT_ERR_FACEINFOSIZE = -25;		//HIWITFaceAlignment中faceInfoSize只能为1,2,3 
        public static int HIWIT_ERR_FACESIZE = -26;		//HIWITFaceDetect中minFaceSize、maxFaceSize小于0或者超过图像的长或宽 
        public static int HIWIT_ERR_DECTECT_ROI = -27;		//HIWITFaceDetect中detectROI超出图像范围或者小于minFaceSize
        public static int HIWIT_ERR_TOO_FREQUENT = -28;		//API调用过于频繁

        [StructLayout(LayoutKind.Sequential)]
        public struct HIWITFaceRect {
            public int left;
            public int top;
            public int right;
            public int bottom;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct HIWITPoseAngle {
            public int yaw;            ///< yaw angle 左右摇头角度,取值范围[-90,90]. 左侧面人脸-90度，右侧面人脸90度,正面人脸0度
            public int pitch;          ///< pitch angle 俯仰角度，取值范围[-90,90]. 抬头角度为正，低头角度为负值，正面人脸0度
            public int roll;           ///< roll angle 左右偏转角度,取值范围[-180,180]. 头向左侧歪为负值，向右为正值
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct HIWITPoint {
            // [MarshalAs(UnmanagedType.R4)]
            public float x;            ///< 特征点在原始图像中的x坐标
          //  [MarshalAs(UnmanagedType.R4)]
            public float y;            ///< 特征点在原始图像中的y坐标    
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct HIWITFaceRegion {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public HIWITPoint[] faceVertex;
            public HIWITPoseAngle posAngle;    ///< 人脸的各个姿态角度
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct HIWITFaceInfo {
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint leftEyeCenter;           ///< 左眼中心点在原始图像中的坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint leftEyeTail;             ///< 左眼左顶角坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint leftEyeInner;            ///< 左眼右顶角坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint rightEyeCenter;          ///< 右眼中心点在原始图像中的坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint rightEyeTail;            ///< 右眼左顶角坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint rightEyeInner;           ///< 右眼右顶角坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint leftNose;                ///< 鼻脚左翼坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint rightNose;               ///< 鼻脚右翼坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint topNose;                 ///< 人中与鼻子的交点
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint leftMouth;               ///< 嘴角左翼坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint rightMouth;              ///< 嘴角右翼坐标
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint mouthCenter;             ///< 嘴巴中心点
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint topCenterUpperLip;       ///< 嘴巴对称线上，上嘴唇中心最高点
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint bottomCenterUpperLip;    ///< 嘴巴对称线上，上嘴唇中心最低点
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint topCenterLowerLip;       ///< 嘴巴对称线上，下嘴唇中心最高点
            [MarshalAs(UnmanagedType.Struct)]
            public HIWITPoint bottomCenterLowerLip;    ///< 嘴巴对称线上，下嘴唇中心最低点
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
            public HIWITPoint[] leftEyeContour;       ///< 左眼轮廓点
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
            public HIWITPoint[] rightEyeContour;      ///< 右眼轮廓点
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
            public HIWITPoint[] leftEyeBrowContour;  ///< 左眼眉毛轮廓点
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
            public HIWITPoint[] rightEyeBrowContour;  ///< 右眼眉毛轮廓点
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 13)]
            public HIWITPoint[] noseContour;         ///< 鼻子轮廓
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 22)]
            public HIWITPoint[] mouthContour;        ///< 嘴巴轮廓点
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 21)]
            public HIWITPoint[] faceContour;         ///< 脸颊轮廓点
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct HIWITDetailFaceInfo {
            public HIWITFaceRegion faceRegion;        ///< 人脸框位置信息，及姿态角度
            public HIWITFaceInfo faceInfo;            ///< 关键特征点信息
            public int glassesType;               ///< 眼镜类型
            public float qualityScore;              ///< 质量分数  
        };

        public static int InitSDK(string libPath)
        {
            return HIWITSDKInit(libPath);
        }

        public static int UnInitSDK()
        {
            return HIWITSDKUnInit();
        }

        public static int FingerVerify(byte[] probeData, byte[] galleryData, ref float score)
        {
            if (probeData == null || galleryData == null) return HIWIT_ERR_ILLEGAL;
            return HIWITFingerVerifyTwoImage(APPIP, APPKEY, probeData, probeData.Length, galleryData, galleryData.Length, ref score);
        }

        public static int IrisVerify(byte[] probeData, byte[] galleryData, ref float score)
        {
            if (probeData == null || galleryData == null) return HIWIT_ERR_ILLEGAL;
            return HIWITIrisVerifyTwoImage(APPIP, APPKEY, probeData, probeData.Length, galleryData, galleryData.Length, ref score);
        }

        public static int FaceVerify(byte[] probeData, byte[] galleryData, ref float score, HIWITFaceInfo? probeInfo = null, HIWITFaceInfo? galleryInfo = null)
        {
            if (probeData == null || galleryData == null) return HIWIT_ERR_ILLEGAL;
            int size = Marshal.SizeOf(typeof(HiwitLib.HIWITFaceInfo));

            IntPtr probePtr = IntPtr.Zero;
            IntPtr galleryPtr = IntPtr.Zero;

            try
            {
                if (probeInfo != null)
                {
                    probePtr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(probeInfo, probePtr, false);
                }

                if (galleryInfo != null)
                {
                    galleryPtr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(galleryInfo, galleryPtr, false);
                }

                return HIWITFaceVerifyTwoImage(APPIP, APPKEY, probeData, probeData.Length, probePtr, galleryData, galleryData.Length, galleryPtr, ref score);
            }
            finally
            {
                Marshal.FreeHGlobal(probePtr);
                Marshal.FreeHGlobal(galleryPtr);
            }

        }

        public static int FaceDetect(byte[] image, HIWITFaceRegion[] faceRect, ref int faceNumber, int minFaceSize, int maxFaceSize, HIWITFaceRect? detectROI = null)
        {
            if (image == null || faceRect == null) return HIWIT_ERR_ILLEGAL;
            IntPtr roiPtr = IntPtr.Zero;
            IntPtr faceRectPtr = IntPtr.Zero;
            try
            {
                if (detectROI != null)
                {
                    roiPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITFaceRect)));
                    Marshal.StructureToPtr(detectROI, roiPtr, false);
                }
                faceRectPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITFaceRegion)) * faceRect.Length);

                int r = HIWITFaceDetect(APPIP, APPKEY, image, image.Length, faceRectPtr, faceRect.Length, ref faceNumber, minFaceSize, maxFaceSize, roiPtr);
                if (r == HIWIT_ERR_NONE && faceNumber != 0)
                {
                    for (int i = 0; i < faceNumber; i++)
                    {
                        IntPtr tmPtr = new IntPtr(faceRectPtr.ToInt64() + Marshal.SizeOf(typeof(HiwitLib.HIWITFaceRegion)) * i);
                        faceRect[i] = (HiwitLib.HIWITFaceRegion)Marshal.PtrToStructure(tmPtr, typeof(HiwitLib.HIWITFaceRegion));
                    }
                }
                return r;
            }
            finally
            {
                Marshal.FreeHGlobal(roiPtr);
                Marshal.FreeHGlobal(faceRectPtr);
            }
        }

        public static int FaceQuality(byte[] image, HIWITPoint eyePointLeft, HIWITPoint eyePointRight, ref float qualitySocre)
        {
            if (image == null) return HIWIT_ERR_ILLEGAL;
            IntPtr epl = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITPoint)));
            Marshal.StructureToPtr(eyePointLeft, epl, false);
            IntPtr epr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITPoint)));
            Marshal.StructureToPtr(eyePointRight, epr, false);
            try
            {
                return HIWITFaceQuality(APPIP, APPKEY, image, image.Length, epl, epr, ref qualitySocre);
            }
            finally
            {
                Marshal.FreeHGlobal(epl);
                Marshal.FreeHGlobal(epr);
            }

        }

        public static int FaceAlignmentFromFaceRegion(byte[] image, HIWITFaceRegion faceRegion, ref HIWITFaceInfo faceInfo)
        {
            if (image == null) return HIWIT_ERR_ILLEGAL;
            IntPtr regionPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITFaceRegion)));
            Marshal.StructureToPtr(faceRegion, regionPtr, false);
            IntPtr infoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITFaceInfo)));
            try
            {
                int r = HIWITFaceAlignmentFromFaceRegion(APPIP, APPKEY, image, image.Length, regionPtr, infoPtr);
                if (r == HIWIT_ERR_NONE) faceInfo = (HIWITFaceInfo)Marshal.PtrToStructure(infoPtr, typeof(HIWITFaceInfo));
                return r;
            }
            finally
            {
                Marshal.FreeHGlobal(regionPtr);
                Marshal.FreeHGlobal(infoPtr);
            }
        }

        public static int FaceAlignment(byte[] image, ref HIWITDetailFaceInfo[] faceInfo, ref int faceNumber, int minFaceSize, int maxFaceSize, HIWITFaceRect? detectROI = null)
        {
            if (image == null || faceInfo == null) return HIWIT_ERR_ILLEGAL;
            IntPtr roiPtr = IntPtr.Zero;
            IntPtr detailPtr = IntPtr.Zero;
            try
            {
                if (detectROI != null)
                {
                    roiPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITFaceRect)));
                    Marshal.StructureToPtr(detectROI, roiPtr, false);
                }
                detailPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HIWITDetailFaceInfo)) * faceInfo.Length);
                int r = HIWITFaceAlignment(APPIP, APPKEY, image, image.Length, detailPtr, faceInfo.Length, ref faceNumber, minFaceSize, maxFaceSize, roiPtr);
                if (r == HIWIT_ERR_NONE && faceNumber != 0)
                {
                    for (int i = 0; i < faceNumber; i++)
                    {
                        IntPtr tmPtr = new IntPtr(detailPtr.ToInt64() + Marshal.SizeOf(typeof(HiwitLib.HIWITDetailFaceInfo)) * i);
                        faceInfo[i] = (HiwitLib.HIWITDetailFaceInfo)Marshal.PtrToStructure(tmPtr, typeof(HiwitLib.HIWITDetailFaceInfo));
                    }
                }
                return r;

            }
            finally
            {
                Marshal.FreeHGlobal(roiPtr);
                Marshal.FreeHGlobal(detailPtr);
            }

        }

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITSDKInit(string libPath);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITSDKUnInit();

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITFingerVerifyTwoImage(string appId, string appKey, byte[] probeData, int probeSize, byte[] galleryData, int gallerySize, ref float verifyScore);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITIrisVerifyTwoImage(string appId, string appKey, byte[] probeData, int probeSize, byte[] galleryData, int gallerySize, ref float verifyScore);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITFaceVerifyTwoImage(
            string appId,
            string appKey,
            byte[] probeData,
            int probeSize,
            IntPtr probeInfo,
            byte[] galleryData,
            int gallerySize,
            IntPtr galleryInfo,
            ref float verifyScore);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITFaceDetect(
                              string appId,
                              string appKey,
                              byte[] imageBuffer,
                              int imageSize,
                              IntPtr faceRect,
                              int faceRectSize,
                              ref int faceNumber,
                              int minFaceSize,
                              int maxFaceSize,
                              IntPtr detectROI);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITFaceQuality(
                              string appId,
                              string appKey,
                              byte[] imageBuffer,
                              int imageSize,
                              IntPtr eyePointLeft,
                              IntPtr eyePointRight,
                              ref float qualityScore);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITFaceAlignmentFromFaceRegion(
                                 string appId,
                                 string appKey,
                                 byte[] imageBuffer,
                                 int imageSize,
                                 IntPtr faceRect,
                                 IntPtr faceInfo);

        [DllImport("hiwitsdk.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HIWITFaceAlignment(
                                 string appId,
                                 string appKey,
                                 byte[] imageBuffer,
                                 int imageSize,
                                 IntPtr faceInfo,
                                 int faceInfoSize,
                                 ref int faceNumber,
                                 int minFaceSize,
                                 int maxFaceSize,
                                 IntPtr detectROI);

    }
}
