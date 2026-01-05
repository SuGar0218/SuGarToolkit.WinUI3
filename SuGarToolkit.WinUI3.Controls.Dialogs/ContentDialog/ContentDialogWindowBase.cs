using SuGarToolkit.WinUI3.Controls.Windows;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public partial class ContentDialogWindowBase : ContentWindow
{
    public ContentDialogWindowBase()
    {
        ExtendsContentIntoTitleBar = true;
        StartupLocation = WindowStartupLocation.CenterOwner;
        SizeToContent = true;
        CanMinimize = false;
        CanMaximize = false;
    }
}
