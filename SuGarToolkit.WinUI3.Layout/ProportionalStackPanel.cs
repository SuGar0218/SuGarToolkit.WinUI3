using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Layout;

public partial class ProportionalStackPanel : Panel
{
    public class ChildProportionCalculatingEventArgs
    {
        public Size PanelSize { get; set; }
        public double Proportion { get; set; } = 1;
    }

    public delegate void ChildProportionCalculatingHandler(ProportionalStackPanel sender, ChildProportionCalculatingEventArgs args);

    public event ChildProportionCalculatingHandler? ChildMeasuringProportionCalculating;
    public event ChildProportionCalculatingHandler? ChildArrangingProportionCalculating;

    public Orientation Orientation
    {
        get => (Orientation) GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(ProportionalStackPanel),
        new PropertyMetadata(default(Orientation))
    );

    //protected override Size MeasureOverride(Size availableSize)
    //{
    //    _visibleChildrenProportions.Clear();
    //    foreach (UIElement child in Children.Where(child => child.Visibility == Visibility.Visible))
    //    {
    //        _visibleChildrenProportions.Add(child, 1);
    //    }
    //    if (_visibleChildrenProportions.Count == 0)
    //    {
    //        return ZeroSize;
    //    }
    //}

    private void MeasureHorizontal(Size availableSize)
    {
        //foreach (UIElement child in _visibleChildrenProportions!)
        //{
        //    double pro
        //}
    }

    private void MeasureVertical(Size availableSize)
    {
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        return base.ArrangeOverride(finalSize);
    }

    private readonly Dictionary<UIElement, double> _visibleChildrenProportions = [];

    private static readonly Size ZeroSize = new(0, 0);
}
