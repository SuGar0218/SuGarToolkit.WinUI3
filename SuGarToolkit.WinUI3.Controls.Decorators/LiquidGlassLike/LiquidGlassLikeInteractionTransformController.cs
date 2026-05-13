using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Controls.Decorators;

public class LiquidGlassLikeInteractionTransformController
{
    public LiquidGlassLikeInteractionTransformController(FrameworkElement target)
    {
        _target = target;
        _transformGroup = new TransformGroup
        {
            Children =
            [
                _scaleTransform,
                _translateTransform
            ]
        };
        _scaleTransfromResetXAnimation.EasingFunction = _easingFunction;
        _scaleTransfromResetYAnimation.EasingFunction = _easingFunction;
        _translateTransformResetXAnimation.EasingFunction = _easingFunction;
        _translateTransformResetYAnimation.EasingFunction = _easingFunction;
        Timeline[] timelines = [_scaleTransfromResetXAnimation,
            _scaleTransfromResetYAnimation,
            _translateTransformResetXAnimation,
            _translateTransformResetYAnimation];
        foreach (Timeline timeline in timelines)
        {
            _resetStoryboard.Children.Add(timeline);
        }
        _resetStoryboard.Completed += OnResetStoryboardCompleted;
        Storyboard.SetTarget(_scaleTransfromResetXAnimation, _scaleTransform);
        Storyboard.SetTargetProperty(_scaleTransfromResetXAnimation, nameof(ScaleTransform.ScaleX));
        Storyboard.SetTarget(_scaleTransfromResetYAnimation, _scaleTransform);
        Storyboard.SetTargetProperty(_scaleTransfromResetYAnimation, nameof(ScaleTransform.ScaleY));
        Storyboard.SetTarget(_translateTransformResetXAnimation, _translateTransform);
        Storyboard.SetTargetProperty(_translateTransformResetXAnimation, nameof(TranslateTransform.X));
        Storyboard.SetTarget(_translateTransformResetYAnimation, _translateTransform);
        Storyboard.SetTargetProperty(_translateTransformResetYAnimation, nameof(TranslateTransform.Y));
        ResetAnimationSeconds = 0.382;
    }

    private readonly FrameworkElement _target;
    private readonly TransformGroup _transformGroup;
    private readonly ScaleTransform _scaleTransform = new();
    private readonly DoubleAnimation _scaleTransfromResetXAnimation = new DoubleAnimation { To = 1 };
    private readonly DoubleAnimation _scaleTransfromResetYAnimation = new DoubleAnimation { To = 1 };
    private readonly TranslateTransform _translateTransform = new();
    private readonly DoubleAnimation _translateTransformResetXAnimation = new DoubleAnimation { To = 1 };
    private readonly DoubleAnimation _translateTransformResetYAnimation = new DoubleAnimation { To = 1 };
    private readonly EasingFunctionBase _easingFunction = new BackEase
    {
        EasingMode = EasingMode.EaseOut,
        Amplitude = 0.5
    };

    private readonly Storyboard _resetStoryboard = new Storyboard();

    public Transform Transform => _transformGroup;

    private Point _dragDelta;

    public Point DragDelta
    {
        get => _dragDelta;
        set
        {
            _dragDelta = value;
            OnDragDeltaChanged();
        }
    }

    public double ResetAnimationSeconds
    {
        get => field;
        set
        {
            field = value;
            OnResetAnimationSecondsChanged();
        }
    }

    public void Reset()
    {
        if (DragDelta.X == 0 && DragDelta.Y == 0)
            return;

        _dragDelta = new Point(0, 0);
        _resetStoryboard.FillBehavior = FillBehavior.HoldEnd;
        _resetStoryboard.Begin();
    }

    private void OnResetStoryboardCompleted(object? sender, object e)
    {
        _resetStoryboard.FillBehavior = FillBehavior.Stop;
        _scaleTransform.ScaleX = 1;
        _scaleTransform.ScaleY = 1;
        _translateTransform.X = 0;
        _translateTransform.Y = 0;
    }

    private void OnResetAnimationSecondsChanged()
    {
        _scaleTransfromResetXAnimation.Duration = TimeSpan.FromSeconds(ResetAnimationSeconds);
        _scaleTransfromResetYAnimation.Duration = TimeSpan.FromSeconds(ResetAnimationSeconds);
        _translateTransformResetXAnimation.Duration = TimeSpan.FromSeconds(ResetAnimationSeconds);
        _translateTransformResetYAnimation.Duration = TimeSpan.FromSeconds(ResetAnimationSeconds);
    }

    private void OnDragDeltaChanged()
    {
        RefreshScaleTranform();
        RefreshTranslateTransform();
    }

    private static readonly double k = 618;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RefreshScaleTranform()
    {
        double absDeltaX = Math.Abs(DragDelta.X);
        double absDeltaY = Math.Abs(DragDelta.Y);
        _scaleTransform.ScaleX = 2 - k / (absDeltaX + k) - 0.5 * absDeltaY / (absDeltaY + k);
        _scaleTransform.ScaleY = 2 - k / (absDeltaY + k) - 0.5 * absDeltaX / (absDeltaX + k);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RefreshTranslateTransform()
    {
        double absDeltaX = Math.Abs(DragDelta.X);
        double absDeltaY = Math.Abs(DragDelta.Y);
        _translateTransform.X = (_target.ActualWidth * 0.5 + 16) * absDeltaX / (absDeltaX + k);
        _translateTransform.Y = (_target.ActualHeight * 0.5 + 16) * absDeltaY / (absDeltaY + k);
        if (DragDelta.X < 0)
        {
            _translateTransform.X = -_translateTransform.X;
        }
        if (DragDelta.Y < 0)
        {
            _translateTransform.Y = -_translateTransform.Y;
        }
    }
}
