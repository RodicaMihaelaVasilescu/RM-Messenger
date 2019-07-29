using ChatApp.Command;
using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    class ChatViewModel: INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

        private UserModel user;
        private string messageBoxContent;
        private ObservableCollection<MessageModel> messagesList;
        private Window window;

        ICommand SendCommand { get; set; }

        public UserModel User
        {
            get { return user; }
            set
            {
                if (user == value) return;
                user = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
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
        public ChatViewModel(Window window,UserModel user)
        {
            this.window = window;
            User = user;
            SendCommand = new RelayCommand(SendCommandExecute);
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
