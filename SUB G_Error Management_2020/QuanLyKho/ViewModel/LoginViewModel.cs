using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand ShowFindPwdWindow {get;set; }

        public ICommand ShowSignUpWindow { get; set; }

        public ICommand LoadedWindowCommand { get; set; }
    public LoginViewModel()
    {
            Isloaded = false;
            ShowFindPwdWindow = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {           
                           
            }
            );
            ShowSignUpWindow = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                SignUpWindow w = new SignUpWindow();
                w.ShowDialog();
            }
            );
        }
}
}
