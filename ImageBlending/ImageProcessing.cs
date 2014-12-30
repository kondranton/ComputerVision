using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVLibrary;

namespace ImageBlending
{
   
    class ImageProcessing
    {
        public static int iterator = 10;
        public byte[] Blending(byte[] sourceImageBytes, int sourceWidth, int sourceHeight,
                               byte[] destImageBytes, int destWidth, int destHeight,
                               byte[] maskImageBytes, int maskWidth, int maskHeight
                                                   )
        {
            Imager source = new Imager(sourceImageBytes, sourceHeight, sourceWidth);
            Imager dest = new Imager(destImageBytes, destHeight, destWidth);
            Imager mask = new Imager(maskImageBytes, maskHeight, maskWidth);
            Imager target = new Imager(destImageBytes, destHeight, destWidth);

            source.Laplacize();
            dest.Laplacize();
            target.Gausize();
            mask.Gausize();

            for (int b = 0; b < iterator; b++)
            {
                byte[] fullMask = AppTools.colorSplicer(mask.bPyr[b], mask.gPyr[b], mask.rPyr[b]);
                byte[,] curMask = AppTools.To2D(Effects.setBinary(fullMask), mask.bPyr[b].GetUpperBound(0) + 1, mask.bPyr[b].GetUpperBound(1) + 1);
                //float[,] curMask = AppTools.To2D(Effects.setBinary(fullMask), mask.bPyr[b].GetUpperBound(0) + 1, mask.bPyr[b].GetUpperBound(1) + 1);

                for (int i = 0; i < source.bPyr[b].GetUpperBound(0); i++)
                    for (int j = 0; j < source.bPyr[b].GetUpperBound(1); j++)
                    {
                        target.bPyr[b][i, j] = AppTools.checkPixel(curMask[i, j] * source.bPyr[b][i, j] + (1 - curMask[i, j]) * dest.bPyr[b][i, j]);
                        target.gPyr[b][i, j] = AppTools.checkPixel(curMask[i, j] * source.gPyr[b][i, j] + (1 - curMask[i, j]) * dest.gPyr[b][i, j]);
                        target.rPyr[b][i, j] = AppTools.checkPixel(curMask[i, j] * source.rPyr[b][i, j] + (1 - curMask[i, j]) * dest.rPyr[b][i, j]);
                    }

            }

            target.pyrCol();

          

            byte[] fullImage = AppTools.colorSplicer(target.Blue, target.Green, target.Red);

            //byte[] resultImage = new byte[fullImage.Length];
            //for (int i = 0; i < fullImage.Length; i++ )
            //    resultImage[i] = AppTools.checkPixel(fullImage[i]+ destImageBytes[i]);

                return fullImage;
            

            

        }
        
    }
}
