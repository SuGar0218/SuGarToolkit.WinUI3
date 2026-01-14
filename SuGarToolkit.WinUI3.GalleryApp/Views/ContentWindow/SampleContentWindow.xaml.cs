using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class SampleContentWindow : ContentWindow
{
    public SampleContentWindow()
    {
        InitializeComponent();
        Closed += OnClosed;
    }

    private void OnClosed(object? sender, System.EventArgs e)
    {
    }
}
