using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CVLibrary
{
    public class Convolution
    {
        public static int iterator = 10;
        public static byte[] Sobel(byte[] originalImage, int width, int height)
        {
            byte[] newImage = new byte[originalImage.Length / 4];
            byte[] grayImage = Effects.setGrayscale(originalImage);
            byte[,] grayImage2D = new byte[height, width];
            byte[,] newImage2D = new byte[height, width];


            grayImage2D = AppTools.To2D(grayImage, height, width);

            int curPixX, curPixY;
            for (int i = 1; i < grayImage2D.GetUpperBound(0); i++)
                for (int j = 1; j < grayImage2D.GetUpperBound(1); j++)
                {
                    curPixX = 0; curPixY = 0;
                    for (int h = 0; h < 3; h++)
                        for (int w = 0; w < 3; w++)
                        {
                            int he = i + h - 1;
                            int wi = j + w - 1;
                            curPixX += Kernels.kernelGx[h, w] * grayImage2D[he, wi];
                            curPixY += Kernels.kernelGy[h, w] * grayImage2D[he, wi];

                        }
                    newImage2D[i + 1, j + 1] = AppTools.checkPixel(Math.Sqrt(curPixX * curPixX + curPixY * curPixY));
                }


            newImage = AppTools.To1D(newImage2D);

            return newImage;

        }
        public static byte[,] GaussianBlur(byte[,] grayImage2D)
        {
            byte[,] newImage2D = new byte[grayImage2D.GetUpperBound(0)+1, grayImage2D.GetUpperBound(1)+1];

            float curPix;

            for (int b = 0; b < 30; b++)
            {

                for (int i = 0; i < grayImage2D.GetUpperBound(0) - 1; i++)
                    for (int j = 0; j < grayImage2D.GetUpperBound(1) - 1; j++)
                    {
                        curPix = 0;
                        for (int h = 0; h < 3; h++)
                            for (int w = 0; w < 3; w++)
                            {
                                int he = i + h;
                                int wi = j + w;

                                curPix += Kernels.gausBlur[h, w] * grayImage2D[he, wi];


                            }
                        newImage2D[i + 1, j + 1] = AppTools.checkPixel(curPix);
                    }
                grayImage2D = newImage2D;
            }

            
            

            return newImage2D;
        }
        public static byte[][,] GausPyramid(byte[,] grayImage2D)
        {
            byte[][,] pyramid = new byte[iterator][,];
            pyramid[0] = grayImage2D;

            for (int b = 1; b < iterator; b++)
                pyramid[b] = AppTools.Subsample(GaussianBlur(pyramid[b-1]));
                
            return pyramid;
        }
        public static byte[][,] LaplacPyramid(byte[,] grayImage2D)
        {
            byte[][,] gausPyramid = GausPyramid(grayImage2D);
            byte[][,] pyramid = new byte[iterator][,];
            pyramid[iterator-1] = gausPyramid[iterator-1];

            for (int b = 0; b < iterator-1; b++)
            {
                byte[,] L0 = gausPyramid[b];
                byte[,] L1 = AppTools.Expand(gausPyramid[b + 1]);
                pyramid[b] = new byte[L0.GetUpperBound(0) + 1, L0.GetUpperBound(1) + 1];
                for (int i = 0; i < L0.GetUpperBound(0); i++)
                    for (int j = 0; j < L0.GetUpperBound(1); j++)
                        pyramid[b][i, j] = AppTools.checkPixel(L0[i, j] - L1[i, j]);
            }  

            return pyramid;
        }
        
       
    }
}
