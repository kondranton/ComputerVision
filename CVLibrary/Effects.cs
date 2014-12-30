using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVLibrary
{
    public class Effects
    {
        public static byte[] setGrayscale(byte[] originalImage)
        {
            byte[] newImage = new byte[originalImage.Length / 4];
            for (int i = 0; i < newImage.Length - 2; i++)
                newImage[i] = AppTools.checkPixel(originalImage[i * 4] * 0.114 + originalImage[(i + 1) * 4] * 0.587 + originalImage[(i + 2) * 4] * 0.299);
            /*
             x = 0.299R + 0.587G + 0.114B;
             */
            return newImage;
        }
        //public static float[] setBinary(byte[] originalImage)
        //{
        //    float[] newImage = new float[originalImage.Length / 4];
        //    byte[] grayImage = setGrayscale(originalImage);

        //    for (int i = 0; i < newImage.Length; i++)
        //    {
        //        newImage[i] =(float)grayImage[i]/255f;
        //    }
        //    return newImage;
        //}
        public static byte[] setBinary(byte[] originalImage)
        {
            byte[] newImage = new byte[originalImage.Length / 4];
            byte[] grayImage = setGrayscale(originalImage);

            for (int i = 0; i < newImage.Length; i++)
            {
                newImage[i] = (byte)(grayImage[i] < 155 ? 0 : 1);
            }
            return newImage;
        }
    }
}
