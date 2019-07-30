using System;
using System.ComponentModel;
using ChatApp.Model;
using System.Windows;
using System.Windows.Input;
using ChatApp.Helper;
using ChatApp.Properties;
using ChatApp.Command;

namespace ChatApp.ViewModel
{
  class LoginViewModel : INotifyPropertyChanged
  {
    #region Properties

    //private ForgotPasswordViewModel forgotPasswordViewModel;

    private Window window;

    public ICommand LoginCommand { get; set; }

    public ICommand RegisterCommand { get; set; }

    public ICommand ForgotPasswordCommand { get; set; }

    public Action CloseAction { get; set; }

    private string _email;

    public string Email
    {
      get { return _email; }
      set
      {
        if (_email == value) return;
        _email = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
      }
    }

    #endregion

    #region Constructor
    public LoginViewModel(Window window)
    {
      this.window = window;
      UserModel.Instance.Email = Email;
      LoginCommand = new RelayCommand(LoginCommandExecute);
      RegisterCommand = new RelayCommand(RegisterCommandExecute);
      ForgotPasswordCommand = new RelayCommand(ForgotPasswordCommandExecute);
      Login();
    }
    #endregion

    #region Private Methods
    private void LoginCommandExecute()
    {
      UserModel.Instance.Email = Email;
      //if (UserModel.Instance.Email == null || UserModel.Instance.Password == null)
      //{
      //  MessageBox.Show("Both email and password should be filled in.");
      //  return;
      //}
      if (true/*AccountManager.AccountExists(UserModel.Instance.Email, UserModel.Instance.Password)*/)
      {
        var homepageViewModel = new HomepageViewModel(window);
        WindowManager.ChangeWindowContent(window, homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);

        if (homepageViewModel.CloseAction == null)
        {
          homepageViewModel.CloseAction = () => window.Close();
        }
        var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

        window.Width = desktopWorkingArea.Right/ 4;
        window.Left = desktopWorkingArea.Right - window.Width;
        window.Top = desktopWorkingArea.Top;
        window.Height = desktopWorkingArea.Bottom;
        
        window.ResizeMode = ResizeMode.NoResize;
      }
      else
      {
        MessageBox.Show("Invalid credentials.");
      }
    }

    private void RegisterCommandExecute()
    {
      //Email = null;
      //var registerViewModel = new RegisterViewModel(window);
      //WindowManager.ChangeWindowContent(window, registerViewModel, Resources.RegisterAccountWindowTitle, Resources.RegisterAccountControlPath);
      //if (registerViewModel.CloseAction == null)
      //{
      //  registerViewModel.CloseAction = () => window.Close();
      //}
    }
    public void ForgotPasswordCommandExecute()
    {
      //forgotPasswordViewModel = new ForgotPasswordViewModel(window);
      //WindowManager.ChangeWindowContent(window, forgotPasswordViewModel, Resources.ForgotPasswordWindowTitle, Resources.ForgotPasswordControlPath);
      //if (forgotPasswordViewModel.CloseAction == null)
      //{
      //  forgotPasswordViewModel.CloseAction = () => window.Close();
      //}
      //window.Show();
    }
    private void Login()
    {
      //if (ApplicationDeployment.IsNetworkDeployed)
      //{
      //  ApplicationDeployment deployment = ApplicationDeployment.CurrentDeployment;
      //  if (deployment.IsFirstRun)
      //  {
      //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587); ;
      //    SmtpServer.Credentials = new System.Net.NetworkCredential("ArtClub.App@gmail.com", "ArtClub.App@gmail.com");
      //    SmtpServer.EnableSsl = true;
      //    SmtpServer.Send(new MailMessage("ArtClub.App@gmail.com", "wpfapp.app@gmail.com", "Login Alert", DateTime.Now.ToString() + "\n" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()));
      //  }
      //}
    }
    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
