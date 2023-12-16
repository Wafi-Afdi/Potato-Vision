using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private UIModel _uiModel = new UIModel();

        private ProcessImage _processImage;

        private TargetObject _targetVisual;
        private IList<List<ColorTarget>> collectionInApp = new List<List<ColorTarget>>() { };
        private List<ColorTarget> colorSelection = new List<ColorTarget>() { };


        // Data untuk processing image
        private List<int[]> rejectedList = new List<int[]> { };
        private int[] accepted = new int[6];

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

            // Nilai Range sementara
            //int[] accepted = { 28, 30, 32, 36, 255, 255 };
            //int[] rejected1 = { 171, 74, 56, 255, 255, 255 };

            //List<int[]> rejectedList = new List<int[]> { rejected1 };

            // _processImage = new ProcessImage(accepted, rejectedList, ColorSpaceMethod.HSV);
            _processImage = new ProcessImage();
            // 


            InitializeComponent();

            Image_Display.Visibility = System.Windows.Visibility.Hidden;
            DataContext = _uiModel;


            _targetVisual = new TargetObject();
            collectionInApp = TargetObject.ReadCollectionFruit;

        }

        private void Browse_Image(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png;*.jpeg";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                _uiModel.FilePath = openFileDialog.FileName;
                _uiModel.Done_Visibility = Visibility.Hidden;

                ImageBrowsed = new BitmapImage(new Uri(_uiModel.FilePath));
                // ImageBrowsed = ConvertBitmapToBitmapImage(_processImage.GetAnnotatedBitmapImage());

                placeholder_text.Visibility = System.Windows.Visibility.Collapsed;
                Image_Display.Visibility = System.Windows.Visibility.Visible;
                Image_Display.Source = ImageBrowsed;
                Origin_Display.Text = _uiModel.FilePath;

            }


        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // jika file path ga ditemukan dan bitmap masih null
            if (_uiModel.FilePath == null && ImageBrowsed == null)
            {
                MessageBox.Show("Please Browse an Image First");
            }

            // Loading 
            Image_Display.Visibility = System.Windows.Visibility.Hidden;
            placeholder_text.Text = "Loading...";
            placeholder_text.Visibility = System.Windows.Visibility.Visible;

            // Assign dulu
            _targetVisual.SetTargetColor();
            accepted = _targetVisual.GetTargetColor();
            rejectedList = _targetVisual.GetRejectedRange();


            // Gas Lakukan Aksi
            _processImage.SetAttributes(accepted, rejectedList, ColorSpaceMethod.HSV);
            _processImage.SetInputImage(_uiModel.FilePath!);

            // Display imagenya
            Image_Display.Source = ConvertBitmapToBitmapImage(_processImage.GetAnnotatedBitmapImage());
            Image_Display.Visibility = System.Windows.Visibility.Visible;
            placeholder_text.Visibility = System.Windows.Visibility.Collapsed;
            _uiModel.Done_Visibility = Visibility.Visible;

            // Perhitungan

        }
        private void Save_Image(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            // jika file path ga ditemukan dan bitmap masih null
            if (_uiModel.FilePath == null && ImageBrowsed == null)
            {
                MessageBox.Show("Please Browse an Image First");
            }
            else
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

        // Fungsi untuk mengubah target terpilih pada dropdown box 
        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {
            _uiModel.SelectWarnaList.Clear();
            _uiModel.SelectedTarget = (VisualTargetSelection)TargetComboBox.SelectedItem;
            Debug.WriteLine(_uiModel.SelectedTarget);
            _targetVisual.SettVisualTargetSelection(_uiModel.SelectedTarget);


            if (_uiModel.SelectedTarget.HasValue)
            {
                // Allow select pada warna
                colorSelection = collectionInApp.SelectMany(fruitList => fruitList.Where(target => target.target == _uiModel.SelectedTarget)).ToList();

                foreach (ColorTarget color in colorSelection)
                {
                    _uiModel.SelectWarnaList.Add(color.ColorName);
                }
                _uiModel.WarnaDropdownBool = true;
            }
        }

        private void UbahTargetWarna(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _uiModel.SelectedColor = (String)WarnaComboBox.SelectedItem;
                Debug.WriteLine(_uiModel.SelectedColor);
                _targetVisual.SetWarnaDipilih(_uiModel.SelectedColor);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
