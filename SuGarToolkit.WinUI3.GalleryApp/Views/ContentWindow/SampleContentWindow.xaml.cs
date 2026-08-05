using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SuGarToolkit.WinUI3.Controls.Windows;

using System;

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

    private void OnCornerRoundnessComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems is not null && e.AddedItems[0] is WindowCornerRoundness cornerRoundness)
        {
            CornerRoundness = cornerRoundness;
        }
    }

    private WindowCornerRoundness[] CornerRoundnesses { get; } = Enum.GetValues<WindowCornerRoundness>();
}
