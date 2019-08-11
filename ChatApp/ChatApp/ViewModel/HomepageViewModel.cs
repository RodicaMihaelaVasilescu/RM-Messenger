using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Model;
using ChatApp.Properties;
using ChatApp.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ChatApp.ViewModel
{
  class HomepageViewModel : INotifyPropertyChanged
  {
    #region Properties
    public Action CloseAction { get; set; }

    private Window window;

    public ICommand LogoutCommand { get; set; }

    public ICommand SendCommand { get; set; }
    public string IsActiveImagePath { get; set; } = UserModel.Instance.IsActive ? "pack://application:,,,/ChatApp;component/Resources/IsOnline.png" : "pack://application:,,,/ChatApp;component/Resources/IsOffline.png";

    private string _welcomeText;

    private ObservableCollection<MessageModel> messagesList;

    private ObservableCollection<UserModel> contactsList;
    private string messageBoxContent;
    private UserModel selectedContact;
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

    public string MessageBoxContent
    {
      get { return messageBoxContent; }
      set
      {
        if (messageBoxContent == value) return;
        messageBoxContent = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MessageBoxContent"));
      }
    }

    public ObservableCollection<MessageModel> MessagesList
    {
      get { return messagesList; }
      set
      {
        if (messagesList == value) return;
        messagesList = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MessagesList"));
      }
    }

    public ObservableCollection<UserModel> ContactsList
    {
      get { return contactsList; }
      set
      {
        if (contactsList == value) return;
        contactsList = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ContactsList"));
      }
    }

    public UserModel SelectedContact
    {
      get { return selectedContact; }
      set
      {
        if (selectedContact == value) return;
        selectedContact = value;
        SelectCommandExecute(SelectedContact);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedContact"));
      }
    }

    private BitmapImage profilePicturePath;
    public BitmapImage ProfilePicturePath
    {
      get { return profilePicturePath; }
      set
      {
        profilePicturePath = value;
        if(value != null) profilePicturePath.CacheOption = BitmapCacheOption.None;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicturePath"));
      }
    }

    #endregion

    #region Constructor
    public HomepageViewModel(Window window)
    {
      this.window = window;
      WelcomeText = "Welcome ";
      Email = UserModel.Instance.Email;
      LogoutCommand = new RelayCommand(LogoutCommandExecute);
      SendCommand = new RelayCommand(SendCommandExecute);
      ProfilePicturePath = null;
      ProfilePicturePath = new BitmapImage(
        new Uri(@"pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg"));
      ProfilePicturePath.CacheOption = BitmapCacheOption.None;
      Image image2 = Image.FromFile(Path.GetDirectoryName(
          Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
          "\\Resources\\ProfilePicture.jpg");
      image2.Dispose();
      ContactsList = new ObservableCollection<UserModel>();

      ContactsList.Add(new UserModel { Email = "email", FirstName = "Rodica", LastName = "Mihaela" });
      ContactsList.Add(new UserModel { Email = "email2", FirstName = "Rodica", LastName = "Vasilescu" });
      ContactsList.Add(new UserModel { Email = "email3", FirstName = "Random", LastName = "Name" });

      MessagesList = new ObservableCollection<MessageModel>();
      MessagesList.Add(new MessageModel { Content = "My first Message", HorizontalAlignment = HorizontalAlignment.Left });
      MessagesList.Add(new MessageModel { Content = "My second Message", HorizontalAlignment = HorizontalAlignment.Right });
      MessagesList.Add(new MessageModel { HorizontalAlignment = HorizontalAlignment.Left, Content = "My third Message.WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" });
      MessagesList.Add(new MessageModel { Content = "My last Message", HorizontalAlignment = HorizontalAlignment.Right });
    }

    private void SelectCommandExecute(UserModel user)
    {
      foreach (Window win in App.Current.Windows)
      {
        if (win != null)
          if (win.Tag != null && win.Tag.ToString() == user.Email + "Child")
          {
            win.Focus();
            return;
          }
      }
      Window child = new Window();
      child.Tag = user.Email + "Child";
      child.Title = Resources.ChatWindowTitle;
      var chatControl = new ChatControl();
      var chatViewModel = new ChatViewModel(child, user, chatControl.AutoScrollViewer);
      chatControl.DataContext = chatViewModel;
      child.Content = chatControl;
      if (chatViewModel.CloseAction == null)
      {
        chatViewModel.CloseAction = () => window.Close();
      }
      child.SizeToContent = SizeToContent.WidthAndHeight;
      child.Show();
    }

    private void SendCommandExecute()
    {
      MessagesList.Add(new MessageModel { Content = MessageBoxContent, HorizontalAlignment = HorizontalAlignment.Right });
      MessageBoxContent = string.Empty;
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
        window.VerticalContentAlignment = VerticalAlignment.Top;
        foreach (Window win in App.Current.Windows)
        {
          if (win.Tag != null && win.Tag.ToString().EndsWith("Child"))
          {
            win.Close();
          }
        }
      }

    }
    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
