using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class SignUpViewModel : BaseViewModel
    {
        private string _FullNameText;

        private string _strFullNameHint;

        public string strFullNameHint
        {
            get { return _strFullNameHint; }
            set { _strFullNameHint = value;
                OnPropertyChanged("strFullNameHint"); 
            }
        }


        public string FullNameText
        {
            get { return _FullNameText; }
            set { _FullNameText = value;}
        }
        public ICommand FullNameHint { get; set; }
        public ICommand GenHint { get; set; }
        public ICommand LoadedWindowCommand { get ; set; }
        public SignUpViewModel()
        {
            FullNameHint = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                var tblFullName = p as TextBox;
                if (tblFullName.Text.Contains("1") == true)
                {
                    strFullNameHint = "Tên chứa số, Ký tự đặc biệt";
                }
                else
                    strFullNameHint = "";
            }
              );

            GenHint = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                var tblFullName = p as TextBox;
                if (tblFullName.Text.Contains("1") == true)
                {
                    strFullNameHint = "Tên chứa số, Ký tự đặc biệt";
                }
                else
                    strFullNameHint = "";
            }
              );

            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
              );
        }
    }
}
