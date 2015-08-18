using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Face_PhotoAlbum.Sources.Common {
    public class StructComversion {
        //将Byte转换为结构体类型
        public static void StructToBytes(object structObj, byte[] outputBytes, int startIndex, int length)
        {
            IntPtr structPtr = Marshal.AllocHGlobal(length);
            try
            {
                //将结构体拷到分配好的内存空间
                Marshal.StructureToPtr(structObj, structPtr, false);
                //从内存空间拷贝到byte 数组
                Marshal.Copy(structPtr, outputBytes, startIndex, length);
            }
            catch
            {
                throw;
            }
            finally
            {
                //释放内存空间
                Marshal.FreeHGlobal(structPtr);
            }
        }

        //将Byte转换为结构体类型
        public static object BytesToStruct(byte[] bytes, int startIndex, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size + startIndex > bytes.Length)
            {
                return null;
            }
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            try
            {
                //将byte数组拷贝到分配好的内存空间
                Marshal.Copy(bytes, startIndex, structPtr, size);
                //将内存空间转换为目标结构体
                object obj = Marshal.PtrToStructure(structPtr, type);
                return obj;
            }
            catch
            {
                throw;
            }
            finally
            {
                //释放内存空间
                Marshal.FreeHGlobal(structPtr);
            }
        }
        public static object StreamToStruct(Stream inputStream, Type type)
        {
            try
            {
                int size = Marshal.SizeOf(type);
                if (size + inputStream.Position > inputStream.Length)
                {
                    return null;
                }
                //分配结构体内存空间
                IntPtr structPtr = Marshal.AllocHGlobal(size);
                //将byte数组拷贝到分配好的内存空间
                int readLength = 8;
                byte[] tempBuffer = new byte[readLength];
                int pos = 0;
                do
                {
                    int length = ((pos + readLength <= size) ? readLength : (size - pos));
                    length = inputStream.Read(tempBuffer, 0, length);
                    Marshal.Copy(tempBuffer, 0, structPtr + pos, length);
                    pos += length;
                } while (pos < size);
                //将内存空间转换为目标结构体
                object obj = Marshal.PtrToStructure(structPtr, type);
                //释放内存空间
                Marshal.FreeHGlobal(structPtr);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public static void StructToStream(object structObj, Stream outputStream)
        {
            int length = Marshal.SizeOf(structObj);
            IntPtr structPtr = Marshal.AllocHGlobal(length);
            try
            {
                byte[] bytes = new byte[length];
                //将结构体拷到分配好的内存空间
                Marshal.StructureToPtr(structObj, structPtr, false);
                //从内存空间拷贝到byte 数组
                Marshal.Copy(structPtr, bytes, 0, length);

                outputStream.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                //释放内存空间
                Marshal.FreeHGlobal(structPtr);
            }
        }
    }
}
