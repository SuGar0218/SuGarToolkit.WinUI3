using Microsoft.UI.Xaml;

using Windows.UI;

namespace SuGarToolkit.WinUI3.Controls.Windows;

public static class WindowDwmAttributesExtensions
{
    extension(Window window)
    {
        public Color? BorderColor() => Win32DwmWindowAttributes.GetBorderColor(window.Handle());
        public void BorderColor(Color? color) => Win32DwmWindowAttributes.SetBorderColor(window.Handle(), color);

        public Color? TitleBarColor() => Win32DwmWindowAttributes.GetTitleBarColor(window.Handle());
        public void TitleBarColor(Color? color) => Win32DwmWindowAttributes.SetTitleBarColor(window.Handle(), color);

        public Color? TitleTextColor() => Win32DwmWindowAttributes.GetTitleTextColor(window.Handle());
        public void TitleTextColor(Color? color) => Win32DwmWindowAttributes.SetTitleTextColor(window.Handle(), color);

        public WindowSystemBackdrop DwmSystemBackdrop() => Win32DwmWindowAttributes.GetSystemBackdrop(window.Handle());
        public void DwmSystemBackdrop(WindowSystemBackdrop systemBackdrop) => Win32DwmWindowAttributes.SetSystemBackdrop(window.Handle(), systemBackdrop);

        public WindowCornerRoundness CornerRoundness() => Win32DwmWindowAttributes.GetCornerRoundness(window.Handle());
        public void CornerRoundness(WindowCornerRoundness cornerRoundness) => Win32DwmWindowAttributes.SetCornerRoundness(window.Handle(), cornerRoundness);

        public bool AutoDarkMode() => Win32DwmWindowAttributes.GetIsDarkMode(window.Handle());
        public void IsDarkMode(bool enable) => Win32DwmWindowAttributes.SetIsDarkMode(window.Handle(), enable);

        private nint Handle() => WinRT.Interop.WindowNative.GetWindowHandle(window);
    }
}
