﻿using ChatApp.Command;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
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
      string[] files = null;
      ProfilePicturePath = "pack://application:,,,/ChatApp;component/Resources/ProfilePicture.jpg";
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
      if (string.IsNullOrEmpty(newFile))
      {
        return;
      }
      var file = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources", "ProfilePicture.*").FirstOrDefault();
      //File.Delete(file);
      File.Copy(newFile, Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\ProfilePicture2" + Path.GetExtension(newFile), true);
      ProfilePicturePath = newFile;
    }
  }
}
