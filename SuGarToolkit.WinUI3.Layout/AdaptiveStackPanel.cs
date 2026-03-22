using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Layout;

public partial class AdaptiveStackPanel : Panel
{
    #region DependencyProperty

    public double HorizontalSpacing
    {
        get => (double) GetValue(HorizontalSpacingProperty);
        set => SetValue(HorizontalSpacingProperty, value);
    }

    public static readonly DependencyProperty HorizontalSpacingProperty = DependencyProperty.Register(
        nameof(HorizontalSpacing),
        typeof(double),
        typeof(AdaptiveStackPanel),
        new PropertyMetadata(default(double), InvalidateMeasureOnPropertyChanged)
    );

    public double VerticalSpacing
    {
        get => (double) GetValue(VerticalSpacingProperty);
        set => SetValue(VerticalSpacingProperty, value);
    }

    public static readonly DependencyProperty VerticalSpacingProperty = DependencyProperty.Register(
        nameof(VerticalSpacing),
        typeof(double),
        typeof(AdaptiveStackPanel),
        new PropertyMetadata(default(double), InvalidateMeasureOnPropertyChanged)
    );

    public Orientation PreferredOrientation
    {
        get => (Orientation) GetValue(PreferredOrientationProperty);
        set => SetValue(PreferredOrientationProperty, value);
    }

    public static readonly DependencyProperty PreferredOrientationProperty = DependencyProperty.Register(
        nameof(PreferredOrientation),
        typeof(Orientation),
        typeof(AdaptiveStackPanel),
        new PropertyMetadata(default(Orientation), InvalidateMeasureOnPropertyChanged)
    );

    private static void InvalidateMeasureOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AdaptiveStackPanel self = (AdaptiveStackPanel) d;
        self.InvalidateMeasure();
    }

    #endregion

    public Orientation ActualOrientation { get; private set; }

    protected override Size MeasureOverride(Size availableSize)
    {
        _visibleChildren = [.. Children.Where(child => child.Visibility == Visibility.Visible)];
        if (_visibleChildren.Count == 0)
            return ZeroSize;

        Size desiredSize;
        switch (PreferredOrientation)
        {
            case Orientation.Vertical:
                desiredSize = MeasureVertical(availableSize);
                if (desiredSize.Height <= availableSize.Height)
                {
                    ActualOrientation = Orientation.Vertical;
                }
                else
                {
                    desiredSize = MeasureHorizontal(availableSize);
                    ActualOrientation = Orientation.Horizontal;
                }
                return desiredSize;

            case Orientation.Horizontal:
                desiredSize = MeasureHorizontal(availableSize);
                if (desiredSize.Width <= availableSize.Width)
                {
                    ActualOrientation = Orientation.Horizontal;
                }
                else
                {
                    desiredSize = MeasureVertical(availableSize);
                    ActualOrientation = Orientation.Vertical;
                }
                return desiredSize;

            default:
                throw new InvalidOperationException(nameof(PreferredOrientation));
        }
    }

    /// <summary>
    /// Only be called in <see cref="MeasureOverride(Size)"/>
    /// </summary>
    private Size MeasureHorizontal(Size availableSize)
    {
        Size desiredSize = new();
        foreach (UIElement child in _visibleChildren!)
        {
            child.Measure(availableSize);
            desiredSize.Width += child.DesiredSize.Width;
            desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
        }
        desiredSize.Width += HorizontalSpacing * (_visibleChildren.Count - 1);
        return desiredSize;
    }

    /// <summary>
    /// Only be called in <see cref="MeasureOverride(Size)"/>
    /// </summary>
    private Size MeasureVertical(Size availableSize)
    {
        Size desiredSize = new();
        foreach (UIElement child in _visibleChildren!)
        {
            child.Measure(availableSize);
            desiredSize.Height += child.DesiredSize.Height;
            desiredSize.Width = Math.Max(desiredSize.Width, child.DesiredSize.Width);
        }
        desiredSize.Height += VerticalSpacing * (_visibleChildren.Count - 1);
        return desiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        _visibleChildren = [.. Children.Where(child => child.Visibility == Visibility.Visible)];
        if (_visibleChildren.Count == 0)
            return ZeroSize;

        return ActualOrientation switch
        {
            Orientation.Vertical => ArrangVertical(finalSize),
            Orientation.Horizontal => ArrangeHorizontal(finalSize),
            _ => throw new InvalidOperationException(nameof(PreferredOrientation)),
        };
    }

    /// <summary>
    /// Only be called in <see cref="ArrangeOverride(Size)"/>
    /// </summary>
    private Size ArrangeHorizontal(Size finalSize)
    {
        double x = 0;
        double height = 0;
        foreach (UIElement child in _visibleChildren!)
        {
            child.Arrange(new Rect(x, 0, child.DesiredSize.Width, Math.Min(child.DesiredSize.Height, finalSize.Height)));
            x += child.ActualSize.X;
            x += HorizontalSpacing;
            height = Math.Max(height, child.ActualSize.Y);
        }
        return new Size(x - HorizontalSpacing, height);
    }

    /// <summary>
    /// Only be called in <see cref="ArrangeOverride(Size)"/>
    /// </summary>
    private Size ArrangVertical(Size finalSize)
    {
        double y = 0;
        double width = 0;
        foreach (UIElement child in _visibleChildren!)
        {
            child.Arrange(new Rect(0, y, Math.Min(child.DesiredSize.Width, finalSize.Width), child.DesiredSize.Height));
            y += child.ActualSize.Y;
            y += VerticalSpacing;
            width = Math.Max(width, child.ActualSize.X);
        }
        return new Size(width, y - VerticalSpacing);
    }

    private List<UIElement>? _visibleChildren;

    private static readonly Size ZeroSize = new(0, 0);
}
