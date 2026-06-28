using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace SuGarToolkit.WinUI3.Layout;

public interface IHeaderBodyFooterView
{
    object? Header { get; set; }
    DataTemplate? HeaderTemplate { get; set; }
    DataTemplateSelector? HeaderTemplateSelector { get; set; }
    Brush? HeaderBackground { get; set; }
    Brush? HeaderSeparatorBrush { get; set; }
    double HeaderSeparatorThickness { get; set; }
    HorizontalAlignment HorizontalHeaderAlignment { get; set; }
    VerticalAlignment VerticalHeaderAlignment { get; set; }

    object? Content { get; set; }
    DataTemplate? ContentTemplate { get; set; }
    DataTemplateSelector ContentTemplateSelector { get; set; }
    Brush? ContentBackground { get; set; }

    object? Footer { get; set; }
    DataTemplate? FooterTemplate { get; set; }
    DataTemplateSelector? FooterTemplateSelector { get; set; }
    Brush? FooterBackground { get; set; }
    Brush? FooterSeparatorBrush { get; set; }
    double FooterSeparatorThickness { get; set; }
    HorizontalAlignment HorizontalFooterAlignment { get; set; }
    VerticalAlignment VerticalFooterAlignment { get; set; }
}
