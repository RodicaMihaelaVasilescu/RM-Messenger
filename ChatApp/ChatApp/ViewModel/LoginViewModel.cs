using System;
using System.ComponentModel;
using ChatApp.Model;
using System.Windows;
using System.Windows.Input;
using ChatApp.Helper;
using ChatApp.Properties;
using ChatApp.Command;
using System.Threading.Tasks;
using ChatApp.Helpers;

namespace ChatApp.ViewModel
{
  class LoginViewModel : INotifyPropertyChanged
  {
    #region Public Properties

    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }
    public ICommand ForgotPasswordCommand { get; set; }
    public ICommand CancelCommand { get; set; }
    public Action CloseAction { get; set; }

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
        UserModel.Instance.IsOnline = !value;
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

    #region Private Properties

    private string _email;
    private string gif;
    private string messageOnSingingIn;
    private bool isSignInAsInvisibleChecked;
    private bool cancelButtonPressed;
    private Visibility visibilityOfLoginFields;
    private Visibility visibilityOfMessageOnSingIn;
    private Window window;

    #endregion

    #region Constructor
    public LoginViewModel(Window window)
    {
      this.window = window;
      UserModel.Instance.Email = string.IsNullOrEmpty(Email) ? string.Empty : Email.Split('@')[0]; ;

      LoginCommand = new RelayCommand(LoginCommandExecute);
      RegisterCommand = new RelayCommand(RegisterCommandExecute);
      ForgotPasswordCommand = new RelayCommand(ForgotPasswordCommandExecute);
      CancelCommand = new RelayCommand(CancelCommandExecute);

      StateBeforeLogin();

      if (AppConfigManager.ReadSetting("SignInAutomatically") == "true")
      {
        LoginCommandExecute();
      }
      Login();
    }

    private void StateBeforeLogin()
    {
      Gif = "pack://application:,,,/ChatApp;component/Resources/YahooMessengerSleep.gif";
      visibilityOfLoginFields = Visibility.Visible;
      visibilityOfMessageOnSingIn = Visibility.Hidden;
      cancelButtonPressed = false;
    }

    #endregion

    #region Private Methods
    private void CancelCommandExecute()
    {
      cancelButtonPressed = true;
      var loginViewModel = new LoginViewModel(window);
      WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
    }
    private async void LoginCommandExecute()
    {
      StateAfterLogin();

      await Task.Delay(5000);

      if (cancelButtonPressed)
      {
        return;
      }

      SwitchToHomepageWindow();
    }

    private void SwitchToHomepageWindow()
    {
      var homepageViewModel = new HomepageViewModel(window);
      WindowManager.ChangeWindowContent(window, homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);

      if (homepageViewModel.CloseAction == null)
      {
        homepageViewModel.CloseAction = () => window.Close();
      }

      WindowManager.ResizeWindow(window);
      window.ResizeMode = ResizeMode.NoResize;
    }

    private void StateAfterLogin()
    {
      Email = string.IsNullOrEmpty(Email) ? string.Empty : Email.Split('@')[0];
      UserModel.Instance.Email = Email;
      MessageOnSingingIn = "Signing in as " + Email;
      VisibilityOfLoginFields = Visibility.Hidden;
      VisibilityOfMessageOnSingIn = Visibility.Visible;
      Gif = "pack://application:,,,/ChatApp;component/Resources/YahooMessengerAwake.gif";
    }

    private void RegisterCommandExecute()
    {
      //to do
    }

    public void ForgotPasswordCommandExecute()
    {  
      //to do
    }

    private void Login()
    {
    }
    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
