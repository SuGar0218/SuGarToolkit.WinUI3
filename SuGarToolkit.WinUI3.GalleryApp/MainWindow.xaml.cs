using Microsoft.UI.Xaml.Media;

using SuGarToolkit.WinUI3.Controls.Dialogs;
using SuGarToolkit.WinUI3.Controls.Windows;
using SuGarToolkit.WinUI3.GalleryApp.ViewModels;
using SuGarToolkit.WinUI3.GalleryApp.Views;

namespace SuGarToolkit.WinUI3.GalleryApp;

public sealed partial class MainWindow : ContentWindow
{
    public MainWindow()
    {
        InitializeComponent();
        MessageBox.SystemBackdrop = new MicaBackdrop();
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<ContentDialogPage>("ContentDialog"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<MessageBoxPage>("MessageBox"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<ContentWindowPage>("ContentWindow"));
    }
}
