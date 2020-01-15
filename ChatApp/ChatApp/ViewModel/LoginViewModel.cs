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
using ChatApp.Database;
using System.Linq;
using System.Configuration;

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

    public bool KeepMeLoggedIn
    {
      get { return _keepMeLoggedIn; }
      set
      {
        if (_keepMeLoggedIn == value) return;
        _keepMeLoggedIn = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KeepMeLoggedIn"));
      }
    }
    public bool SignInAutomatically
    {
      get { return _signInAutomatically; }
      set
      {
        if (_signInAutomatically == value) return;
        _signInAutomatically = value;
        ConfigurationSettings.AppSettings["SignInAutomatically"] = value == true ? "True" : "False";
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SignInAutomatically"));
      }
    }

    public bool SignInAsInvisible
    {
      get { return _signInAsInvisible; }
      set
      {
        if (_signInAsInvisible == value) return;
        _signInAsInvisible = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SignInAsInvisible"));
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
    private RMMessengerEntities _context;
    private Window window;
    private bool _keepMeLoggedIn;
    private bool _signInAutomatically;
    private bool _signInAsInvisible;

    #endregion

    #region Constructor
    public LoginViewModel(Window window)
    {
      _context = new RMMessengerEntities();
      this.window = window;
      UserModel.Instance.Email = string.IsNullOrEmpty(Email) ? string.Empty : Email.Split('@')[0]; ;

      LoginCommand = new RelayCommand(LoginCommandExecute);
      RegisterCommand = new RelayCommand(RegisterCommandExecute);
      ForgotPasswordCommand = new RelayCommand(ForgotPasswordCommandExecute);
      CancelCommand = new RelayCommand(CancelCommandExecute);
      StateBeforeLogin();
      if (KeepMeLoggedIn)
      {
        UserModel.Instance.Email = ConfigurationSettings.AppSettings.Get("Username");
        UserModel.Instance.EncryptedPassword = ConfigurationSettings.AppSettings.Get("EncryptedPassword");
        UserModel.Instance.Email = ConfigurationSettings.AppSettings.Get("Username");

      }
      if (AppConfigManager.ReadSetting("SignInAutomatically") == "True")
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
      KeepMeLoggedIn = ConfigurationSettings.AppSettings.Get("KeepMeLoggedIn") == "True" ? true : false;
      SignInAutomatically = ConfigurationSettings.AppSettings.Get("SignInAutomatically") == "True" ? true : false;
      SignInAsInvisible = ConfigurationSettings.AppSettings.Get("SignInAsInvisible") == "True" ? true : false;
    }

    #endregion

    #region Private Methods
    private void CancelCommandExecute()
    {
      cancelButtonPressed = true;
      ConfigurationSettings.AppSettings["SignInAutomatically"] = "False";
      var loginViewModel = new LoginViewModel(window);
      WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
    }
    private async void LoginCommandExecute()
    {
      string keepMeLoggedIn = ConfigurationSettings.AppSettings.Get("KeepMeLoggedIn");
      if (keepMeLoggedIn == "True")
      {
        Email = ConfigurationSettings.AppSettings.Get("Username");
        UserModel.Instance.Email = ConfigurationSettings.AppSettings.Get("Username");
        UserModel.Instance.EncryptedPassword = ConfigurationSettings.AppSettings.Get("EncryptedPassword");
      }
      if (!_context.Users.Any(u => u.Username == Email &&
         u.Password == UserModel.Instance.EncryptedPassword))
      {
        return;
      }
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
      var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      var settings = configFile.AppSettings.Settings;
      if (KeepMeLoggedIn)
      {
        settings["Username"].Value = Email;
        settings["EncryptedPassword"].Value = UserModel.Instance.EncryptedPassword;

      }
      else
      {
        settings["Username"].Value = string.Empty;
        settings["EncryptedPassword"].Value = string.Empty;

      }

      settings["KeepMeLoggedIn"].Value = KeepMeLoggedIn.ToString();
      settings["SignInAutomatically"].Value = SignInAutomatically.ToString();
      settings["SignInAsInvisible"].Value = SignInAsInvisible.ToString();
      configFile.Save(ConfigurationSaveMode.Modified);

      ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
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
      string keepMeLoggedIn = ConfigurationSettings.AppSettings.Get("KeepMeLoggedIn");
      if (keepMeLoggedIn == "True")
      {
        UserModel.Instance.EncryptedPassword = ConfigurationSettings.AppSettings.Get("EncryptedPassword");
        UserModel.Instance.Email = ConfigurationSettings.AppSettings.Get("Username");
        //LoginCommandExecute();
      }
    }
    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
