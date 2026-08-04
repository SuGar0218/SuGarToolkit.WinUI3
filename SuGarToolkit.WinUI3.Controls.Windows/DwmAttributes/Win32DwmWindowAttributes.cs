using System;
using System.Linq;

using Windows.UI;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dwm;

namespace SuGarToolkit.WinUI3.Controls.Windows;

public class Win32DwmWindowAttributes
{
    public static Color? GetBorderColor(nint hwnd) => GetDwmColorAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_BORDER_COLOR);
    public static void SetBorderColor(nint hwnd, Color? color) => SetDwmColorAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_BORDER_COLOR, color);

    public static Color? GetTitleBarColor(nint hwnd) => GetDwmColorAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR);
    public static void SetTitleBarColor(nint hwnd, Color? color) => SetDwmColorAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR, color);

    public static Color? GetTitleTextColor(nint hwnd) => GetDwmColorAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_TEXT_COLOR);
    public static void SetTitleTextColor(nint hwnd, Color? color) => SetDwmColorAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_TEXT_COLOR, color);

    public static bool GetIsDarkMode(nint hwnd) => GetDwmBoolAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE);
    public static void SetIsDarkMode(nint hwnd, bool enable) => SetDwmBoolAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, enable);

    public static WindowCornerRoundness GetCornerRoundness(nint hwnd) => (WindowCornerRoundness) GetDwmIntAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE);
    public static void SetCornerRoundness(nint hwnd, WindowCornerRoundness cornerRoundness) => SetDwmIntAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE, (int) cornerRoundness);

    public static WindowSystemBackdrop GetSystemBackdrop(nint hwnd) => (WindowSystemBackdrop) GetDwmIntAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE);
    public static void SetSystemBackdrop(nint hwnd, WindowSystemBackdrop systemBackdrop) => SetDwmIntAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, (int) systemBackdrop);

    private static Color? GetDwmColorAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute)
    {
        byte[] bytes = new byte[4];
        PInvoke.DwmGetWindowAttribute(new HWND(hwnd), attribute, bytes);
        if (bytes.All(b => b == byte.MaxValue))
            return null;

        return Color.FromArgb(byte.MaxValue, bytes[0], bytes[1], bytes[2]);
    }

    private static void SetDwmColorAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, Color? color)
    {
        if (color.HasValue)
        {
            Color value = color.Value;
            PInvoke.DwmSetWindowAttribute(new HWND(hwnd), attribute, [value.R, value.G, value.B, 0]);
        }
        else
        {
            PInvoke.DwmSetWindowAttribute(new HWND(hwnd), attribute, [byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue]);
        }
    }

    private static bool GetDwmBoolAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute)
    {
        return GetDwmIntAttribute(hwnd, attribute) != 0;
    }

    private static void SetDwmBoolAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, bool value)
    {
        SetDwmIntAttribute(hwnd, attribute, value ? 1 : 0);
    }

    private static int GetDwmIntAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute)
    {
        byte[] bytes = new byte[4];
        PInvoke.DwmGetWindowAttribute(new HWND(hwnd), attribute, bytes);
        return BitConverter.ToInt32(bytes);
    }

    private static void SetDwmIntAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, int value)
    {
        PInvoke.DwmSetWindowAttribute(new HWND(hwnd), attribute, BitConverter.GetBytes(value));
    }
}
