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
        private System.Windows.Visibility _done_display = System.Windows.Visibility.Hidden;
        private string? _selectedColor = null;
        private string? _filepath = null;
        private VisualTargetSelection? _selectedTarget = null;
        private string _acceptedObject = "-";
        private string _rejectedObject = "-";
        private string _totalObject = "-";

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

        public VisualTargetSelection? SelectedTarget
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
            private set
            {
                _totalObject = value;
                OnPropertyChanged("TotalAccept");
            }
        }

        public string RejectedObjectCount
        {
            get { return _rejectedObject; }
            private set
            {
                _totalObject = value;
                OnPropertyChanged("RejectedObjectCount");
            }
        }

        public string AcceptedObjectCount
        {
            get { return _acceptedObject; }
            private set
            {
                _totalObject = value;
                OnPropertyChanged("AcceptedObjectCount");
            }
        }

        private void CheckCanStart()
        {
            if (_selectedColor != null && _selectedTarget != null && _filepath != null)
            {
                StartButton = true;
            }
            else StartButton = false;
        }

        private void UpdateTotal()
        {
            int? reject_real;
            int? accept_real;
            int reject;
            int accept;
            int? total;
            if (int.TryParse(this._rejectedObject, out reject)) {
                reject_real = reject;
            }    
            else reject_real = null;

            if (int.TryParse(this._acceptedObject, out accept))
            {
                accept_real = accept;
            }
            else accept_real = null;

            if (reject_real == null)
            {
                if(accept_real == null)
                {
                    total = null;
                }
                total = accept_real;
            } 
            else if (accept_real == null)
            {
                if (reject_real == null)
                {
                    total = null;
                }
                total = reject_real;
            }
            else
            {
                total = reject_real + accept_real;
            }

            if (total == null)
            {
                
            }
            else TotalAccept = total.ToString();

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
