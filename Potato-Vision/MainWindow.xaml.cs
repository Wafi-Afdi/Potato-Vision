using System;
using System.Collections.Generic;
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
        private string filePath = "";
        private int? Terima;
        private int? Tolak;
        private int? Total;




        public MainWindow()
        {
         
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
                filePath = openFileDialog.FileName;
                ImageBrowsed = new BitmapImage(new Uri(filePath));
                placeholder_text.Visibility = System.Windows.Visibility.Collapsed;
                Image_Display.Visibility = System.Windows.Visibility.Visible;
                Image_Display.Source = ImageBrowsed;
                Origin_Display.Text = filePath;
            }

            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // jika file path ga ditemukan dan bitmap masih null
            if (filePath == "" && ImageBrowsed == null)
            {
                MessageBox.Show("Please Browse an Image First");
            }
        }
        private void Save_Image(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            // jika file path ga ditemukan dan bitmap masih null
            if (filePath == "" && ImageBrowsed == null)
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
