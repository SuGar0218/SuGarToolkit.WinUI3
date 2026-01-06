using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SuGarToolkit.WinUI3.Controls.Windows;

internal class WindowStyleHelper
{
    public WindowStyleHelper(HWND hwnd)
    {
        _hwnd = hwnd;
    }

    private readonly HWND _hwnd;

    private bool _canMinimize;
    private bool _canMaximize;

    public bool CanMinimize
    {
        get => _canMinimize;
        set
        {
            if (_canMinimize == value)
                return;

            if (value)
            {
                EnableMinimize();
            }
            else
            {
                DisableMinimize();
            }
        }
    }

    public bool CanMaximize
    {
        get => _canMaximize;
        set
        {
            if (_canMaximize == value)
                return;

            if (value)
            {
                EnableMaximize();
            }
            else
            {
                DisableMaximize();
            }
        }
    }

    public nint EnableMinimize()
    {
        _canMinimize = true;
        nint style = PInvokeNative.GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style |= WindowStyles.WS_MINIMIZEBOX;
        return PInvokeNative.SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

    public nint DisableMinimize()
    {
        _canMinimize = false;
        nint style = PInvokeNative.GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style &= ~WindowStyles.WS_MINIMIZEBOX;
        return PInvokeNative.SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

    public nint EnableMaximize()
    {
        _canMaximize = true;
        nint style = PInvokeNative.GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style |= WindowStyles.WS_MAXIMIZEBOX;
        return PInvokeNative.SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

    public nint DisableMaximize()
    {
        _canMaximize = false;
        nint style = PInvokeNative.GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style &= ~WindowStyles.WS_MAXIMIZEBOX;
        return PInvokeNative.SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }
}
