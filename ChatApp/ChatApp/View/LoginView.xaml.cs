using ChatApp.ViewModel;
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
using System.Windows.Shapes;

namespace ChatApp.View
{
  /// <summary>
  /// Interaction logic for LoginView.xaml
  /// </summary>
  public partial class LoginView : Window
  {
    public LoginView()
    {
      InitializeComponent();
      DataContext = new LoginViewModel(this);
      var desktopWorkingArea = SystemParameters.WorkArea;
      //this.Width = desktopWorkingArea.Right/5;
      this.Width = 370;
      this.Left = desktopWorkingArea.Right - this.Width;
      this.Top = desktopWorkingArea.Top;
      this.Height = desktopWorkingArea.Bottom;
    }
  }
}
