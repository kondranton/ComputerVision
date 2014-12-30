using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageBlending
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage sourceImage, destImage, maskImage;
        private byte[] sourceImageBytes, destImageBytes, maskImageBytes;

        public MainWindow()
        {
            InitializeComponent();
        }

        // open file from the finder
        private void openSourceButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage(openFileDialog.FileName, ref sourcePanel, ref sourceImage, ref sourceImageBytes);
            }
        }
        private void openDestination_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage(openFileDialog.FileName, ref destinationPanel, ref destImage, ref destImageBytes);
            }
        }
        private void openMaskButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage(openFileDialog.FileName, ref maskPanel, ref maskImage, ref maskImageBytes);
            }
        }

        // show image on the window
        private void showImage(string filename, ref Image originalPanel, ref BitmapImage originalImage, ref byte[] originalImageBytes)
        {
            originalImage = ImageConvertor.FilenameToImage(filename);
            originalImageBytes = ImageConvertor.ImageToByteArray(filename);
            originalPanel.Source = originalImage;
        }

        // click on processing buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageProcessing process = new ImageProcessing();
            byte[] processedImageBytes;

            processedImageBytes = process.Blending(sourceImageBytes, sourceImage.PixelWidth, sourceImage.PixelHeight,
                                                   destImageBytes,   destImage.PixelWidth,   destImage.PixelHeight,
                                                   maskImageBytes,   maskImage.PixelWidth,   maskImage.PixelHeight
                                                   );
            processedPanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, destImage.PixelWidth, destImage.PixelHeight, 4);
                   
            

        }

        

       

       
    }
}
