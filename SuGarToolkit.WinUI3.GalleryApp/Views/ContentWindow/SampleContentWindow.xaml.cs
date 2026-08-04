using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class SampleContentWindow : ContentWindow
{
    public SampleContentWindow()
    {
        InitializeComponent();
    }

    private void OnColorPickerColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        OuterBorderColor = args.NewColor;
    }

    private void OnResetOuterBorderColorButtonClick(object sender, RoutedEventArgs e)
    {
        OuterBorderColor = null;
    }
}
