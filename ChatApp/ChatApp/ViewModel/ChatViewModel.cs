﻿using ChatApp.Command;
using ChatApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
  class ChatViewModel : INotifyPropertyChanged
  {
    #region Public Properties

    public ICommand SendCommand { get; set; }
    public Action CloseAction { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    public ScrollViewer AutoScroll;
    public bool IsLogsChangedPropertyInViewModel { get; set; } = true;

    public DisplayedContactModel User
    {
      get { return user; }
      set
      {
        if (user == value) return;
        user = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
      }
    }

    public string ProfilePicture
    {
      get { return _profilePicture; }
      set
      {
        if (_profilePicture == value) return;
        _profilePicture = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicture"));
      }
    }

    public string PersonalProfilePicture
    {
      get { return _personalProfilePicture; }
      set
      {
        if (_personalProfilePicture == value) return;
        _personalProfilePicture = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonalProfilePicture"));
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

    #endregion

    #region Private Properties

    private string _profilePicture;
    private string _personalProfilePicture;
    private string messageBoxContent;
    private DisplayedContactModel user;
    private ObservableCollection<MessageModel> messagesList;
    private Window window;

    #endregion

    #region Constructor
    public ChatViewModel(Window window, DisplayedContactModel user, ScrollViewer scroll)
    {
      this.window = window;
      User = user;
      user.OnlineIcoPath = "pack://application:,,,/ChatApp;component/Resources/Offline.ico";
      AutoScroll = scroll;
      SendCommand = new RelayCommand(SendCommandExecute);
      ProfilePicture = user.ImagePath;
      PersonalProfilePicture = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg";

      //add fake messages 
      //TO DO : Server
      AddFakeMessages();
    }

    #endregion

    #region Private Methods

    private void AddFakeMessages()
    {
      MessagesList = new ObservableCollection<MessageModel>();
      MessagesList.Add(new MessageModel { SentBy = user.Name + "\n05-Sep-19 23:35:44", Content = "My first Message", HorizontalAlignment = HorizontalAlignment.Left });
      MessagesList.Add(new MessageModel { SentBy = UserModel.Instance.Email + "\n05-Sep-19 23:36:43", Content = "My second Message", HorizontalAlignment = HorizontalAlignment.Right });
      MessagesList.Add(new MessageModel { SentBy = user.Name + "\n05-Sep-19 23:37:29", HorizontalAlignment = HorizontalAlignment.Left, Content = "My third Message.AWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" });
      MessagesList.Add(new MessageModel { SentBy = user.Name + "\n05-Sep-19 23:37:45", HorizontalAlignment = HorizontalAlignment.Left, Content = "My Message." });
      MessagesList.Add(new MessageModel { SentBy = user.Name + "\n05-Sep-19 23:37:53", HorizontalAlignment = HorizontalAlignment.Left, Content = "Another Message." });
      MessagesList.Add(new MessageModel { SentBy = UserModel.Instance.Email + "\n05-Sep-19 23:38:21", Content = "My last Message", HorizontalAlignment = HorizontalAlignment.Right });
    }

    private void SendCommandExecute()
    {
      if(string.IsNullOrEmpty(messageBoxContent))
      {
        return;
      }
      MessagesList.Add(new MessageModel {SentBy = UserModel.Instance.Email+ "\n" + DateTime.Now, Content = MessageBoxContent, HorizontalAlignment = HorizontalAlignment.Right });
      MessageBoxContent = string.Empty;
      AutoScroll.ScrollToEnd();
    }

    #endregion

  }
}
