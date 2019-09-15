using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Model;

namespace ChatApp.ViewModel
{
  class EmoticonsViewModel : INotifyPropertyChanged
  {
    private string _selectedEmoticon = "pack://application:,,,/ChatApp;component/Resources/Emoticons/43.gif";
    private List<List<string>> _emoticonsMatrix;

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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedEmoticon"));
      }
    }

    public EmoticonsViewModel()
    {
      InitializeEmoticonList();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    void InitializeEmoticonList()
    {

      EmoticonsMatrix = new List<List<string>>();
      int counter = 1;
      for (int index = 1; index < 80 / 9; index++)
      {
        var newLine = new List<string>();
        for (int index2 = 0; index2 < 6; index2++)
        {
          if (counter < 80)
          {
            newLine.Add(string.Format("pack://application:,,,/ChatApp;component/Resources/Emoticons/{0}.gif", counter++));
          }
        }

        EmoticonsMatrix.Add(newLine);
      }



    }
  }
}
