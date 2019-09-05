using ChatApp.Helper;
using ChatApp.Properties;
using ChatApp.View;
using ChatApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Model
{
  class ContactListsModel: INotifyPropertyChanged
  {
    private DisplayedContactModel _selectedContact;

    public string ListName { get; set; }
    public string ImagePath { get; set; }
    public List<DisplayedContactModel> ContactsList { get; set; }
    public DisplayedContactModel SelectedContact
    {
      get { return _selectedContact; }
      set
      {
        if (_selectedContact == value) return;
        _selectedContact = value;
        if(value != null)
        {
          WindowManager.OpenChatWindow(value);
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedContact"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
