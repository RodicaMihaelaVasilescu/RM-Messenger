using ChatApp.Command;
using ChatApp.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChatApp.ViewModel
{
  class DisplayImageViewModel : INotifyPropertyChanged
  {

    public Action CloseAction { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand SelectImageCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    private BitmapImage profilePicturePath;
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
      //ProfilePicturePath = new BitmapImage(
      //    //  new Uri(@"pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg"));
      ProfilePicturePath = new BitmapImage(
  new Uri(@"pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg"));
      ProfilePicturePath.CacheOption = BitmapCacheOption.None;
      Image image2 = Image.FromFile(Path.GetDirectoryName(
          Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
          "\\Resources\\ProfilePicture.jpg");
      image2.Dispose();
      //ProfilePicturePath = Resources.ProfilePictureAccount;
      //ProfilePicturePath.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
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

      try
      {
        string path = Path.GetDirectoryName(
          Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
          "\\Resources\\ProfilePicture.jpg"; ;
        Image image2 = Image.FromFile(path);
        image2.Dispose();
        File.Delete(path);  // works


        //if (File.Exists(path))
        //{
        //  File.Delete(path);
        //}
        //string FileToReplace = newFile;
        File.Copy(newFile, path);
        ProfilePicturePath = new BitmapImage(new Uri(Path.GetDirectoryName(
          Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
          "\\Resources\\ProfilePicture.jpg"));
        ProfilePicturePath.CacheOption = BitmapCacheOption.None;
        Image image3 = Image.FromFile(Path.GetDirectoryName(
          Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
          "\\Resources\\ProfilePicture.jpg");
        image3.Dispose();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

    }
  }
}
