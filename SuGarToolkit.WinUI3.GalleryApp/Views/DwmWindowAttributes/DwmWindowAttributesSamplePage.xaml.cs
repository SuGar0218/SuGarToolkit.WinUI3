using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class DwmWindowAttributesSamplePage : Page
{
    public DwmWindowAttributesSamplePage()
    {
        InitializeComponent();
    }

    private void OnCustomizeWindowColorButtonClick(object sender, RoutedEventArgs e)
    {
        new SampleDwmAttributesWindow().Activate();
    }
}
