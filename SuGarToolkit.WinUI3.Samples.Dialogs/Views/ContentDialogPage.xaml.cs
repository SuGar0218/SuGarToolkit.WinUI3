using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using SuGarToolkit.WinUI3.Controls.Dialogs;
using SuGarToolkit.WinUI3.Controls.Windows;
using SuGarToolkit.WinUI3.Samples.Dialogs.ViewModels;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Foundation.Collections;

namespace SuGarToolkit.WinUI3.Samples.Dialogs.Views;

public sealed partial class ContentDialogPage : Page
{
    public ContentDialogPage()
    {
        InitializeComponent();
    }

    private async void OnShowWindowedContentDialogButtonClick(object sender, RoutedEventArgs e)
    {
        WindowedContentDialogInfo.Result = await new WindowedContentDialog
        {
            Header = WindowedContentDialogInfo.Header,
            Content = "Lorem ipsum sit amet.",
            PrimaryButtonContent = WindowedContentDialogInfo.PrimaryButtonText,
            SecondaryButtonContent = WindowedContentDialogInfo.SecondaryButtonText,
            CloseButtonContent = WindowedContentDialogInfo.CloseButtonText,
            Owner = ContentWindow.GetWindow(this)
        }
        .ShowAsync();
    }

    private void OnShowContentDialogWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleContentDialogWindow window = new()
        {
            PrimaryButtonContent = ContentDialogWindowInfo.PrimaryButtonText,
            SecondaryButtonContent = ContentDialogWindowInfo.SecondaryButtonText,
            CloseButtonContent = ContentDialogWindowInfo.CloseButtonText,
            Owner = ContentWindow.GetWindow(this)
        };
        window.ShowDialog();
    }

    private void OnShowExtendedContentDialogWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleExtendedContentDialogWindow window = new()
        {
            Owner = ContentWindow.GetWindow(this),
            PrimaryButtonContent = ExtendedContentDialogWindowInfo.PrimaryButtonText,
            SecondaryButtonContent = ExtendedContentDialogWindowInfo.SecondaryButtonText,
            CloseButtonContent = ExtendedContentDialogWindowInfo.CloseButtonText
        };
        window.ShowDialog();
    }

    private void OnShowUacDialogWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleUacStyleDialogWindow window = new()
        {
            Owner = ContentWindow.GetWindow(this),
            Severity = (InfoBarSeverity) PART_SeverityComboBox.SelectedItem
        };
        window.ShowDialog();
    }
}
