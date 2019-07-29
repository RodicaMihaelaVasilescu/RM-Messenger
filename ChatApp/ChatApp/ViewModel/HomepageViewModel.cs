using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Model;
using ChatApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
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

        private string _welcomeText;

        private ObservableCollection<MessageModel> messagesList;

        private ObservableCollection<UserModel> contactsList;
        private string messageBoxContent;
        private UserModel selectedContact;

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
        #endregion

        #region Constructor
        public HomepageViewModel(Window window)
        {
            this.window = window;
            WelcomeText = "Welcome ";
            LogoutCommand = new RelayCommand(LogoutCommandExecute);
            SendCommand = new RelayCommand(SendCommandExecute);

            ContactsList = new ObservableCollection<UserModel>();

            ContactsList.Add(new UserModel { Email = "email", FirstName = "Rodica", LastName = "Mihaela" });
            ContactsList.Add(new UserModel { Email = "email", FirstName = "Diana", LastName = "Stefania" });

            MessagesList = new ObservableCollection<MessageModel>();
            MessagesList.Add(new MessageModel { Content = "My first Message", HorizontalAlignment = HorizontalAlignment.Left });
            MessagesList.Add(new MessageModel { Content = "My second Message", HorizontalAlignment = HorizontalAlignment.Right });
            MessagesList.Add(new MessageModel { HorizontalAlignment = HorizontalAlignment.Left, Content = "My third Message.WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" });
            MessagesList.Add(new MessageModel { Content = "My last Message", HorizontalAlignment = HorizontalAlignment.Right });
        }

        private void SelectCommandExecute(UserModel user)
        {
            Window child = new Window();
            child.Tag = "child";
            var chatViewModel = new ChatViewModel(child, user);
            WindowManager.ChangeWindowContent(child, chatViewModel, Resources.ChatWindowTitle, Resources.ChatControlPath);

            if (chatViewModel.CloseAction == null)
            {
                chatViewModel.CloseAction = () => window.Close();
            }
            child.Height = 400;
            child.Width = 600;
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
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
