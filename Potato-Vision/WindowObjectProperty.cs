using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ImageProcessing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
using Visual_Object;
using System.Collections.ObjectModel;
using OpenCvSharp;
using System.Data;

namespace Potato_Vision
{
    class UIModel : ObservableObject
    {
        private bool _startButton;
        private bool _warnaDropdown;
        private bool _saveDropdown;
        private bool _uploadDropdown;
        private System.Windows.Visibility _done_display = System.Windows.Visibility.Hidden;
        private string? _selectedColor = null;
        private string? _filepath = null;
        private VisualTargetSelection _selectedTarget;
        private string _acceptedObject = "-";
        private string _rejectedObject = "-";
        private string _totalObject = "-";
        private string _titleImage = "";

        private ObservableCollection<string> _selectWarnaList = new ObservableCollection<string>() { };


        public bool StartButton
        {
            get
            {
                return _startButton;
            }
            set
            {
                _startButton = value;
                OnPropertyChanged("StartButton");
            }
        }

        public bool SaveDropdownBool
        {
            get { return _saveDropdown; }
            set
            {
                _saveDropdown = value;
                OnPropertyChanged();
            }
        }

        public bool UploadDropdownBool
        {
            get { return _uploadDropdown; }
            set
            {
                _uploadDropdown = value;
                OnPropertyChanged();
            }
        }

        public bool WarnaDropdownBool
        {
            get 
            { 
                return _warnaDropdown; 
            }
            set
            {
                _warnaDropdown = value;
                OnPropertyChanged("WarnaDropdownBool");
            }

        }

        public ObservableCollection<string> SelectWarnaList
        {
            get { return _selectWarnaList; }
            set 
            { 
                _selectWarnaList = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedColor
        {
            get { return _selectedColor; }
            set 
            { 
                _selectedColor = value;
                CheckCanStart();
                OnPropertyChanged("SelectedColor");
            }
        }

        public string? FilePath
        {
            get { return _filepath; }
            set
            {
                _filepath = value;
                CheckCanStart();
                OnPropertyChanged("FilePath");
            }
        }

        public VisualTargetSelection SelectedTarget
        {
            get { return _selectedTarget; }
            set
            {
                _selectedTarget = value;
                CheckCanStart();
                OnPropertyChanged("SelectedTarget");
            }
        }

        public System.Windows.Visibility Done_Visibility
        {
            get { return _done_display; }
            set
            {
                _done_display = value;
                OnPropertyChanged("Done_Visibility");
            }
        }

        public string TotalAccept
        {
            get { return _totalObject; }
            set
            {
                _totalObject = value;
                OnPropertyChanged();
            }
        }

        public string RejectedObjectCount
        {
            get { return _rejectedObject; }
            set
            {
                _rejectedObject = value;
                OnPropertyChanged();
            }
        }

        public string AcceptedObjectCount
        {
            get { return _acceptedObject; }
            set
            {
                _acceptedObject = value;
                OnPropertyChanged();
            }
        }

        public string ImageTitle
        {
            get { return _titleImage; }
            set
            {
                _titleImage = value;
                OnPropertyChanged("ImageTitle");
            }
        }

        private void CheckCanStart()
        {
            if (_selectedColor != null  && _filepath != null)
            {
                StartButton = true;
            }
            else StartButton = false;
        }

        public void ClearAllResultUI()
        {
            RejectedObjectCount = "-";
            AcceptedObjectCount = "-";
            TotalAccept = "-";
            Done_Visibility = System.Windows.Visibility.Collapsed;
            SaveDropdownBool = false;
            UploadDropdownBool = false;
        }



        

    }

    class MainModel
    {

    }


    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
