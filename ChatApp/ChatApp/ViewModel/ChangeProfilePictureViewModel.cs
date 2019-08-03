using ChatApp.Command;
using ChatApp.Helper;
using ChatApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
  class ChangeProfilePictureViewModel : INotifyPropertyChanged
  {
    public Action CloseAction { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand DisplayImageCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public Popup popup;
    public ChangeProfilePictureViewModel()
    {
      CloseCommand = new RelayCommand(CloseCommandExecute);
      DisplayImageCommand = new RelayCommand(DisplayImageCommandExecute);
    }

    private void DisplayImageCommandExecute()
    {
      var window =new Window();
      var displayImageViewModel = new DisplayImageViewModel();
      WindowManager.ChangeWindowContent(window, displayImageViewModel, Resources.DisplayImageTitle, Resources.DisplayImagePath);
      window.Width = 376;
      var padding = 10;
      window.Left = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width - window.Width - padding;
      window.Top = SystemParameters.WorkArea.Top - padding;

      //foreach (Window win in App.Current.Windows)
      //{
      //  if (win.Tag != null && win.Tag.ToString().EndsWith("Child"))
      //  {
      //    win.Close();
      //  }
      //}
      window.ShowDialog();
    }

    private void CloseCommandExecute()
    {
      //CloseAction?.Invoke();
      popup.IsOpen = false;
    }
  }
}
