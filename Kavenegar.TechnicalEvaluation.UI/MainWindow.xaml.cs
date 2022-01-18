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
using Kavenegar.TechnicalEvaluation.FileManager;
using Kavenegar.TechnicalEvaluation.FileManager.Manager;
using Microsoft.Win32;

namespace Kavenegar.TechnicalEvaluation.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IExcelManager _excelManager;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
            //var bb = new ExcelManager(@"H:\KaveNegar\TestKaveExl.xlsx").ReadExcelToDataSet();
            //var a = ConfigurationManager.DefaultConnectionString;
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
                if (fileDialog.ShowDialog() == true)
                {
                    btnSelectExcel.IsEnabled = false;
                    _excelManager = new ExcelManager(fileDialog.FileName);                    
                    Task tsa = _excelManager.BulkInsertChunkedExcelToDbAsync();
                    await tsa;
                    lblInsertStatus.Foreground = Brushes.Green;
                    lblInsertStatus.Content = "Success";
                }
            }
            catch (Exception)
            {
                lblInsertStatus.Foreground = Brushes.Red;
                lblInsertStatus.Content = "Failed";
            }

            btnSelectExcel.IsEnabled = true;

        }
    }
}
