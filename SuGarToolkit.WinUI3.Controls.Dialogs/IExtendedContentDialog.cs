using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

public interface IExtendedContentDialog : IContentDialog
{
    object? ExtendedHeader { get; set; }
    DataTemplate? ExtendedHeaderTemplate { get; set; }
}
