using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

namespace SuGarToolkit.WinUI3.Samples.Dialogs.ViewModels;

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
    public partial ContentDialogResult Result { get; set; }
}
