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
    private string gif = "pack://application:,,,/ChatApp;component/Resources/Emoticons/43.gif";
    private List<EmoticonsRowModel> _emoticonsMatrix;

    //public ObservableCollection<string> EmoticonsList { get; set; }
    public List<EmoticonsRowModel> EmoticonsMatrix
    {
      get { return _emoticonsMatrix; }
      set
      {
        if (_emoticonsMatrix == value) return;
        _emoticonsMatrix = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmoticonsMatrix"));
      }
    }
    public string Gif
    {
      get { return gif; }
      set
      {
        if (gif == value) return;
        gif = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gif"));
      }
    }

    public EmoticonsViewModel()
    {
      InitializeEmoticonList();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    void InitializeEmoticonList()
    {

      //for (int emoticonId = 1; emoticonId < 141; emoticonId++)
      //{
      //  if (emoticonId >= 80 || emoticonId < 100)
      //  {
      //    continue;
      //  }
      //  emoticons.Add(string.Format("pack://application:,,,/ChatApp;component/Resources/Emoticons/{0}.gif", emoticonId));
      //}

      EmoticonsMatrix = new List<EmoticonsRowModel>();
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

        EmoticonsMatrix.Add(new EmoticonsRowModel
        {
          Emoticons = newLine
        });
      }



    }
  }
}
