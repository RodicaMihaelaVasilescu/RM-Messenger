using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Model;
using ChatApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

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

    public List<ContactListsModel> ContactsLists
    {
      get { return _contactsLists; }
      set
      {
        if (_contactsLists == value) return;
        _contactsLists = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ContactsLists"));
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

    private string _welcomeText;
    private string _email;
    private string messageBoxContent;
    private Window window;
    private ObservableCollection<MessageModel> messagesList;
    private BitmapImage profilePicturePath;
    private List<ContactListsModel> _contactsLists;

    #endregion

    #region Constructor

    public HomepageViewModel(Window window)
    {
      this.window = window;

      LogoutCommand = new RelayCommand(LogoutCommandExecute);
      SendCommand = new RelayCommand(SendCommandExecute);

      InitializeContactList();
      InitializeUserProfile();
    }

    private void InitializeUserProfile()
    {
      WelcomeText = "Welcome ";

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

    private void InitializeContactList()
    {
      ContactsLists = new List<ContactListsModel>();
      AddRecentContactsFakeList();
      AddAdressBookFakeList();
    }

    private void AddRecentContactsFakeList()
    {
      var recent = new ContactListsModel
      {
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/RecentContact_Icon.png",
        ContactsList = new List<DisplayedContactModel>()
      };
      recent.ContactsList.Add(new DisplayedContactModel
      {
        Name = "test",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/OfflineProfilePicture.jpg",
        OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Offline.ico"
      });

      recent.ListName = string.Format("Recent Contacts (0/{0})", recent.ContactsList.Count);
      ContactsLists.Add(recent);
    }

    private void AddAdressBookFakeList()
    {
      var addressBook = new ContactListsModel
      {
        ContactsList = new List<DisplayedContactModel>()
      };
      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "test",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/OfflineProfilePicture.jpg",
        OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Offline.ico"
      });

      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "ankii29.an",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture2.jpg",
        OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Online.ico"

      });

      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "iasmi31_iasmi",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture4.jpg",
        OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Offline.ico"
      });
      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "madalina.mada",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture3.jpg",
        OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Online.ico"
      });
      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "mihaela1234",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/OfflineProfilePicture.jpg",
        OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Busy.ico"
      });

      addressBook.ListName = string.Format("Address Book (0/{0})", addressBook.ContactsList.Count);
      ContactsLists.Add(addressBook);
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
