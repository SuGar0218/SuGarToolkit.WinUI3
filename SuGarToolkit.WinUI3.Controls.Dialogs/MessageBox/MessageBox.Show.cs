using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public partial class MessageBox
{
    public static Task<MessageBoxResult> Show(string message)
    {
        return Show(null, message, null, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
    }

    public static Task<MessageBoxResult> Show(string? message, string? title)
    {
        return Show(null, message, title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
    }

    public static Task<MessageBoxResult> Show(Window? owner, string message)
    {
        return Show(owner, message, null, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
    }

    public static Task<MessageBoxResult> Show(Window? owner, string? message, string? title)
    {
        return Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
    }

    public static Task<MessageBoxResult> Show(
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton defaultButton)
    {
        return Show(null, message, title, buttons, icon, defaultButton);
    }

    public static Task<MessageBoxResult> Show(
        Window? owner,
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton defaultButton)
    {
        return new MessageBox
        {
            Owner = owner,
            Title = title,
            Message = message,
            Buttons = buttons,
            Icon = icon,
            DefaultButton = defaultButton
        }
        .ShowAsync();
    }

    public static SystemBackdrop? SystemBackdrop { get; set; }
}
