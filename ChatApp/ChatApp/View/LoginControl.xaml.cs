using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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
      string keepMeLoggedIn = ConfigurationSettings.AppSettings.Get("KeepMeLoggedIn");
      string signInAutomatically = ConfigurationSettings.AppSettings.Get("SignInAutomatically");
      string signInAsInvisible = ConfigurationSettings.AppSettings.Get("SignInAsInvisible");
      UserModel.Instance.IsOnline = signInAsInvisible == "True";

      if (signInAutomatically == "True")
      {
        SignInAutomatically.IsChecked = true;
      }

      if (signInAsInvisible == "True")
      {
        SignInAsInvisible.IsChecked = true;
      }

      if (keepMeLoggedIn == "True")
      {
        KeepMeLoggedIn.IsChecked = true;
        Password.Password = ConfigurationSettings.AppSettings.Get("EncryptedPassword");
        UserModel.Instance.EncryptedPassword = ConfigurationSettings.AppSettings.Get("EncryptedPassword");
        UserModel.Instance.Email = ConfigurationSettings.AppSettings.Get("Username");
        Email.Text = ConfigurationSettings.AppSettings.Get("Username");
      }
    }
    private void Password_PasswordChanged(object sender, RoutedEventArgs e)
    {
      UserModel.Instance.EncryptedPassword = getHashSha256(Password.Password);
    }
    public static string getHashSha256(string text)
    {
      byte[] bytes = Encoding.Unicode.GetBytes(text);
      SHA256Managed hashstring = new SHA256Managed();
      byte[] hash = hashstring.ComputeHash(bytes);
      string hashString = string.Empty;
      foreach (byte x in hash)
      {
        hashString += String.Format("{0:x2}", x);
      }
      return hashString;
    }

    private void Click(object sender, RoutedEventArgs e)
    {
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
