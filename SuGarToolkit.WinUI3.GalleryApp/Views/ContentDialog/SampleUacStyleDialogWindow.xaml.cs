using SuGarToolkit.WinUI3.Controls.Dialogs;

using System;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class SampleUacStyleDialogWindow : UacStyleDialogWindow
{
    public SampleUacStyleDialogWindow()
    {
        InitializeComponent();
    }

    private void UacStyleDialogWindow_PrimaryButtonClick(object sender, EventArgs e)
    {
        TryClose();
    }

    private void UacStyleDialogWindow_CloseButtonClick(object sender, EventArgs e)
    {
        TryClose();
    }
}
