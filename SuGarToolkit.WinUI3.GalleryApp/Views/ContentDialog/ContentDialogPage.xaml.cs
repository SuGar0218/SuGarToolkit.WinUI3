using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using SuGarToolkit.WinUI3.Controls.Dialogs;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

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
            ButtonOrientation = WindowedContentDialogInfo.Orientation,
            SystemBackdrop = new MicaBackdrop(),
            OwnerAppWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId)
        }
        .ShowAsync();
    }

    private async void OnShowContentDialogWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleContentDialogWindow window = new()
        {
            PrimaryButtonContent = ContentDialogWindowInfo.PrimaryButtonText,
            SecondaryButtonContent = ContentDialogWindowInfo.SecondaryButtonText,
            CloseButtonContent = ContentDialogWindowInfo.CloseButtonText,
            ButtonOrientation = ContentDialogWindowInfo.Orientation,
            ShowInTaskbar = ContentDialogWindowInfo.ShowInTaskbar,
            OwnerAppWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId)
        };
        if (ContentDialogWindowInfo.IsModal)
        {
            await window.ShowDialogAsync();
        }
        else
        {
            window.Show();
        }
    }

    private async void OnShowExtendedContentDialogWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleExtendedContentDialogWindow window = new()
        {
            PrimaryButtonContent = ExtendedContentDialogWindowInfo.PrimaryButtonText,
            SecondaryButtonContent = ExtendedContentDialogWindowInfo.SecondaryButtonText,
            CloseButtonContent = ExtendedContentDialogWindowInfo.CloseButtonText,
            ButtonOrientation = ExtendedContentDialogWindowInfo.Orientation,
            ShowInTaskbar = ExtendedContentDialogWindowInfo.ShowInTaskbar,
            OwnerAppWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId)
        };
        if (ExtendedContentDialogWindowInfo.IsModal)
        {
            await window.ShowDialogAsync();
        }
        else
        {
            window.Show();
        }
    }

    private async void OnShowUacDialogWindowButtonClick(object sender, RoutedEventArgs e)
    {
        SampleUacStyleDialogWindow window = new()
        {
            OwnerAppWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId),
            Severity = (InfoBarSeverity) PART_SeverityComboBox.SelectedItem
        };
        await window.ShowDialogAsync();
    }
}
