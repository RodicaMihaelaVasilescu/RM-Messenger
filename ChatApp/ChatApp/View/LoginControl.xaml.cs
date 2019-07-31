using ChatApp.Model;
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
  /// Interaction logic for LoginControl.xaml
  /// </summary>
  public partial class LoginControl : UserControl
  {
    public LoginControl()
    {
      InitializeComponent();
      VerticalAlignment = VerticalAlignment.Top;
    }
    private void Password_PasswordChanged(object sender, RoutedEventArgs e)
    {
      UserModel.Instance.Password = Password.Password;
    }

    private void Click(object sender, RoutedEventArgs e)
    {
      Email.Text = null;
      Password.Password = null;
    }
  }
}
