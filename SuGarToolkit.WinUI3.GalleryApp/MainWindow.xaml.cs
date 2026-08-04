using Microsoft.UI.Xaml.Media;

using SuGarToolkit.WinUI3.Controls.Dialogs;
using SuGarToolkit.WinUI3.Controls.Windows;
using SuGarToolkit.WinUI3.GalleryApp.ViewModels;
using SuGarToolkit.WinUI3.GalleryApp.Views;
using SuGarToolkit.WinUI3.GalleryApp.Views.HeaderBodyFooterView;
using SuGarToolkit.WinUI3.GalleryApp.Views.LiquidGlassLike;

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
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<TextBoxValidationPage>("TextBoxValidation"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<AdaptiveStackPanelPage>("AdaptiveStackPanel"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<LiquidGlassLikeInteractionPage>("LiquidGlassLikeInteraction"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<HeaderBodyFooterViewPage>("HeaderBodyFooterView"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<DwmWindowAttributesSamplePage>("DwmWindowAttributes"));
        PART_MainView.ViewModel.Pages.Add(PageViewModel.Create<TestPage>("Test"));
    }
}
