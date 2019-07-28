using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Model;
using ChatApp.Properties;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ChatApp.ViewModel
{
    class HomepageViewModel : INotifyPropertyChanged
    {
        #region Properties
        public Action CloseAction { get; set; }

        private Window window;

        public ICommand LogoutCommand { get; set; }

        private string _welcomeText;

        public string WelcomeText
        {
            get { return _welcomeText; }
            set
            {
                if (_welcomeText == value) return;
                _welcomeText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WelcomeText"));
            }
        }
    #endregion

        #region Constructor
        public HomepageViewModel(Window window)
        {
            this.window = window;
            LogoutCommand = new RelayCommand(LogoutCommandExecute);
            WelcomeText = UserModel.Instance.Email;
        }
    #endregion
   
        #region Private Methods
        private void LogoutCommandExecute()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var loginViewModel = new LoginViewModel(window);
                WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
