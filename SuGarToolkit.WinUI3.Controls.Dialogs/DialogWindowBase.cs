using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public partial class DialogWindowBase : ContentWindow
{
    public DialogWindowBase()
    {
        ExtendsContentIntoTitleBar = true;
        StartupLocation = WindowStartupLocation.CenterOwner;
        SizeToContent = true;
        CanMinimize = false;
        CanMaximize = false;
        ShowInTaskbar = false;
    }
}
