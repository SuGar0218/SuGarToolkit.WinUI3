using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

using SuGarToolkit.WinUI3.Controls.Dialogs;
using SuGarToolkit.WinUI3.Controls.Windows;
using SuGarToolkit.WinUI3.Samples.Dialogs.ViewModels;
using SuGarToolkit.WinUI3.Samples.Dialogs.Views;

namespace SuGarToolkit.WinUI3.Samples.Dialogs;

public sealed partial class MainWindow : ContentWindow
{
    public MainWindow()
    {
        InitializeComponent();
        MessageBox.SystemBackdrop = new MicaBackdrop();
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<ContentDialogPage>("ContentDialog"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<MessageBoxPage>("MessageBox"));
    }
}
