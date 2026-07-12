using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System.Collections;
using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

[TemplatePart(Name = nameof(PART_CommandSpaceScrollViewer), Type = typeof(ScrollViewer))]
[TemplatePart(Name = nameof(PART_CommandSpaceItemsControl), Type = typeof(ItemsControl))]
public partial class ContentDialogEx : ContentControl
{
    public ContentDialogEx()
    {
        DefaultStyleKey = typeof(ContentDialogEx);
    }

    public object? Header
    {
        get => (object?) GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(ContentDialogEx),
        new PropertyMetadata(default(object?))
    );

    public DataTemplate? HeaderTemplate
    {
        get => (DataTemplate?) GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
        nameof(HeaderTemplate),
        typeof(DataTemplate),
        typeof(ContentDialogEx),
        new PropertyMetadata(default(DataTemplate?))
    );

    public DataTemplateSelector? HeaderTemplateSelector
    {
        get => (DataTemplateSelector?) GetValue(HeaderTemplateSelectorProperty);
        set => SetValue(HeaderTemplateSelectorProperty, value);
    }

    public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register(
        nameof(HeaderTemplateSelector),
        typeof(DataTemplateSelector),
        typeof(ContentDialogEx),
        new PropertyMetadata(default(DataTemplateSelector?))
    );

    public IEnumerable? ButtonItemsSource
    {
        get => (IEnumerable?) GetValue(ButtonItemsSourceProperty);
        set => SetValue(ButtonItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ButtonItemsSourceProperty = DependencyProperty.Register(
        nameof(ButtonItemsSource),
        typeof(IEnumerable),
        typeof(ContentDialogEx),
        new PropertyMetadata(default(IEnumerable?))
    );

    public IList<object> ButtonItems
    {
        get
        {
            if (PART_CommandSpaceItemsControl is not null)
                return PART_CommandSpaceItemsControl.Items;

            return _initItems ??= [];
        }
    }

    private List<object>? _initItems;

    private ItemsControl? PART_CommandSpaceItemsControl;
    private ScrollViewer? PART_CommandSpaceScrollViewer;

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        PART_CommandSpaceItemsControl = GetTemplateChild(nameof(PART_CommandSpaceItemsControl)) as ItemsControl;
        PART_CommandSpaceScrollViewer = GetTemplateChild(nameof(PART_CommandSpaceScrollViewer)) as ScrollViewer;
        if (PART_CommandSpaceItemsControl is not null && _initItems is not null)
        {
            foreach (object item in _initItems)
            {
                PART_CommandSpaceItemsControl.Items.Add(item);
            }
            _initItems = null;
        }
    }
}
