using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;

namespace SuGarToolkit.WinUI3.Helpers;

public class CapsuleShapeHelper
{
    public static void ShapeControlIntoCapsule(Control control)
    {
        control.CornerRadius = new CornerRadius(Math.Min(control.ActualWidth, control.ActualHeight) / 2);
    }

    public static bool GetShapeIntoCapsule(Control target) => (bool) target.GetValue(ShapeIntoCapsuleProperty);
    public static void SetShapeIntoCapsule(Control target, bool value) => target.SetValue(ShapeIntoCapsuleProperty, value);

    public static readonly DependencyProperty ShapeIntoCapsuleProperty = DependencyProperty.RegisterAttached(
        "ShapeIntoCapsule",
        typeof(bool),
        typeof(Control),
        new PropertyMetadata(default(bool), OnShapeIntoCapsuleChanged)
    );

    private static void OnShapeIntoCapsuleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Control target = (Control) d;
        if ((bool) e.NewValue)
        {
            ShapeControlIntoCapsule(target);
            target.SizeChanged += OnControlSizeChanged;
        }
        else
        {
            target.SizeChanged -= OnControlSizeChanged;
        }
    }

    private static void OnControlSizeChanged(object sender, SizeChangedEventArgs e)
    {
        Control target = (Control) sender;
        ShapeControlIntoCapsule(target);
    }
}
