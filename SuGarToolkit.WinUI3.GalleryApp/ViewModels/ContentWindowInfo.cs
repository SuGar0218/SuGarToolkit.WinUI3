using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuGarToolkit.WinUI3.GalleryApp.ViewModels;

public partial class ContentWindowInfo : ObservableObject
{
    [ObservableProperty]
    public partial string Title { get; set; } = "Title";

    [ObservableProperty]
    public partial bool ExtendsContentIntoTitleBar { get; set; }

    [ObservableProperty]
    public partial double Width { get; set; } = 800;

    [ObservableProperty]
    public partial double MinWidth { get; set; } = 0;

    [ObservableProperty]
    public partial double MaxWidth { get; set; } = double.PositiveInfinity;

    [ObservableProperty]
    public partial double Height { get; set; } = 600;

    [ObservableProperty]
    public partial double MinHeight { get; set; } = 0;

    [ObservableProperty]
    public partial double MaxHeight { get; set; } = double.PositiveInfinity;

    [ObservableProperty]
    public partial bool ShowInTaskbar { get; set; } = true;

    [ObservableProperty]
    public partial bool CanMinimize { get; set; } = true;

    [ObservableProperty]
    public partial bool CanMaximize { get; set; } = true;

    [ObservableProperty]
    public partial bool CanResize { get; set; } = true;

    [ObservableProperty]
    public partial bool IsModal { get; set; }
}
