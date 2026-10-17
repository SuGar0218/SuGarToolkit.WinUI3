using Microsoft.UI.Xaml.Controls;

using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class SegmentedSelectorPage : Page
{
    public SegmentedSelectorPage()
    {
        InitializeComponent();
    }

    private List<string> Items { get; } = ["Lorem", "Ipsum", "Sit", "Amet"];
}
