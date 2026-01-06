using CommunityToolkit.Mvvm.ComponentModel;

using SuGarToolkit.WinUI3.Controls.Dialogs;

using System;
using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.GalleryApp.ViewModels;

public partial class MessageBoxInfo : ObservableObject
{
    [ObservableProperty]
    public partial string? Title { get; set; } = "Title";

    [ObservableProperty]
    public partial string? Message { get; set; } = "Message";

    [ObservableProperty]
    public partial MessageBoxButtons Buttons { get; set; }

    [ObservableProperty]
    public partial MessageBoxIcon Icon { get; set; }

    [ObservableProperty]
    public partial MessageBoxDefaultButton DefaultButton { get; set; }

    [ObservableProperty]
    public partial MessageBoxResult Result { get; set; }

    public static List<MessageBoxButtons> MessageBoxButtons => field ??= [.. Enum.GetValues<MessageBoxButtons>()];
    public static List<MessageBoxIcon> MessageBoxIcons => field ??= [.. Enum.GetValues<MessageBoxIcon>()];
    public static List<MessageBoxDefaultButton> MessageBoxDefaultButtons => field ??= [.. Enum.GetValues<MessageBoxDefaultButton>()];
}
