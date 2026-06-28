using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace SuGarToolkit.WinUI3.Layout
{
    [TemplatePart(Name = nameof(PART_RootGrid), Type = typeof(HeaderBodyFooterView))]
    public partial class HeaderBodyFooterView
    {
        public Orientation Orientation
        {
            get => (Orientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(Orientation.Vertical, (d, e) => ((HeaderBodyFooterView) d).OnOrientationChanged(e))
        );

        private void OnOrientationChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateRootGridLayout();
        }

        public Brush? ContentBackground
        {
            get => (Brush?) GetValue(ContentBackgroundProperty);
            set => SetValue(ContentBackgroundProperty, value);
        }

        public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register(
            nameof(ContentBackground),
            typeof(Brush),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(Brush))
        );

        public object? Header
        {
            get => (object?) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(object))
        );

        public DataTemplate? HeaderTemplate
        {
            get => (DataTemplate?) GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            nameof(HeaderTemplate),
            typeof(DataTemplate),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(DataTemplate))
        );

        public DataTemplateSelector? HeaderTemplateSelector
        {
            get => (DataTemplateSelector?) GetValue(HeaderTemplateSelectorProperty);
            set => SetValue(HeaderTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register(
            nameof(HeaderTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(DataTemplateSelector))
        );

        public Brush? HeaderBackground
        {
            get => (Brush?) GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderBackgroundProperty, value);
        }

        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register(
            nameof(HeaderBackground),
            typeof(Brush),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(Brush))
        );

        public Brush? HeaderForeground
        {
            get => (Brush?) GetValue(HeaderForegroundProperty);
            set => SetValue(HeaderForegroundProperty, value);
        }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register(
            nameof(HeaderForeground),
            typeof(Brush),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(Brush))
        );

        public Brush? HeaderSeparatorBrush
        {
            get => (Brush?) GetValue(HeaderSeparatorBrushProperty);
            set => SetValue(HeaderSeparatorBrushProperty, value);
        }

        public static readonly DependencyProperty HeaderSeparatorBrushProperty = DependencyProperty.Register(
            nameof(HeaderSeparatorBrush),
            typeof(Brush),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(Brush?))
        );

        public double HeaderSeparatorThickness
        {
            get => (double) GetValue(HeaderSeparatorThicknessProperty);
            set => SetValue(HeaderSeparatorThicknessProperty, value);
        }

        public static readonly DependencyProperty HeaderSeparatorThicknessProperty = DependencyProperty.Register(
            nameof(HeaderSeparatorThickness),
            typeof(double),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(double))
        );

        public HorizontalAlignment HorizontalHeaderAlignment
        {
            get => (HorizontalAlignment) GetValue(HorizontalHeaderAlignmentProperty);
            set => SetValue(HorizontalHeaderAlignmentProperty, value);
        }

        public static readonly DependencyProperty HorizontalHeaderAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalHeaderAlignment),
            typeof(HorizontalAlignment),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(HorizontalAlignment))
        );

        public VerticalAlignment VerticalHeaderAlignment
        {
            get => (VerticalAlignment) GetValue(VerticalHeaderAlignmentProperty);
            set => SetValue(VerticalHeaderAlignmentProperty, value);
        }

        public static readonly DependencyProperty VerticalHeaderAlignmentProperty = DependencyProperty.Register(
            nameof(VerticalHeaderAlignment),
            typeof(VerticalAlignment),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(VerticalAlignment))
        );

        public object? Footer
        {
            get => (object?) GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
            nameof(Footer),
            typeof(object),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(object))
        );

        public DataTemplate? FooterTemplate
        {
            get => (DataTemplate?) GetValue(FooterTemplateProperty);
            set => SetValue(FooterTemplateProperty, value);
        }

        public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register(
            nameof(FooterTemplate),
            typeof(DataTemplate),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(DataTemplate))
        );

        public DataTemplateSelector? FooterTemplateSelector
        {
            get => (DataTemplateSelector?) GetValue(FooterTemplateSelectorProperty);
            set => SetValue(FooterTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty FooterTemplateSelectorProperty = DependencyProperty.Register(
            nameof(FooterTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(DataTemplateSelector))
        );

        public Brush? FooterBackground
        {
            get => (Brush?) GetValue(FooterBackgroundProperty);
            set => SetValue(FooterBackgroundProperty, value);
        }

        public Brush? FooterSeparatorBrush
        {
            get => (Brush?) GetValue(FooterSeparatorBrushProperty);
            set => SetValue(FooterSeparatorBrushProperty, value);
        }

        public double FooterSeparatorThickness
        {
            get => (double) GetValue(FooterSeparatorThicknessProperty);
            set => SetValue(FooterSeparatorThicknessProperty, value);
        }

        public static readonly DependencyProperty FooterSeparatorThicknessProperty = DependencyProperty.Register(
            nameof(FooterSeparatorThickness),
            typeof(double),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(double))
        );

        public static readonly DependencyProperty FooterSeparatorBrushProperty = DependencyProperty.Register(
            nameof(FooterSeparatorBrush),
            typeof(Brush),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(Brush?))
        );

        public static readonly DependencyProperty FooterBackgroundProperty = DependencyProperty.Register(
            nameof(FooterBackground),
            typeof(Brush),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(Brush))
        );

        public HorizontalAlignment HorizontalFooterAlignment
        {
            get => (HorizontalAlignment) GetValue(HorizontalFooterAlignmentProperty);
            set => SetValue(HorizontalFooterAlignmentProperty, value);
        }

        public static readonly DependencyProperty HorizontalFooterAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalFooterAlignment),
            typeof(HorizontalAlignment),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(HorizontalAlignment))
        );

        public VerticalAlignment VerticalFooterAlignment
        {
            get => (VerticalAlignment) GetValue(VerticalFooterAlignmentProperty);
            set => SetValue(VerticalFooterAlignmentProperty, value);
        }

        public static readonly DependencyProperty VerticalFooterAlignmentProperty = DependencyProperty.Register(
            nameof(VerticalFooterAlignment),
            typeof(VerticalAlignment),
            typeof(HeaderBodyFooterView),
            new PropertyMetadata(default(VerticalAlignment))
        );
    }
}
