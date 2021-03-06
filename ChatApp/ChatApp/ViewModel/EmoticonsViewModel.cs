﻿using ChatApp.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
  class EmoticonsViewModel : INotifyPropertyChanged
  {
    #region Private Properties

    private string _selectedEmoticon;
    private List<List<string>> _emoticonsMatrix;

    #endregion

    #region Public Properties
    public ICommand CancelCommand { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;

    public List<List<string>> EmoticonsMatrix
    {
      get { return _emoticonsMatrix; }
      set
      {
        if (_emoticonsMatrix == value) return;
        _emoticonsMatrix = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmoticonsMatrix"));
      }
    }

    public string SelectedEmoticon
    {
      get { return _selectedEmoticon; }
      set
      {
        if (_selectedEmoticon == value) return;
        _selectedEmoticon = value;
        SelectedEmoticon = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedEmoticon"));
      }
    }

    #endregion

    #region Constructor

    public EmoticonsViewModel()
    {
      InitializeEmoticonList();
      CancelCommand = new RelayCommand(CancelCommandExecute);
    }

    private void CancelCommandExecute()
    {

    }

    #endregion

    #region Private Methods

    private void InitializeEmoticonList()
    {
      EmoticonsMatrix = new List<List<string>>();
      int emoticonId = 1;
      for (int row = 0; row < 8; row++)
      {
        var emoticonsList = new List<string>();
        for (int column = 0; column < 6; column++)
        {
            emoticonsList.Add(string.Format("pack://application:,,,/ChatApp;component/Resources/Emoticons/{0}.gif", emoticonId++));
        }

        EmoticonsMatrix.Add(emoticonsList);
      }
      SelectedEmoticon = EmoticonsMatrix.FirstOrDefault().FirstOrDefault();
    }

    #endregion
  }
}
