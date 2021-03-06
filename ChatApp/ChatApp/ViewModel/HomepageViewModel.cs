﻿using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Model;
using ChatApp.Properties;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChatApp.ViewModel
{
  class HomepageViewModel : INotifyPropertyChanged
  {
    #region Public Properties

    public Action CloseAction { get; set; }
    public ICommand LogoutCommand { get; set; }
    public ICommand SendCommand { get; set; }
    public string OnlineIcoPath { get; set; } = UserModel.Instance.IsOnline ? "pack://application:,,,/ChatApp;component/Resources/Online.ico" : "pack://application:,,,/ChatApp;component/Resources/Offline.ico";

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

    public BitmapImage ProfilePicturePath
    {
      get { return profilePicturePath; }
      set
      {
        profilePicturePath = value;
        if (value != null) profilePicturePath.CacheOption = BitmapCacheOption.None;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicturePath"));
      }
    }

    #endregion

    #region Private Properties

    private string _email;
    private Window window;
    private BitmapImage profilePicturePath;

    #endregion

    #region Constructor

    public HomepageViewModel(Window window)
    {
      this.window = window;
      LogoutCommand = new RelayCommand(LogoutCommandExecute);
      InitializeUserProfile();
    }

    private void InitializeUserProfile()
    {
      Email = UserModel.Instance.Email;

      ProfilePicturePath = new BitmapImage(new Uri(@"pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg"));
      ProfilePicturePath.CacheOption = BitmapCacheOption.None;
      var path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Resources\\ProfilePicture.jpg";
      if (!File.Exists(path))
      {
        File.Create(path);
      }
      else
      {
        Image image = Image.FromFile(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Resources\\ProfilePicture.jpg");
        image.Dispose();
      }
    }
    #endregion

    #region Private Methods

    private void LogoutCommandExecute()
    {
      ConfigurationSettings.AppSettings["SignInAutomatically"] = "False";
      var loginViewModel = new LoginViewModel(window);
      WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
      window.VerticalContentAlignment = VerticalAlignment.Top;
      foreach (Window win in App.Current.Windows)
      {
        if (win.Tag != null && win.Tag.ToString().EndsWith("Child"))
        {
          win.Close();
        }
      }
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
