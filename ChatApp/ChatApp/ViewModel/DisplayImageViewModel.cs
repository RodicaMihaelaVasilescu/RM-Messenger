using ChatApp.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
  class DisplayImageViewModel : INotifyPropertyChanged
  {
    private string profilePicturePath;

    public Action CloseAction { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand SelectImageCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public string ProfilePicturePath
    {
      get { return profilePicturePath; }
      set
      {
        if (profilePicturePath == value) return;
        profilePicturePath = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicturePath"));
      }
    }
    public DisplayImageViewModel()
    {
      CloseCommand = new RelayCommand(CloseCommandExecute);
      SelectImageCommand = new RelayCommand(SelectImageCommandExecute);
      ProfilePicturePath = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/Resources", "ProfilePicture.*").FirstOrDefault();
    }

    private void CloseCommandExecute()
    {
      CloseAction?.Invoke();
    }

    private void SelectImageCommandExecute()
    {

      //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
      Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
      dialog.Filter = "All files (*.*)|*.*|PNG files (*.png)|*.png*|JPG files (*.jpg)|*.jpg*";
      dialog.ShowDialog();

      var newFile = dialog.FileName;

      {
        Task.Factory.StartNew(() =>
        {
          var file = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/Resources", "ProfilePicture.*").FirstOrDefault();
          ProfilePicturePath = "D:\\GitHub\\ChatApp\\ChatApp\\ChatApp\\Resources\\SendImage.png";
          var fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
          File.Delete(file);
          //writer.WriteLine(content);
          //File.Replace(newFile, file, file + ".bck");
          File.Copy(newFile, Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/Resources/ProfilePicture" + Path.GetExtension(newFile));
          ProfilePicturePath = newFile;
        });
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicturePath"));

      }


    }
  }
}
