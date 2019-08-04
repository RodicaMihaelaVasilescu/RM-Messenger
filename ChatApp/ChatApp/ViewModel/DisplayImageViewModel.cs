using ChatApp.Command;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChatApp.ViewModel
{
  class DisplayImageViewModel : INotifyPropertyChanged
  {
    private BitmapImage profilePicturePath;

    public Action CloseAction { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand SelectImageCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public BitmapImage ProfilePicturePath
    {
      get { return profilePicturePath; }
      set
      {
        profilePicturePath = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicturePath"));
      }
    }
    public DisplayImageViewModel()
    {
      CloseCommand = new RelayCommand(CloseCommandExecute);
      SelectImageCommand = new RelayCommand(SelectImageCommandExecute);
      ProfilePicturePath = new BitmapImage(
        new Uri(@"pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg"));
      ProfilePicturePath.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
    }

    private void CloseCommandExecute()
    {
      CloseAction?.Invoke();
    }

    private void SelectImageCommandExecute()
    {
      Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
      dialog.Filter = "All files (*.*)|*.*|PNG files (*.png)|*.png*|JPG files (*.jpg)|*.jpg*";
      dialog.ShowDialog();
      var newFile = dialog.FileName;
      if (string.IsNullOrEmpty(newFile) || ProfilePicturePath == null)
      {
        return;
      }
      //var uri = new System.Uri("c:\\foo");
      //var converted = uri.AbsoluteUri;
      //var path = ProfilePicturePath.UriSource.AbsolutePath;
      //File.Delete(ProfilePicturePath.UriSource.AbsolutePath);
      //var file = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources", "ProfilePicture.*").FirstOrDefault();
      ////File.Delete(file);
      ///
      var file = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Resources\\ProfilePcture" + Guid.NewGuid() + ".jpg";
   //   File.Create(file);
      //String originalPath = ProfilePicturePath.UriSource.OriginalString;
      //String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
      //var x = ProfilePicturePath.UriSource.AbsolutePath;
      //var y = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
      File.Copy(newFile, file);
      //var file = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources", "ProfilePicture.*").FirstOrDefault();
      ProfilePicturePath = new BitmapImage(new Uri(file));
      //ProfilePicturePath = new BitmapImage(
      //  new Uri("pack://application:,,,/ChatApp;component/Resources/ProfilePicture2.jpg"));
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilePicturePath"));
      // ProfilePicturePath = newFile;
      //File.Copy(newFile, "pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg", true);
    }
  }
}
