using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

using System;
using System.Threading.Tasks;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public partial class MessageBox
{
    public MessageBox()
    {
        _window = new DialogWindowBase();
        _view = new MessageBoxView();
        _taskCompletionSource = new TaskCompletionSource<MessageBoxResult>();
        _view.Loaded += OnViewLoaded;
        _window.SystemBackdrop = SystemBackdrop;
        _window.Content = _view;
        _window.Closed += OnWindowClosed;
        _view.ResultChanged += OnResultChanged;
    }

    public string? Title
    {
        get => _view.Title;
        set => _view.Title = value;
    }

    public string? Message
    {
        get => _view.Message;
        set => _view.Message = value;
    }

    public MessageBoxIcon Icon
    {
        get => _view.Icon;
        set => _view.Icon = value;
    }

    public MessageBoxButtons Buttons
    {
        get => _view.Buttons;
        set => _view.Buttons = value;
    }

    public MessageBoxDefaultButton DefaultButton
    {
        get => _view.DefaultButton;
        set => _view.DefaultButton = value;
    }

    //public SystemBackdrop? SystemBackdrop
    //{
    //    get => _window.SystemBackdrop;
    //    set => _window.SystemBackdrop = value;
    //}

    public Window? Owner
    {
        get => _window.Owner;
        set => _window.Owner = value;
    }

    private readonly DialogWindowBase _window;
    private readonly MessageBoxView _view;
    private readonly TaskCompletionSource<MessageBoxResult> _taskCompletionSource;

    public Task<MessageBoxResult> ShowAsync()
    {
        _window.ShowDialog();
        return _taskCompletionSource.Task;
    }

    private void OnViewLoaded(object sender, RoutedEventArgs e)
    {
        _view.MaxWidth = double.PositiveInfinity;
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        _taskCompletionSource.SetResult(_view.Result);
    }

    private void OnResultChanged(object? sender, EventArgs e)
    {
        _window.TryClose();
    }
}
