using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class ContentWindowPage : Page
{
    public ContentWindowPage()
    {
        InitializeComponent();
    }

    private async void OnOpenNewWindowButtonClick(object sender, RoutedEventArgs e)
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
            window.OwnerAppWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId);
            await window.ShowDialogAsync();
        }
        else
        {
            window.Show();
        }
    }
}
