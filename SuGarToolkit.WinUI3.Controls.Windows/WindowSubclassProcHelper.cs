using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace SuGarToolkit.WinUI3.Controls.Windows;

internal class WindowSubclassProcHelper
{
	public WindowSubclassProcHelper(HWND hwnd)
	{
		_hwnd = hwnd;
		_coreSubclassProc = CoreSubclassProc;
		_userSubclassProcs = [];
		PInvoke.SetWindowSubclass(_hwnd, _coreSubclassProc, 142857, 0);
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
}
