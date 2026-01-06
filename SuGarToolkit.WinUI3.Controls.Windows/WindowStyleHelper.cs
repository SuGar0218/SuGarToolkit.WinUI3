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
        var style = GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style |= WindowStyles.WS_MINIMIZEBOX;
        return SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

    public nint DisableMinimize()
    {
        _canMinimize = false;
        var style = GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style &= ~WindowStyles.WS_MINIMIZEBOX;
        return SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

    public nint EnableMaximize()
    {
        _canMaximize = true;
        var style = GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style |= WindowStyles.WS_MAXIMIZEBOX;
        return SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

    public nint DisableMaximize()
    {
        _canMaximize = false;
        var style = GetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        style &= ~WindowStyles.WS_MAXIMIZEBOX;
        return SetWindowLongPtr(_hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, style);
    }

#if SUGAR_TOOLKIT_X64
    private static nint GetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex) => PInvoke.GetWindowLongPtr(hWnd, nIndex);
    private static nint SetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, nint dwNewLong) => PInvoke.SetWindowLongPtr(hWnd, nIndex, dwNewLong);
#else
    private static int GetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex) => PInvoke.GetWindowLong(hWnd, nIndex);
    private static int SetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, int dwNewLong) => PInvoke.SetWindowLong(hWnd, nIndex, dwNewLong);
#endif
}
