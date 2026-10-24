using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;

namespace SuGarToolkit.WinUI3.Controls;

[TemplateVisualState(GroupName = "CommonStates", Name = VisualStates.Normal)]
[TemplateVisualState(GroupName = "CommonStates", Name = VisualStates.PointerOver)]
[TemplateVisualState(GroupName = "CommonStates", Name = VisualStates.Pressed)]
[TemplateVisualState(GroupName = "CommonStates", Name = VisualStates.Disabled)]
public partial class SegmentedSelectorSliderBlock : Control
{
    public SegmentedSelectorSliderBlock()
    {
        DefaultStyleKey = typeof(SegmentedSelectorSliderBlock);
        IsEnabledChanged += OnIsEnabledChanged;
    }

    public SegmentedSelectorSliderBlockState State
    {
        get => (SegmentedSelectorSliderBlockState) GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        nameof(State),
        typeof(SegmentedSelectorSliderBlockState),
        typeof(SegmentedSelectorSliderBlock),
        new PropertyMetadata(default(SegmentedSelectorSliderBlockState), (d, e) => ((SegmentedSelectorSliderBlock) d).OnStateChanged(e))
    );

    private void OnStateChanged(DependencyPropertyChangedEventArgs e)
    {
        UpdateVisualState();
    }

    private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, VisualStates.Disabled, useTransitions: true);
            return;
        }
        switch (State)
        {
            case SegmentedSelectorSliderBlockState.Normal:
                VisualStateManager.GoToState(this, VisualStates.Normal, useTransitions: true);
                break;
            case SegmentedSelectorSliderBlockState.PointerOver:
                VisualStateManager.GoToState(this, VisualStates.PointerOver, useTransitions: true);
                break;
            case SegmentedSelectorSliderBlockState.Pressed:
                VisualStateManager.GoToState(this, VisualStates.Pressed, useTransitions: true);
                break;
            default:
                throw new InvalidOperationException(nameof(State) + " is not valid value of " + nameof(SegmentedSelectorSliderBlockState));
        }
    }

    private static class VisualStates
    {
        public const string Normal = nameof(Normal);
        public const string PointerOver = nameof(PointerOver);
        public const string Pressed = nameof(Pressed);
        public const string Disabled = nameof(Disabled);
    }
}
