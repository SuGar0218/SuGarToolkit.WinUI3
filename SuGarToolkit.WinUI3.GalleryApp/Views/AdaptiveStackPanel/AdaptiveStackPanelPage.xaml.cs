using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class AdaptiveStackPanelPage : Page
{
    public AdaptiveStackPanelPage()
    {
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        PART_AdaptiveStackPanel.PreferredOrientation = (Orientation) ((ComboBox) sender).SelectedItem;
    }

    public static readonly int[] Orientations = [0, 1];
}
