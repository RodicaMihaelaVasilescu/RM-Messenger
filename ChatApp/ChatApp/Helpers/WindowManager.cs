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
      window.Top = desktopWorkingArea.Top;
      window.Height = desktopWorkingArea.Bottom;
      window.Width = window.Height / 2.35;
      window.Left = desktopWorkingArea.Right - window.Width;
    }
  }
}
