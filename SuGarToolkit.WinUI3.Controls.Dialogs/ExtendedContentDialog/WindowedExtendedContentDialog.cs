using Microsoft.UI.Xaml.Controls;

using System;
using System.Threading.Tasks;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public partial class WindowedExtendedContentDialog : ExtendedContentDialogWindow, IExtendedContentDialog, IContentDialogInteraction
{
    public WindowedExtendedContentDialog()
    {
        PrimaryButtonClick += OnPrimaryButtonClick;
        SecondaryButtonClick += OnSecondaryButtonClick;
        CloseButtonClick += OnCloseButtonClick;
        Closed += OnClosed;
    }

    private TaskCompletionSource<ContentDialogResult>? _taskCompletionSource;

    public Task<ContentDialogResult> ShowAsync()
    {
        _taskCompletionSource = new TaskCompletionSource<ContentDialogResult>();
        ShowDialog();
        return _taskCompletionSource.Task;
    }

    private void OnPrimaryButtonClick(object? sender, EventArgs e)
    {
        TryClose();
    }

    private void OnSecondaryButtonClick(object? sender, EventArgs e)
    {
        TryClose();
    }

    private void OnCloseButtonClick(object? sender, EventArgs e)
    {
        TryClose();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        _taskCompletionSource?.SetResult(Result);
    }
}
