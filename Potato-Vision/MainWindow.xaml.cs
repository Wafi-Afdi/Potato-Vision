using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
using ImageProcessing;
using Microsoft.Win32;
using Visual_Object;

namespace Potato_Vision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage? ImageBrowsed;
        private string? filePath;


        private ProcessImage _processImage;

        private TargetObject? Target_Visual;
        private Visual_Object.SaveData? saveData = new SaveData();

        // Nilai Range sementara
        int[] accepted = { 28, 30, 32, 36, 255, 255 };
        int[] rejected1 = { 171, 74, 56, 255, 255, 255 };

        List<int[]> rejectedList = new List<int[]> { };


        static BitmapImage ConvertBitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            var memory = new MemoryStream();
            bitmap.Save(memory, ImageFormat.Png);

            memory.Position = 0;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }

        public MainWindow()
        {
         

            List<int[]> rejectedList = new List<int[]> { rejected1 };

            _processImage = new();
            // 


            InitializeComponent();
            Image_Display.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Browse_Image(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                // setup dulu bro gambarnya
                filePath = openFileDialog.FileName;

                // _processImage.SetInputImage(filePath);

                ImageBrowsed = new BitmapImage(new Uri(filePath));
                // ImageBrowsed = ConvertBitmapToBitmapImage(_processImage.GetAnnotatedBitmapImage());

                placeholder_text.Visibility = System.Windows.Visibility.Collapsed;
                Image_Display.Visibility = System.Windows.Visibility.Visible;
                Image_Display.Source = ImageBrowsed;
                Origin_Display.Text = filePath;

            }

            


        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // jika file path ga ditemukan dan bitmap masih null
            if (filePath == null || ImageBrowsed == null)
            {
                MessageBox.Show("Please Insert an Image First");
                return;
            }

            // process Image dan loading
            Image_Display.Visibility = System.Windows.Visibility.Hidden;
            placeholder_text.Visibility = System.Windows.Visibility.Visible;
            placeholder_text.Text = "Processing..."; // loading first
            _processImage.SetAttributes(accepted, rejectedList, ColorSpaceMethod.HSV);
            _processImage.SetInputImage(filePath);
            ImageBrowsed = ConvertBitmapToBitmapImage(_processImage.GetAnnotatedBitmapImage());

            // display result image
            placeholder_text.Visibility = System.Windows.Visibility.Collapsed;
            Image_Display.Visibility = System.Windows.Visibility.Visible;
            Image_Display.Source = ImageBrowsed;


            // save the data
            saveData.AcceptView = _processImage.GetAcceptedCount();
            saveData.RejectView = _processImage.GetRejectedCount();
            saveData.Total();

            // display the data in ui
            Terima_Display.Text = saveData.AcceptView.ToString();
            Tolak_Display.Text = saveData.RejectView.ToString();
            Total_Display.Text = saveData.total.ToString();
        }


        private void Save_Image(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            // jika file path ga ditemukan dan bitmap masih null
            if (filePath == null || ImageBrowsed == null)
            {
                MessageBox.Show("Please Browse an Image First");
            } else
            {
                save.Title = "Save picture as";
                save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (save.ShowDialog() == true)
                {
                    JpegBitmapEncoder jpg = new JpegBitmapEncoder();
                    jpg.Frames.Add(BitmapFrame.Create(ImageBrowsed));
                    using (Stream stm = File.Create(save.FileName))
                    {
                        jpg.Save(stm);
                    }
                }

            }

        }
    }
}
