using System;
using System.Collections.Generic;

using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace SuGarToolkit.WinUI3.Controls.Windows;

internal partial class WindowSubclassProcHelper : IDisposable
{
    public WindowSubclassProcHelper(HWND hwnd)
    {
        _hwnd = hwnd;
        _coreSubclassProc = CoreSubclassProc;
        _userSubclassProcs = [];
        PInvoke.SetWindowSubclass(_hwnd, _coreSubclassProc, _coreSubclassProcId, 0);
    }

    private readonly HWND _hwnd;
    private readonly SUBCLASSPROC _coreSubclassProc;
    private readonly List<WindowSubclassProc> _userSubclassProcs;

    public void AddSubclassProc(WindowSubclassProc proc)
    {
        _userSubclassProcs.Add(proc);
    }

    public void RemoveSubclassProc(WindowSubclassProc proc)
    {
        _userSubclassProcs.Remove(proc);
    }

    private LRESULT CoreSubclassProc(HWND hWnd, uint uMsg, WPARAM wParam, LPARAM lParam, nuint uIdSubclass, nuint dwRefData)
    {
        bool handled = false;
        foreach (WindowSubclassProc proc in _userSubclassProcs)
        {
            nint result = proc.Invoke(hWnd, uMsg, wParam, lParam, ref handled);
            if (handled)
                return new LRESULT(result);
        }
        return PInvoke.DefSubclassProc(hWnd, uMsg, wParam, lParam);
    }

    private const nuint _coreSubclassProcId = 142857;

    #region 通过释放模式实现 IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
                PInvoke.RemoveWindowSubclass(_hwnd, _coreSubclassProc, _coreSubclassProcId);
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            // TODO: 将大型字段设置为 null
            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~WindowSubclassProcHelper()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
