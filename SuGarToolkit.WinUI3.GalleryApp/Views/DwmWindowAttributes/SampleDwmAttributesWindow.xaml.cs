using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class SampleDwmAttributesWindow : Window
{
    public SampleDwmAttributesWindow()
    {
        InitializeComponent();
        this.DwmSystemBackdrop(WindowSystemBackdrop.Acrylic);
    }

    private void OnTitleBarColorPickerColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        this.TitleBarColor(args.NewColor);
    }

    private void OnTitleTextColorPickerColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        this.TitleTextColor(args.NewColor);
    }

    private void OnWindowBorderColorPickerColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        this.BorderColor(args.NewColor);
    }

    private void OnTitleBarResetButtonClick(object sender, RoutedEventArgs e)
    {
        this.TitleBarColor(null);
    }

    private void OnTitleTextResetButtonClick(object sender, RoutedEventArgs e)
    {
        this.TitleTextColor(null);
    }

    private void OnWindowBorderColorResetButtonClick(object sender, RoutedEventArgs e)
    {
        this.BorderColor(null);
    }

    private void OnAutoDarkModeCheckBoxChecked(object sender, RoutedEventArgs e)
    {
        this.IsDarkMode(true);
    }

    private void OnAutoDarkModeCheckBoxUnchecked(object sender, RoutedEventArgs e)
    {
        this.IsDarkMode(false);
    }

    private void OnWindowCornerRoundnessSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems is not null && e.AddedItems[0] is FrameworkElement frameworkElement && frameworkElement.DataContext is WindowCornerRoundness cornerRoundness)
        {
            this.CornerRoundness(cornerRoundness);
        }
    }

    private void StackPanel_Holding(object sender, Microsoft.UI.Xaml.Input.HoldingRoutedEventArgs e)
    {

    }
}
