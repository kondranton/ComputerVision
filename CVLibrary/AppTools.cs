using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVLibrary
{
    public class AppTools
    {
        //Array Convertors
        // 1D -> 2D
        public static byte[,] To2D(byte[] Array1D, int height, int width)
        {
            byte[,] Array2D = new byte[height, width];
            int index = 0;
            for (int i = 0; i < Array2D.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < Array2D.GetUpperBound(1) + 1; j++)
                    Array2D[i, j] = Array1D[index++];
            return Array2D;
        }
        public static float[,] To2D(float[] Array1D, int height, int width)
        {
            float[,] Array2D = new float[height, width];
            int index = 0;
            for (int i = 0; i < Array2D.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < Array2D.GetUpperBound(1) + 1; j++)
                    Array2D[i, j] = Array1D[index++];
            return Array2D;
        }
        
        // 2D -> 1D
        public static byte[] To1D(byte[,] Array2D)
        {
            byte[] Array1D = new byte[Array2D.Length];
            int index = 0;
            for (int i = 0; i < Array2D.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < Array2D.GetUpperBound(1) + 1; j++)
                    Array1D[index++] = Array2D[i, j];
            return Array1D;
        }
        public static byte[,] Separator(byte[] Array1D, int height, int width, int colNum)
        {
            byte[,] resArray =  new byte[height,width];
            int index = colNum;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++ )
                {
                    resArray[i, j] = Array1D[index];
                    index += 4;
                    }
                     
                    return resArray;
        }
        public static byte[] colorSplicer(byte[,]Blue, byte[,]Green, byte[,]Red)
        {
            byte[] fullImage = new byte[Blue.Length * 4];
            int index = 0;
            for(int i = 0; i < Blue.GetUpperBound(0)+1; i++)
                for(int j = 0 ; j < Blue.GetUpperBound(1)+1; j++){
                    fullImage[index++] = Blue[i, j];
                    fullImage[index++] = Green[i, j];
                    fullImage[index++] = Red[i, j];
                    ++index;
                }
            return fullImage;
        }
        public static int[] gradCreator(int N,byte[,] channel, byte [,] mask)
        {
            int[] gradMatrix = new int[N];
            int index = 0;
            for (int i = 0; i < channel.GetUpperBound(0); i++)
                for (int j = 0; j < channel.GetUpperBound(1); j++)
                    if (mask[i, j] == 255) gradMatrix[index++] = channel[i, j] * 4 - channel[i-1, j] - channel[i, j-1] - channel[i+1, j] - channel[i, j+1];
            return gradMatrix;
        }
        public static int[] gradCreator(int N, byte[,] channel)
        {
            int[] gradMatrix = new int[N];

            int index = 0;
            for (int i = 0; i < channel.GetUpperBound(0); i++)
                for (int j = 0; j < channel.GetUpperBound(1); j++)
                    if (j == channel.GetUpperBound(1) - 1 || i == channel.GetUpperBound(0) - 1||j == 0 || i == 0)
                    gradMatrix[index++] = channel[i, j];
                    else
                    gradMatrix[index++] = channel[i, j] * 4 - channel[i - 1, j] - channel[i, j - 1] - channel[i + 1, j] - channel[i, j + 1];

            return gradMatrix;
        }
        public static int[,] sparseMatrixCreator(int N)
        {
            int[,] sparseMat = new int[N,N];

            int index = 0;
            for (int i = 0; i < N; i++)
            {
                sparseMat[i, index] = 4;
                sparseMat[i, index + 1] = sparseMat[i, index + 2] = sparseMat[i, index + 3] = sparseMat[i, index + 4] = -1;
                index++;
            }
            
            return sparseMat;
        }
        public static int countMask(byte[,] mask)
        {
            int counter =0;
            for (int i = 0; i < mask.GetUpperBound(0); i++)
                for (int j = 0; j < mask.GetUpperBound(1); j++)
                    if (mask[i, j] == 255) counter++;
            return counter;
        }
        public static byte[,] Subsample(byte[,] image2X)
        {
            byte coef1 = (byte)(image2X.GetUpperBound(0) % 2 == 0 ? 0 : 1);
            byte coef2 = (byte)(image2X.GetUpperBound(1) % 2 == 0 ? 0 : 1);
            byte[,] result = new byte[image2X.GetUpperBound(0)/2+coef1, image2X.GetUpperBound(1)/2+coef2];

            for (int i = 0; i < result.GetUpperBound(0)+1; i++)
                for (int j = 0; j < result.GetUpperBound(1)+1; j++)
                     result[i,j] = image2X[i*2,j*2];

            return result;
        }
        public static byte[,] Expand(byte[,] image2X)
        {
            byte[,] result = new byte[(image2X.GetUpperBound(0)+1) *2, (image2X.GetUpperBound(1)+1) * 2];

            for (int i = 0; i < result.GetUpperBound(0); i +=2)
                for (int j = 0; j < result.GetUpperBound(1); j +=2)
                {
                    result[i, j] = result[i+1, j] = result[i, j+1] = result[i+1, j+1] = image2X[i / 2, j / 2];
                }

            return result;
        }
        public static byte[,] pyrCollapse(byte[][,] pyramid, int ind)
        {
            if (ind == 0) return pyramid[0];
            else
            {
                pyramid[ind - 1] = Collapse(pyramid[ind - 1], pyramid[ind]);
                return(pyrCollapse(pyramid,--ind));
            }

        }

        private static byte[,] Collapse(byte[,] im1, byte[,] im2)
        {
            byte[,] eIm2 = Expand(im2);
            byte[,] result = new byte[im1.GetUpperBound(0)+1, im1.GetUpperBound(1)+1];
            for (int i = 0; i < result.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < result.GetUpperBound(1) + 1; j++)
                    if (eIm2.GetUpperBound(0)+1 < i && eIm2.GetUpperBound(1)+1 < j)
                        result[i, j] = checkPixel(eIm2[i, j] + im1[i, j]);
                    else result[i, j] = im1[i, j];
            return result;
            
        }
        
        //Controls pixel-value limits
        public static byte checkPixel(double pix)
        {
            return (byte)(pix > 0 ? (pix < 255 ? pix : 255) : 0);
        }
    }
}
