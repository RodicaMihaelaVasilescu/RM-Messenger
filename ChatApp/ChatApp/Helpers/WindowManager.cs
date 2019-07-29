﻿using System;
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
            var controlAssembly = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name);
            var controlType = controlAssembly.GetType(controlPath);
            var newControl = Activator.CreateInstance(controlType) as UserControl;
            newControl.DataContext = viewModel;
            window.Content = newControl;
        }
  }
}