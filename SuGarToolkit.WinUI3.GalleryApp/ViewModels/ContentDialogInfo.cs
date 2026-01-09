using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.GalleryApp.ViewModels;

public partial class ContentDialogInfo : ObservableObject
{
    [ObservableProperty]
    public partial string? Header { get; set; } = "Header";

    [ObservableProperty]
    public partial string? PrimaryButtonText { get; set; } = "Primary";

    [ObservableProperty]
    public partial string? SecondaryButtonText { get; set; } = "Secondary";

    [ObservableProperty]
    public partial string? CloseButtonText { get; set; } = "Close";

    [ObservableProperty]
    public partial bool IsModal { get; set; }

    [ObservableProperty]
    public partial bool ShowInTaskbar { get; set; }

    [ObservableProperty]
    public partial Orientation Orientation { get; set; } = Orientation.Horizontal;

    [ObservableProperty]
    public partial ContentDialogResult Result { get; set; }

    public static List<Orientation> Orientations => field ??= [Orientation.Horizontal, Orientation.Vertical];
}
