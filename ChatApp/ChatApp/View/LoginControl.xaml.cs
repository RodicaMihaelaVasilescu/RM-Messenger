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
using System.Windows.Forms;
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
  public partial class LoginControl : System.Windows.Controls.UserControl
  {
    public LoginControl()
    {
      InitializeComponent();
      Email.Focus();
      VerticalAlignment = VerticalAlignment.Top;
    }

    private void Click(object sender, RoutedEventArgs e)
    {
      Email.Text = null;
      Password.Password = null;
    }

    private void Email_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        Password.Focus();
      }
    }

    private void Password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      Login.IsDefault = true;
    }
  }
}
