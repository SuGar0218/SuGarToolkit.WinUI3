using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class ContentWindowPage : Page
{
    public ContentWindowPage()
    {
        InitializeComponent();
    }

    private void OnOpenNewWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleContentWindow window = new()
        {
            Title = ContentWindowInfo.Title,
            Width = ContentWindowInfo.Width,
            Height = ContentWindowInfo.Height,
            MinWidth = ContentWindowInfo.MinWidth,
            MinHeight = ContentWindowInfo.MinHeight,
            MaxWidth = ContentWindowInfo.MaxWidth,
            MaxHeight = ContentWindowInfo.MaxHeight,
            ShowInTaskbar = ContentWindowInfo.ShowInTaskbar,
            CanMinimize = ContentWindowInfo.CanMinimize,
            CanMaximize = ContentWindowInfo.CanMaximize,
            CanResize = ContentWindowInfo.CanResize
        };
        if (ContentWindowInfo.IsModal)
        {
            window.Owner = ContentWindow.GetWindow(this);
            window.ShowDialog();
        }
        else
        {
            window.Show();
        }
    }
}
