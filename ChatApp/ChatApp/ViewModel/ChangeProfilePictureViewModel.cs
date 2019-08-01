using ChatApp.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
  class ChangeProfilePictureViewModel : INotifyPropertyChanged
  {
    public Action CloseAction { get; set; }
    public ICommand CloseCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public ChangeProfilePictureViewModel()
    {

      CloseCommand = new RelayCommand(CloseCommandExecute);
    }

    private void CloseCommandExecute()
    {
      CloseAction?.Invoke();
    }
  }
}
