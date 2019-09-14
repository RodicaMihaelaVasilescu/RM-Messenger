using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.View
{
  /// <summary>
  /// Interaction logic for ChatView.xaml
  /// </summary>
  public partial class ChatControl : UserControl
  {
    public ChatControl()
    {
      InitializeComponent();
      MessageBox.Focus();
    }
    private void TS_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
      ScrollViewer scrollviewer = sender as ScrollViewer;
      if (e.Delta > 0)
      {
        scrollviewer.LineUp();
      }
      else
      {
        scrollviewer.LineDown();
      }
      e.Handled = true;
    }

    private void EmoticonsButton_MouseEnter(object sender, MouseEventArgs e)
    {
      EmoticonsPopupTooltip.IsOpen = true;
    }
    private void EmoticonsTooltip_MouseLeave(object sender, MouseEventArgs e)
    {
      EmoticonsPopupTooltip.IsOpen = false;
    }

    private void EmoticonsButton_MouseLeave(object sender, MouseEventArgs e)
    {
      if (!EmoticonsPopupTooltip.IsMouseOver)
      {
        EmoticonsPopupTooltip.IsOpen = false;
      }
    }
  }
}
