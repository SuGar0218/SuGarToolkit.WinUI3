using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace SuGarToolkit.WinUI3.GalleryApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public ObservableCollection<PageViewModel> Pages { get; private set; } = [];
}
