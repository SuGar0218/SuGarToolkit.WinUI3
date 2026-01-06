using System;
using System.Runtime.InteropServices;

using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SuGarToolkit.WinUI3.Controls.Windows;

internal static partial class PInvokeNative
{
    public static nint GetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex)
    {
        if (Environment.Is64BitOperatingSystem)
        {
            return GetWindowLong64(hWnd, (int) nIndex);
        }
        else
        {
            return GetWindowLong32(hWnd, (int) nIndex);
        }
    }

    public static nint SetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, nint dwNewLong)
    {
        if (Environment.Is64BitOperatingSystem)
        {
            return SetWindowLong64(hWnd, (int) nIndex, dwNewLong);
        }
        else
        {
            return SetWindowLong32(hWnd, (int) nIndex, dwNewLong);
        }
    }

    [LibraryImport("User32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static partial nint GetWindowLong64(nint hWnd, int nIndex);

    [LibraryImport("User32.dll", EntryPoint = "GetWindowLongW")]
    private static partial nint GetWindowLong32(nint hWnd, int nIndex);

    [LibraryImport("User32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static partial nint SetWindowLong64(nint hWnd, int nIndex, nint dwNewLong);

    [LibraryImport("User32.dll", EntryPoint = "SetWindowLongW")]
    private static partial nint SetWindowLong32(nint hWnd, int nIndex, nint dwNewLong);
}
