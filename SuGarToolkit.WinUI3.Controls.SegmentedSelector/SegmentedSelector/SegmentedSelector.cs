using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Animation;

using System;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Controls;

[TemplatePart(Name = nameof(PART_SliderThumb), Type = typeof(Thumb))]
[TemplatePart(Name = nameof(PART_SliderHost), Type = typeof(Canvas))]
[TemplatePart(Name = nameof(PART_ItemsHost), Type = typeof(ItemsPresenter))]
public partial class SegmentedSelector : ListViewBase
{
    public SegmentedSelector()
    {
        DefaultStyleKey = typeof(SegmentedSelector);

        SelectionChanged += OnSelectionChanged;
        SizeChanged += OnSizeChanged;
    }

    private readonly Storyboard _selectionChangingStoryboard = new() { FillBehavior = FillBehavior.Stop };
    private readonly DoubleAnimation _sliderThumbWidthAnimation = new();
    private readonly DoubleAnimation _sliderThumbCanvasLeftAnimation = new();
    private readonly EasingFunctionBase _easingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut };

    private Thumb PART_SliderThumb;
    private Canvas PART_SliderHost;
    private ItemsPresenter PART_ItemsHost;
    private bool _isPrivateSelecting;

    private SegmentedSelectorItem SelectedItemContainer
    {
        get;
        set
        {
            if (field == value)
                return;

            field?.IsHitTestVisible = true;
            field = value;
            field?.IsHitTestVisible = false;
        }
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        PART_SliderThumb = GetTemplateChild(nameof(PART_SliderThumb)) as Thumb;
        PART_SliderHost = GetTemplateChild(nameof(PART_SliderHost)) as Canvas;
        PART_ItemsHost = GetTemplateChild(nameof(PART_ItemsHost)) as ItemsPresenter;

        if (PART_SliderThumb is not null && PART_SliderHost is not null)
        {
            PART_SliderThumb.DragDelta += OnSliderThumbDragDelta;
            PART_SliderThumb.DragCompleted += OnSliderThumbDragCompleted;

            _sliderThumbWidthAnimation.EasingFunction = _easingFunction;
            _sliderThumbCanvasLeftAnimation.EasingFunction = _easingFunction;
            Storyboard.SetTarget(_sliderThumbWidthAnimation, PART_SliderThumb);
            Storyboard.SetTargetProperty(_sliderThumbWidthAnimation, "Width");
            Storyboard.SetTarget(_sliderThumbCanvasLeftAnimation, PART_SliderThumb);
            Storyboard.SetTargetProperty(_sliderThumbCanvasLeftAnimation, "(Canvas.Left)");
            _selectionChangingStoryboard.Children.Add(_sliderThumbWidthAnimation);
            _selectionChangingStoryboard.Children.Add(_sliderThumbCanvasLeftAnimation);
            foreach (Timeline timeline in _selectionChangingStoryboard.Children)
            {
                timeline.Duration = TimeSpan.FromSeconds(0.382);
            }
        }
    }

    private void OnSliderThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        SelectedItemContainer?.IsSelected = false;
        double left = Canvas.GetLeft(PART_SliderThumb);
        if (double.IsNaN(left))
        {
            left = 0;
        }
        left += e.HorizontalChange;
        left = Math.Clamp(left, 0, PART_SliderHost.ActualWidth - PART_SliderThumb.ActualWidth);
        Canvas.SetLeft(PART_SliderThumb, left);
    }

    private void OnSliderThumbDragCompleted(object sender, DragCompletedEventArgs e)
    {
        Point thumbPosition = PART_SliderThumb.TransformToVisual(this).TransformPoint(new Point(0, 0));
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

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);

        SegmentedSelectorItem container = (SegmentedSelectorItem) element;
        container.Click += OnItemContainerClick;
    }

    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        base.ClearContainerForItemOverride(element, item);

        SegmentedSelectorItem container = (SegmentedSelectorItem) element;
        container.Click -= OnItemContainerClick;
    }

    private void OnItemContainerClick(object sender, RoutedEventArgs e)
    {
        SegmentedSelectorItem container = (SegmentedSelectorItem) sender;
        SelectedItem = ItemContainerGenerator.ItemFromContainer(container);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedItemContainer = (SegmentedSelectorItem) ItemContainerGenerator.ContainerFromItem(SelectedItem);
        if (e.RemovedItems is not null && e.RemovedItems.Count > 0)
        {
            foreach (object item in e.RemovedItems)
            {
                SegmentedSelectorItem container = (SegmentedSelectorItem) ItemContainerGenerator.ContainerFromItem(item);
                container?.IsSelected = false;
            }
        }
        if (e.AddedItems is not null && e.AddedItems.Count > 0)
        {
            foreach (object item in e.AddedItems)
            {
                SegmentedSelectorItem container = (SegmentedSelectorItem) ItemContainerGenerator.ContainerFromItem(item);
                container?.IsSelected = true;
            }
        }
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
        if (SelectedItem is null || PART_SliderThumb is null || PART_SliderHost is null)
            return;

        Point selectedItemContainerPosition = SelectedItemContainer.TransformToVisual(PART_SliderHost).TransformPoint(new Point(0, 0));
        PART_SliderThumb.BorderThickness = SelectedItemContainer.BorderThickness;
        PART_SliderThumb.Height = SelectedItemContainer.ActualHeight;
        _sliderThumbWidthAnimation.From = PART_SliderThumb.ActualWidth;
        _sliderThumbWidthAnimation.To = SelectedItemContainer.ActualWidth;
        double fromLeft = Canvas.GetLeft(PART_SliderThumb);
        double toLeft = selectedItemContainerPosition.X;
        if (double.IsNaN(fromLeft))
        {
            fromLeft = 0;
        }
        _sliderThumbCanvasLeftAnimation.From = fromLeft;
        _sliderThumbCanvasLeftAnimation.To = toLeft;
        _selectionChangingStoryboard.Begin();
        SelectedItemContainer.IsSelected = true;
        PART_SliderThumb.Width = SelectedItemContainer.ActualWidth;
        Canvas.SetLeft(PART_SliderThumb, toLeft);
    }
}
