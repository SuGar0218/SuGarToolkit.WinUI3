namespace SuGarToolkit.WinUI3.Controls.Validations;

/// <summary>
/// Similar to <see cref="Microsoft.UI.Xaml.Data.UpdateSourceTrigger"/>.
/// <br/>
/// <see href="https://learn.microsoft.com/zh-cn/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.updatesourcetrigger"/>
/// </summary>
public enum ValidationTrigger
{
    Default,
    PropertyChanged,
    LostFocus,
    Explicit
}
