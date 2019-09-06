using ChatApp.Model;
using ChatApp.Properties;
using ChatApp.View;
using ChatApp.ViewModel;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatApp.Helper
{
  public class WindowManager
  {
    public static void ChangeWindowContent(Window window, object viewModel, string title, string controlPath)
    {
      window.Title = title;
      //window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      //window.SizeToContent = SizeToContent.WidthAndHeight;
      var controlAssembly = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name);
      var controlType = controlAssembly.GetType(controlPath);
      var newControl = Activator.CreateInstance(controlType) as UserControl;
      newControl.DataContext = viewModel;
      window.Content = newControl;
    }
    public static void ResizeWindow(Window window)
    {
      
      var desktopWorkingArea = SystemParameters.WorkArea;
      window.Width = desktopWorkingArea.Right/4.5;
      window.Top = desktopWorkingArea.Top;
      window.Height = desktopWorkingArea.Bottom;
      window.Left = desktopWorkingArea.Right - window.Width;
    }

    public static void OpenChatWindow(DisplayedContactModel displayedContact)
    {
      if (displayedContact == null)
      {
        return;
      }
      UserModel user = new UserModel
      {
        Email = displayedContact.Name,
        FirstName = displayedContact.Name,
        LastName = displayedContact.Name,
        IsOnline = false
      };
      foreach (Window win in App.Current.Windows)
      {
        if (win != null)
          if (win.Tag != null && win.Tag.ToString() == user.Email + "Child")
          {
            win.Focus();
            return;
          }
      }
      Window child = new Window();
      child.Tag = user.Email + "Child";
      child.Title = Resources.ChatWindowTitle;
      var chatControl = new ChatControl();
      var chatViewModel = new ChatViewModel(child, displayedContact, chatControl.AutoScrollViewer);
      chatControl.DataContext = chatViewModel;
      child.Content = chatControl;
      child.SizeToContent = SizeToContent.WidthAndHeight;
      child.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/Chat.ico", UriKind.RelativeOrAbsolute));
      child.Show();
    }
  }
}
