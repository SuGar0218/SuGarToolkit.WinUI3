using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace SuGarToolkit.WinUI3.Controls.Windows;

/// <summary>
/// 类似 WPF 中的 <see href="https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.interop.hwndsourcehook">HwndSourceHook</see>
/// <br/>
/// 参照 CsWin32 生成的 <see cref="SUBCLASSPROC"/>
/// </summary>
/// <param name="hwnd"><see cref="HWND"/></param>
/// <param name="msg">窗口收到的消息</param>
/// <param name="wParam"><see cref="WPARAM"/></param>
/// <param name="lParam"><see cref="LPARAM"/></param>
/// <param name="handled">此消息是否已完成处理？若已完成处理，则直接将结果返回给操作系统，此前添加的子过程消息处理都不会被执行。</param>
/// <returns><see cref="LRESULT"/>消息处理结果，若没有完成处理（handled = false）请返回 0.</returns>
public delegate nint WindowSubclassProc(nint hwnd, uint msg, nuint wParam, nint lParam, ref bool handled);
