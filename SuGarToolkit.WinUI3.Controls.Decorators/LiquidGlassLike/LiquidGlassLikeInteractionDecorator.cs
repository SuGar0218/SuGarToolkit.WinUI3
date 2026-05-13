using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

using System;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Controls.Decorators;

public sealed partial class LiquidGlassLikeInteractionDecorator : ContentControl
{
    public LiquidGlassLikeInteractionDecorator()
    {
        DefaultStyleKey = typeof(LiquidGlassLikeInteractionDecorator);
        _controller = new LiquidGlassLikeInteractionTransformController(this);
        RenderTransform = _controller.Transform;
        RenderTransformOrigin = new Point(0.5, 0.5);
        AddHandler(PointerPressedEvent, new PointerEventHandler(OnPointerPressed), handledEventsToo: true);
        AddHandler(PointerMovedEvent, new PointerEventHandler(OnPointerMoved), handledEventsToo: true);
        AddHandler(PointerReleasedEvent, new PointerEventHandler(OnPointerReleased), handledEventsToo: true);
    }

    private readonly LiquidGlassLikeInteractionTransformController _controller;

    private bool _isPointerPressed;
    private Point _pointerPressedPosition;

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        _isPointerPressed = true;
        _pointerPressedPosition = e.GetCurrentPoint(this).Position;
    }

    private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
        if (!_isPointerPressed)
            return;

        Point currentPosition = e.GetCurrentPoint(this).Position;
        Point dragDelta = new(currentPosition.X - _pointerPressedPosition.X, currentPosition.Y - _pointerPressedPosition.Y);
        if (Math.Abs(dragDelta.X) <= double.Epsilon || Math.Abs(dragDelta.Y) <= double.Epsilon)
            return;

        CapturePointer(e.Pointer);
        _controller.DragDelta = dragDelta;
    }

    private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
        ReleasePointerCapture(e.Pointer);
        _controller.Reset();
        _isPointerPressed = false;
    }
}
