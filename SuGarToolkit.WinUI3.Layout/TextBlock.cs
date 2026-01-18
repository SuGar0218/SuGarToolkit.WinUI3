using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

using Windows.Foundation;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.Win32;

namespace SuGarToolkit.WinUI3.Layout;

/// <summary>
/// <see cref="Microsoft.UI.Xaml.Controls.TextBlock"/> with some specific text may have a smaller DesiredSize.
/// </summary>
[ContentProperty(Name = nameof(Text))]
[TemplatePart(Name = nameof(PART_TextBlock), Type = typeof(Microsoft.UI.Xaml.Controls.TextBlock))]
public partial class TextBlock : Control
{
    public TextBlock()
    {
        DefaultStyleKey = typeof(TextBlock);
    }

    #region DependencyProperty

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(TextBlock),
        new PropertyMetadata(string.Empty)
    );

    public TextWrapping TextWrapping
    {
        get => (TextWrapping) GetValue(TextWrappingProperty);
        set => SetValue(TextWrappingProperty, value);
    }

    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
        nameof(TextWrapping),
        typeof(TextWrapping),
        typeof(TextBlock),
        new PropertyMetadata(default(TextWrapping))
    );

    public TextAlignment TextAlignment
    {
        get => (TextAlignment) GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
        nameof(TextAlignment),
        typeof(TextAlignment),
        typeof(TextBlock),
        new PropertyMetadata(default(TextAlignment))
    );

    public TextTrimming TextTrimming
    {
        get => (TextTrimming) GetValue(TextTrimmingProperty);
        set => SetValue(TextTrimmingProperty, value);
    }

    public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register(
        nameof(TextTrimming),
        typeof(TextTrimming),
        typeof(TextBlock),
        new PropertyMetadata(default(TextTrimming))
    );

    public TextDecorations TextDecorations
    {
        get => (TextDecorations) GetValue(TextDecorationsProperty);
        set => SetValue(TextDecorationsProperty, value);
    }

    public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register(
        nameof(TextDecorations),
        typeof(TextDecorations),
        typeof(TextBlock),
        new PropertyMetadata(default(TextDecorations))
    );

    public TextLineBounds TextLineBounds
    {
        get => (TextLineBounds) GetValue(TextLineBoundsProperty);
        set => SetValue(TextLineBoundsProperty, value);
    }

    public static readonly DependencyProperty TextLineBoundsProperty = DependencyProperty.Register(
        nameof(TextLineBounds),
        typeof(TextLineBounds),
        typeof(TextBlock),
        new PropertyMetadata(default(TextLineBounds))
    );

    public TextReadingOrder TextReadingOrder
    {
        get => (TextReadingOrder) GetValue(TextReadingOrderProperty);
        set => SetValue(TextReadingOrderProperty, value);
    }

    public static readonly DependencyProperty TextReadingOrderProperty = DependencyProperty.Register(
        nameof(TextReadingOrder),
        typeof(TextReadingOrder),
        typeof(TextBlock),
        new PropertyMetadata(default(TextReadingOrder))
    );

    #endregion

    private Microsoft.UI.Xaml.Controls.TextBlock? PART_TextBlock;

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        PART_TextBlock = (Microsoft.UI.Xaml.Controls.TextBlock) GetTemplateChild(nameof(PART_TextBlock));
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        Size desiredSize = base.MeasureOverride(availableSize);
        double textScale = _uiSettings.TextScaleFactor;
        return new Size(desiredSize.Width + textScale * 2, desiredSize.Height);
    }

    private double DpiScale => IsLoaded ? XamlRoot.RasterizationScale : (double) PInvoke.GetDpiForSystem() / 96;

    private readonly UISettings _uiSettings = new();
}
