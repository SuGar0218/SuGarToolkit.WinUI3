using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using SuGarToolkit.WinUI3.Controls.Dialogs;
using SuGarToolkit.WinUI3.Controls.Windows;

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
            ButtonOrientation = ContentDialogWindowInfo.Orientation,
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
            CloseButtonContent = ExtendedContentDialogWindowInfo.CloseButtonText,
            ButtonOrientation = ExtendedContentDialogWindowInfo.Orientation
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
