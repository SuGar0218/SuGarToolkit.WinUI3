# SuGarToolkit for WinUI 3

## Features

### WindowedContentDialog and ContentDialogWindow

Show ContentDialog in a separate window.

<img width="447" height="246" alt="WindowedContentDialog" src="https://github.com/user-attachments/assets/4c6f616e-1776-4f9a-9fc2-e0355e63e683" />

### ExtendedContentDialog and UacStyleDialog

An extension version of ContentDialog, inspired by the Windows User Account Control window.

<img width="539" height="439" alt="ExtendedContentDialog" src="https://github.com/user-attachments/assets/b8cd5816-0c01-4baf-8471-dc775a0a70b3" />

### MessageBox with Fluent Design

![MessageBox](https://github.com/user-attachments/assets/a2c0ca11-f303-46aa-96de-5c9941e084a4)

### ContentWindow, derived from ContentControl

ContentWindow is somewhat like Window ([System.Windows.Window](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.window)) in WPF.

<img width="1918" height="1018" alt="ContentWindow" src="https://github.com/user-attachments/assets/f32e6a7a-026c-4c75-a781-0e7a02218223" />

## Build

To build for 64-bit system, please define constant `SUGAR_TOOLKIT_X64`. One feasible approach is adding the config below to csproj.

``` xml
<PropertyGroup Condition="'$(Platform)'=='x64'">
    <DefineConstants>$(DefineConstants);SUGAR_TOOLKIT_X64</DefineConstants>
</PropertyGroup>
```

Since this library references Windows APIs,

some only works on 64-bit system: `GetWindowLongPtr`, `SetWindowLongPtr`;

some are designed for 32-bit system: `GetWindowLong`, `SetWindowLong`;

there is code like this:

``` C#
#if SUGAR_TOOLKIT_X64
    private static nint GetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex) => PInvoke.GetWindowLongPtr(hWnd, nIndex);
    private static nint SetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, nint dwNewLong) => PInvoke.SetWindowLongPtr(hWnd, nIndex, dwNewLong);
#else
    private static int GetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex) => PInvoke.GetWindowLong(hWnd, nIndex);
    private static int SetWindowLongPtr(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, int dwNewLong) => PInvoke.SetWindowLong(hWnd, nIndex, dwNewLong);
#endif
```
