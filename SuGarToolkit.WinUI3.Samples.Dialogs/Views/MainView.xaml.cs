using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using SuGarToolkit.WinUI3.Samples.Dialogs.ViewModels;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

namespace SuGarToolkit.WinUI3.Samples.Dialogs.Views;

public sealed partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void OnNavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is PageViewModel pageViewModel)
        {
            PART_Frame.Navigate(pageViewModel.PageType);
        }
    }
}
