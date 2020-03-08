using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace QuanLyKho.ViewModel
{
    class InputViewModel : BaseViewModel
    {
        public System.Data.DataTable _dtTable ;
        public System.Data.DataTable  dtTable
        {
            get
            {
                return _dtTable;
            }
            set
            {
                _dtTable = value;
                OnPropertyChanged("dtTable");
            }
        }
        public List<string> ListHeader = new List<string> {"Number","Name", "InputDate", "Amount", "BuyPrice", "SellPrice", "Status" };
        private WHObject _Object;

        public WHObject Object
        {
            get
            {
                return _Object;
            }
            set
            {
                _Object = value;
                OnPropertyChanged("Object");
            }
        }
        public DataView _dtView;
        public DataView dtView
        {
            get
            {
                return _dtView;
            }
            set
            {
                _dtView = value;
                OnPropertyChanged("dtView");
            }
        }

        public ICommand ImportExcelCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        
        public InputViewModel()
        {
            Object = new WHObject();
            dtTable = new System.Data.DataTable();
            dtView = new DataView(dtTable);
            Object.InputDate = DateTime.Now;
            foreach(string a in ListHeader)
            {
                dtTable.Columns.Add(a, typeof(string));
            }
            AddCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                if (Object.Name == null || Object.SellPrice == null || Object.InputDate == null || Object.Status == null || Object.Amount == null || Object.BuyPrice == null)
                {
                    System.Windows.Forms.MessageBox.Show("Nhập đủ thông tin đi !!");
                    return;
                }
                else if (Object.Name == "" || Object.SellPrice == "" || Object.Status == "" || Object.Status == "" || Object.Amount == "" || Object.BuyPrice == "")
                {
                    System.Windows.Forms.MessageBox.Show("Nhập đủ thông tin đi !!");
                    return;
                }
                DataRow dtRow = dtTable.NewRow();
                int Numberring = dtTable.Rows.Count + 1;
                dtRow.BeginEdit();
                dtRow[ListHeader[0]] = Numberring.ToString();
                dtRow[ListHeader[1]] = Object.Name;
                dtRow[ListHeader[2]] = Object.InputDate.ToString();
                dtRow[ListHeader[3]] = Object.Amount;
                dtRow[ListHeader[4]] = Object.BuyPrice;
                dtRow[ListHeader[5]] = Object.SellPrice;
                dtRow[ListHeader[6]] = Object.Status;
                dtRow.EndEdit();
                dtTable.Rows.Add(dtRow);
                dtTable.AcceptChanges();

            }
              );
            EditCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {

            }
              );
            DeleteCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {

            }
              );
            ImportExcelCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {

            }
              );
            ExportExcelCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                ImportExcel();
            }
              );

        }
    
        private void ImportExcel()
        {
            try 
            {

                System.Windows.Forms.OpenFileDialog FileOpen = new System.Windows.Forms.OpenFileDialog();
                FileOpen.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                string FilePath = "";
                if(FileOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FilePath = FileOpen.FileName;
                    Excel.Application myExcel = new Excel.Application();
                    Excel.Workbook myWorkBook = myExcel.Workbooks.Open(FilePath);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error!" + e.Message);
            }
        }
        private void ExportExcel(System.Data.DataTable dtTable , string FilePath = null)
        {
            try
            {
                if (dtTable == null || dtTable.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (var i = 0; i < dtTable.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = dtTable.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < dtTable.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < dtTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = dtTable.Rows[i][j];
                    }
                }

                // check file path
                if (!string.IsNullOrEmpty(FilePath))
                {
                    try
                    {
                        workSheet.SaveAs(FilePath);
                        excelApp.Quit();
                        System.Windows.Forms.MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    
    }
    public class WHObject
    {
        public string Name { get; set; }
        public DateTime InputDate { get; set; }
        public string Amount { get; set; }
        public string BuyPrice { get; set; }
        public string SellPrice { get; set; }
        public string Status { get; set; }

    }
}
