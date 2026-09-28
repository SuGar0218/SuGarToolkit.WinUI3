using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SuGarToolkit.WinUI3.Controls;

[TemplateVisualState(GroupName = nameof(VisualStates.SelectionStates), Name = nameof(VisualStates.SelectionStates.Selected))]
[TemplateVisualState(GroupName = nameof(VisualStates.SelectionStates), Name = nameof(VisualStates.SelectionStates.Unselected))]
public partial class SegmentedSelectorItem : Button
{
    public SegmentedSelectorItem()
    {
        DefaultStyleKey = typeof(SegmentedSelectorItem);
    }

    public bool IsSelected
    {
        get => (bool) GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected),
        typeof(bool),
        typeof(SegmentedSelectorItem),
        new PropertyMetadata(default(bool), OnIsSelectedChanged)
    );

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
        {
            VisualStateManager.GoToState((Control) d, VisualStates.SelectionStates.Selected, useTransitions: false);
        }
        else
        {
            VisualStateManager.GoToState((Control) d, VisualStates.SelectionStates.Unselected, useTransitions: false);
        }
    }

    protected static class VisualStates
    {
        public static class CommonStates
        {
            public const string Normal = nameof(Normal);
            public const string PointerOver = nameof(PointerOver);
            public const string Pressed = nameof(Pressed);
        }

        public static class SelectionStates
        {
            public const string Selected = nameof(Selected);
            public const string Unselected = nameof(Unselected);
        }
    }
}
