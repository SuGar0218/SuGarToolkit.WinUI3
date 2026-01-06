using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SuGarToolkit.WinUI3.Controls.Dialogs;
using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class MessageBoxPage : Page
{
    public MessageBoxPage()
    {
        InitializeComponent();
    }

    private async void OnShowMessageBoxButtonClick(object sender, RoutedEventArgs e)
    {
        messageBoxInfo.Result = await MessageBox.Show(ContentWindow.GetWindow(this), messageBoxInfo.Message, messageBoxInfo.Title, messageBoxInfo.Buttons, messageBoxInfo.Icon, messageBoxInfo.DefaultButton);
    }
}
