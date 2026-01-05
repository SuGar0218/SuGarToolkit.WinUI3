using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public interface IContentDialogInteraction
{
    Task<ContentDialogResult> ShowAsync();
}
