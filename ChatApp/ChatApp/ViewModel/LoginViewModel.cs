using System;
using System.ComponentModel;
using ChatApp.Model;
using System.Windows;
using System.Windows.Input;
using ChatApp.Helper;
using ChatApp.Properties;
using ChatApp.Command;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.ViewModel
{
  class LoginViewModel : INotifyPropertyChanged
  {
    #region Properties

    //private ForgotPasswordViewModel forgotPasswordViewModel;

    private Window window;

    private bool cancelButtonPressed;
    public ICommand LoginCommand { get; set; }

    public ICommand RegisterCommand { get; set; }

    public ICommand ForgotPasswordCommand { get; set; }

    public ICommand CancelCommand { get; set; }

    public Action CloseAction { get; set; }

    private string _email;
    private string gif;
    private Visibility visibilityOfLoginFields;
    private Visibility visibilityOfMessageOnSingIn;
    private string messageOnSingingIn;
    private bool isSignInAsInvisibleChecked;

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

    public bool IsSignInAsInvisibleChecked
    {
      get { return isSignInAsInvisibleChecked; }
      set
      {
        if (isSignInAsInvisibleChecked == value) return;
        isSignInAsInvisibleChecked = value;
        UserModel.Instance.IsActive = !value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSignInAsInvisibleChecked"));
      }
    }

    public string MessageOnSingingIn
    {
      get { return messageOnSingingIn; }
      set
      {
        if (messageOnSingingIn == value) return;
        messageOnSingingIn = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MessageOnSingingIn"));
      }
    }

    public string Gif
    {
      get { return gif; }
      set
      {
        if (gif == value) return;
        gif = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gif"));
      }
    }

    public Visibility VisibilityOfLoginFields
    {
      get { return visibilityOfLoginFields; }
      set
      {
        if (visibilityOfLoginFields == value) return;
        visibilityOfLoginFields = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VisibilityOfLoginFields"));
      }
    }

    public Visibility VisibilityOfMessageOnSingIn
    {
      get { return visibilityOfMessageOnSingIn; }
      set
      {
        if (visibilityOfMessageOnSingIn == value) return;
        visibilityOfMessageOnSingIn = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VisibilityOfMessageOnSingIn"));
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
      CancelCommand = new RelayCommand(CancelCommandExecute);
      Gif = "pack://application:,,,/ChatApp;component/Resources/YahooMessengerSleep.gif";
      visibilityOfLoginFields = Visibility.Visible;
      visibilityOfMessageOnSingIn = Visibility.Hidden;
      cancelButtonPressed = false;

      Login();
    }

    #endregion

    #region Private Methods
    private void CancelCommandExecute()
    {
      cancelButtonPressed = true;
      var loginViewModel = new LoginViewModel(window);
      WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
      //window.VerticalContentAlignment = VerticalAlignment.Top;
    }
    private async void LoginCommandExecute()
    {
      UserModel.Instance.Email = Email;
      //if (UserModel.Instance.Email == null || UserModel.Instance.Password == null)
      //{
      //  MessageBox.Show("Both email and password should be filled in.");
      //  return;
      //}
      if (true/*AccountManager.AccountExists(UserModel.Instance.Email, UserModel.Instance.Password)*/)
      {
        Gif = "pack://application:,,,/ChatApp;component/Resources/YahooMessengerAwake.gif";
        MessageOnSingingIn = "Sign in as " + Email;
        VisibilityOfLoginFields = Visibility.Hidden;
        VisibilityOfMessageOnSingIn = Visibility.Visible;
        await Task.Delay(5000);
        if (cancelButtonPressed)
        {
          return;
        }

        var homepageViewModel = new HomepageViewModel(window);
        WindowManager.ChangeWindowContent(window, homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);

        if (homepageViewModel.CloseAction == null)
        {
          homepageViewModel.CloseAction = () => window.Close();
        }
        var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

        WindowManager.ResizeWindow(window);

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
