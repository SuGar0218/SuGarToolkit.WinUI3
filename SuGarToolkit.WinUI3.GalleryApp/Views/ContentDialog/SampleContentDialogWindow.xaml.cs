using SuGarToolkit.WinUI3.Controls.Dialogs;

using System;


namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class SampleContentDialogWindow : ContentDialogWindow
{
    public SampleContentDialogWindow()
    {
        InitializeComponent();
    }

    private void ContentDialogWindow_PrimaryButtonClick(object sender, EventArgs e)
    {
        MessageBox.Show(Window, "Primary button is clicked.", "Primary");
    }

    private void ContentDialogWindow_SecondaryButtonClick(object sender, EventArgs e)
    {
        MessageBox.Show(Window, "Secondary button is clicked.", "Secondary");
    }

    private async void ContentDialogWindow_CloseButtonClick(object sender, EventArgs e)
    {
        MessageBoxResult result = await MessageBox.Show(Window, "Close button is clicked.", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        if (result == MessageBoxResult.Yes)
        {
            TryClose();
        }
    }
}
