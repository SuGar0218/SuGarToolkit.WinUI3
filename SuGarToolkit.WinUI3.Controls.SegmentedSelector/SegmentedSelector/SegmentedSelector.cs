using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Animation;

using System;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Controls;

/// <remarks>
/// Although it is based on <see cref="ListViewBase"/>, for <see cref="ListViewBase.SelectionMode"/>, only <see cref="SelectionMode.Single"/> is supported.
/// </remarks>
[TemplatePart(Name = nameof(PART_SliderBlock), Type = typeof(SegmentedSelectorSliderBlock))]
[TemplatePart(Name = nameof(PART_SliderBlockHost), Type = typeof(Canvas))]
[TemplatePart(Name = nameof(PART_SliderThumb), Type = typeof(Thumb))]
[TemplatePart(Name = nameof(PART_SliderThumbHost), Type = typeof(Canvas))]
[TemplatePart(Name = nameof(PART_ItemsHost), Type = typeof(ItemsPresenter))]
public partial class SegmentedSelector : ListViewBase
{
    public SegmentedSelector()
    {
        DefaultStyleKey = typeof(SegmentedSelector);

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        SelectionChanged += OnSelectionChanged;
        SizeChanged += OnSizeChanged;
        RegisterPropertyChangedCallback(SelectionModeProperty, OnSelectionModeChanged);
    }

    private readonly Storyboard _selectionChangingStoryboard = new() { FillBehavior = FillBehavior.Stop };
    private readonly DoubleAnimation _sliderBlockWidthAnimation = new();
    private readonly DoubleAnimation _sliderBlockCanvasLeftAnimation = new();
    private readonly EasingFunctionBase _easingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut };

    private SegmentedSelectorSliderBlock? PART_SliderBlock;
    private Canvas? PART_SliderBlockHost;
    private Thumb? PART_SliderThumb;
    private Canvas? PART_SliderThumbHost;
    private ItemsPresenter? PART_ItemsHost;
    private SegmentedSelectorItem? _previousItemContainer;
    private SegmentedSelectorItem? _selectedItemContainer;
    private bool _isPrivateSelecting;
    private SegmentedSelectorSliderThumbPointerStateManager? _thumbPointerStateManager;

    private SegmentedSelectorSliderThumbPointerStateManager CreateThumbPointerStateManager(Thumb thumb)
    {
        SegmentedSelectorSliderThumbPointerStateManager thumbPointerStateManager = new(thumb);
        thumbPointerStateManager.StateChanged += OnThumbPointerStateManagerStateChanged;
        return thumbPointerStateManager;
    }

    private void OnThumbPointerStateManagerStateChanged(object? sender, EventArgs e)
    {
        if (PART_SliderBlock is null)
            return;

        SegmentedSelectorSliderThumbPointerStateManager thumbPointerStateManager = (SegmentedSelectorSliderThumbPointerStateManager) sender!;
        switch (thumbPointerStateManager.State)
        {
            case SegmentedSelectorSliderThumbPointerState.Normal:
                PART_SliderBlock.State = SegmentedSelectorSliderBlockState.Normal;
                break;
            case SegmentedSelectorSliderThumbPointerState.PointerOver:
                PART_SliderBlock.State = SegmentedSelectorSliderBlockState.PointerOver;
                break;
            case SegmentedSelectorSliderThumbPointerState.Dragging:
                PART_SliderBlock.State = SegmentedSelectorSliderBlockState.Pressed;
                break;
            default:
                break;
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_thumbPointerStateManager is null && PART_SliderThumb is not null)
        {
            _thumbPointerStateManager = CreateThumbPointerStateManager(PART_SliderThumb);
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _thumbPointerStateManager?.Dispose();
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        PART_SliderBlock = GetTemplateChild(nameof(PART_SliderBlock)) as SegmentedSelectorSliderBlock;
        PART_SliderBlockHost = GetTemplateChild(nameof(PART_SliderBlockHost)) as Canvas;
        PART_SliderThumb = GetTemplateChild(nameof(PART_SliderThumb)) as Thumb;
        PART_SliderThumbHost = GetTemplateChild(nameof(PART_SliderThumbHost)) as Canvas;
        PART_ItemsHost = GetTemplateChild(nameof(PART_ItemsHost)) as ItemsPresenter;

        if (PART_SliderThumb is not null && PART_SliderThumbHost is not null)
        {
            PART_SliderThumb.DragDelta += OnSliderThumbDragDelta;
            PART_SliderThumb.DragCompleted += OnSliderThumbDragCompleted;
            _thumbPointerStateManager ??= CreateThumbPointerStateManager(PART_SliderThumb);
        }
        if (PART_SliderBlock is not null && PART_SliderBlockHost is not null)
        {
            _sliderBlockWidthAnimation.EasingFunction = _easingFunction;
            _sliderBlockCanvasLeftAnimation.EasingFunction = _easingFunction;
            Storyboard.SetTarget(_sliderBlockWidthAnimation, PART_SliderBlock);
            Storyboard.SetTargetProperty(_sliderBlockWidthAnimation, "Width");
            Storyboard.SetTarget(_sliderBlockCanvasLeftAnimation, PART_SliderBlock);
            Storyboard.SetTargetProperty(_sliderBlockCanvasLeftAnimation, "(Canvas.Left)");
            _selectionChangingStoryboard.Children.Add(_sliderBlockWidthAnimation);
            _selectionChangingStoryboard.Children.Add(_sliderBlockCanvasLeftAnimation);
            foreach (Timeline timeline in _selectionChangingStoryboard.Children)
            {
                timeline.Duration = TimeSpan.FromSeconds(0.382);
            }
        }
        UpdateSliderVisibility();
    }

    /// <summary>
    /// Canvas.Left of PART_SliderThumb after dragging.
    /// </summary>
    /// <remarks>
    /// It can be out of the range of PART_SliderThumbHost, but will be constrained in <see cref="SlideThumbToSelectedItem"/>.
    /// </remarks>
    private double _thumbDragLeft;

    private void OnSliderThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        _thumbDragLeft += e.HorizontalChange;
        double left = Math.Clamp(_thumbDragLeft, 0, Math.Abs(PART_SliderThumbHost!.ActualWidth - PART_SliderThumb!.ActualWidth));
        SetSliderPropertyValue(Canvas.LeftProperty, left);
    }

    private void OnSliderThumbDragCompleted(object sender, DragCompletedEventArgs e)
    {
        Point thumbPosition = PART_SliderThumb!.TransformToVisual(this).TransformPoint(new Point(0, 0));
        double minDistance = double.MaxValue;
        int selectedIndex = SelectedIndex;
        for (int i = 0; i < Items.Count; i++)
        {
            UIElement container = (UIElement) ItemContainerGenerator.ContainerFromIndex(i);
            Point containerPosition = container.TransformToVisual(this).TransformPoint(new Point(0, 0));
            double distance = Math.Abs(containerPosition.X - thumbPosition.X);
            if (distance < minDistance)
            {
                minDistance = distance;
                selectedIndex = i;
            }
        }
        _isPrivateSelecting = true;
        SelectedIndex = selectedIndex;
        _isPrivateSelecting = false;
        SlideThumbToSelectedItem();
    }

    protected override bool IsItemItsOwnContainerOverride(object item) => item is SegmentedSelectorItem;

    protected override DependencyObject GetContainerForItemOverride() => new SegmentedSelectorItem();

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _previousItemContainer = _selectedItemContainer;
        _selectedItemContainer = (SegmentedSelectorItem) ItemContainerGenerator.ContainerFromItem(SelectedItem);
        UpdateSliderVisibility();
        if (!_isPrivateSelecting)
        {
            DispatcherQueue.TryEnqueue(SlideThumbToSelectedItem);
        }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        SlideThumbToSelectedItem();
    }

    private void SlideThumbToSelectedItem()
    {
        if (_selectedItemContainer is null || SelectedItem is null || PART_SliderBlock is null || PART_SliderThumb is null || PART_SliderThumbHost is null)
            return;

        Point selectedItemContainerPosition = _selectedItemContainer.TransformToVisual(PART_SliderThumbHost).TransformPoint(new Point(0, 0));
        _sliderBlockWidthAnimation.From = PART_SliderThumb.ActualWidth;
        _sliderBlockWidthAnimation.To = _selectedItemContainer.ActualWidth;
        double fromLeft = Canvas.GetLeft(PART_SliderThumb);
        double toLeft = selectedItemContainerPosition.X;
        _thumbDragLeft = toLeft;
        if (double.IsNaN(fromLeft))
        {
            fromLeft = 0;
        }
        _sliderBlockCanvasLeftAnimation.From = fromLeft;
        _sliderBlockCanvasLeftAnimation.To = toLeft;
        if (_previousItemContainer is not null)
        {
            _selectionChangingStoryboard.Begin();
        }
        _selectedItemContainer.IsSelected = true;
        SetSliderPropertyValue(FrameworkElement.WidthProperty, _selectedItemContainer.ActualWidth);
        SetSliderPropertyValue(FrameworkElement.HeightProperty, _selectedItemContainer.ActualHeight);
        SetSliderPropertyValue(Canvas.LeftProperty, toLeft);
    }

    private void UpdateSliderVisibility()
    {
        Visibility visibility = SelectedItem is null ? Visibility.Collapsed : Visibility.Visible;
        SetSliderPropertyValue(UIElement.VisibilityProperty, visibility);
    }

    private void SetSliderPropertyValue(DependencyProperty property, object value)
    {
        PART_SliderThumb?.SetValue(property, value);
        PART_SliderBlock?.SetValue(property, value);
    }

    private void OnSelectionModeChanged(DependencyObject sender, DependencyProperty dp)
    {
        if (SelectionMode != ListViewSelectionMode.Single)
        {
            SelectionMode = ListViewSelectionMode.Single;
        }
    }
}
