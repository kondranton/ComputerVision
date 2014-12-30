using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVLibrary
{
    public class Kernels
    {
        
        
        
        
        // Sobel
        public static int[,] kernelGx = 
        {
            {-1, 0, 1},
            {-2, 0, 2},
            {-1, 0, 1}
        };
        public static int[,] kernelGy = 
        {
            {-1,-2,-1},
            { 0, 0, 0},
            { 1, 2, 1}
        };
        
        //Gaussian Blur
        public static float[,] gausBlur = 
        {
            {1/16f,1/8f,1/16f},
            {1/8f, 1/4f, 1/8f},
            {1/16f,1/8f,1/16f} 
        };
        public static float[,] laplacBlur = 
        {
            {0,  1, 0},
            {1, -4, 1},
            {0,  1, 0}
        };
        public static float[,] laplacBlur2 = 
        {
            {1,  1, 1},
            {1, -8, 1},
            {1,  1, 1}
        };
    }
}
