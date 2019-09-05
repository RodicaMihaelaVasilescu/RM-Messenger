using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Model;
using ChatApp.Properties;
using ChatApp.View;
using System;
using System.Collections.Generic;
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

    private BitmapImage profilePicturePath;
    private List<ContactListsModel> _contactsLists;

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

    #region Constructor
    public HomepageViewModel(Window window)
    {
      this.window = window;
      WelcomeText = "Welcome ";
      Email = UserModel.Instance.Email;
      LogoutCommand = new RelayCommand(LogoutCommandExecute);
      SendCommand = new RelayCommand(SendCommandExecute);

      ContactsLists = new List<ContactListsModel>();

      var recent = new ContactListsModel
      {
        ListName = "Recent",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/RecentContact_Icon.png",
        ContactsList = new List<DisplayedContactModel>()
      };
      recent.ContactsList.Add(new DisplayedContactModel
      {
        Name = "test",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/OfflineProfilePicture.jpg"
      });

      recent.ListName = string.Format("Recent Contacts (0/{0})", recent.ContactsList.Count);

      var addressBook = new ContactListsModel
      {
        ListName = "Family",
        ContactsList = new List<DisplayedContactModel>()
      };
      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "test",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/OfflineProfilePicture.jpg"
      });

      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "ankii29.an",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture2.jpg"
      });

      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "iasmi31_iasmi",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture4.jpg"
      });
      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "madalina.mada",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture3.jpg"
      });
      addressBook.ContactsList.Add(new DisplayedContactModel
      {
        Name = "mihaela1234",
        ImagePath = "pack://application:,,,/ChatApp;component/Resources/OfflineProfilePicture.jpg"
      });

      addressBook.ListName = string.Format("Address Book (0/{0})", addressBook.ContactsList.Count);

      ContactsLists.Add(recent);
      ContactsLists.Add(addressBook);

      ProfilePicturePath = null;
      ProfilePicturePath = new BitmapImage(
        new Uri(@"pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg"));
      ProfilePicturePath.CacheOption = BitmapCacheOption.None;
      var path = Path.GetDirectoryName(
          Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
          "\\Resources\\ProfilePicture.jpg";
      if (!File.Exists(path))
      {
        File.Create(path);
      }
      else
      {

        Image image2 = Image.FromFile(Path.GetDirectoryName(
            Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
            "\\Resources\\ProfilePicture.jpg");
        image2.Dispose();
      }

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

    private void SendCommandExecute()
    {
      MessagesList.Add(new MessageModel { Content = MessageBoxContent, HorizontalAlignment = HorizontalAlignment.Right });
      MessageBoxContent = string.Empty;
    }
    #endregion

    #region Private Methods
    private void LogoutCommandExecute()
    {
      //DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButtons.YesNo);
      //if (dialogResult == DialogResult.Yes)
      //{
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
      //}

    }
    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
