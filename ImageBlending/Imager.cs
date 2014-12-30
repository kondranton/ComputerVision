using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVLibrary;

namespace ImageBlending
{
    class Imager
    {
        public byte[,] Blue , Green, Red;
        public byte[][,] bPyr, gPyr, rPyr;
        public void Laplacize()
        {
            bPyr = Convolution.LaplacPyramid(Blue);
            gPyr = Convolution.LaplacPyramid(Green);
            rPyr = Convolution.LaplacPyramid(Red);
        }
        public void Gausize()
        {
            bPyr = Convolution.GausPyramid(Blue);
            gPyr = Convolution.GausPyramid(Green);
            rPyr = Convolution.GausPyramid(Red);
        }
        public void pyrCol()
        {
            Blue = AppTools.pyrCollapse(bPyr, ImageProcessing.iterator-1);
            Green = AppTools.pyrCollapse(gPyr, ImageProcessing.iterator - 1);
            Red = AppTools.pyrCollapse(rPyr, ImageProcessing.iterator - 1);
        }
        public Imager(byte[] sourceImageBytes, int sourceHeight, int sourceWidth)
        {
            Blue = AppTools.Separator(sourceImageBytes, sourceHeight, sourceWidth, 0);
            Green = AppTools.Separator(sourceImageBytes, sourceHeight, sourceWidth, 1);
            Red = AppTools.Separator(sourceImageBytes, sourceHeight, sourceWidth, 2);
        }
    }
}
