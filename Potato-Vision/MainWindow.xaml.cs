using System;
using System.Collections.Generic;
using System.Data;
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

namespace Potato_Vision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void FillDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn id = new DataColumn("ID", typeof(string));
            DataColumn name = new DataColumn("File Name", typeof(string));
            DataColumn accept = new DataColumn("Accept", typeof(int));
            DataColumn reject = new DataColumn("Reject", typeof(int));
            DataColumn total = new DataColumn("Total", typeof(int));


            dt.Columns.Add(id);
            dt.Columns.Add(name);
            dt.Columns.Add(accept);
            dt.Columns.Add(reject);
            dt.Columns.Add(total);

            //Yang diatas ini buat nunjukin kolom2 data gridnya, nah sekarang tinggal masukin daftarnya gimana
        }
    }
}
