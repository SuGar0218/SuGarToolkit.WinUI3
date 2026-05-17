using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

using System;
using System.Runtime.CompilerServices;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Controls.Decorators;

public class LiquidGlassLikeInteractionTransformController
{
    public LiquidGlassLikeInteractionTransformController(FrameworkElement target)
    {
        _target = target;
        _target.SizeChanged += OnTargetSizeChanged;
        _transformGroup = new TransformGroup
        {
            Children =
            [
                _pressTranform,
                _scaleTransform,
                _translateTransform
            ]
        };
        _scaleTransfromResetXAnimation.EasingFunction = _easingFunction;
        _scaleTransfromResetYAnimation.EasingFunction = _easingFunction;
        _translateTransformResetXAnimation.EasingFunction = _easingFunction;
        _translateTransformResetYAnimation.EasingFunction = _easingFunction;
        _pressTranformResetXAnimation.EasingFunction = _easingFunction;
        _pressTranformResetYAnimation.EasingFunction = _easingFunction;
        _pressTranformXAnimation.EasingFunction = _easingFunction;
        _pressTranformYAnimation.EasingFunction = _easingFunction;

        foreach (Timeline timeline in new Timeline[]
        {
            _scaleTransfromResetXAnimation,
            _scaleTransfromResetYAnimation,
            _translateTransformResetXAnimation,
            _translateTransformResetYAnimation,
            _pressTranformResetXAnimation,
            _pressTranformResetYAnimation
        })
        {
            _resetStoryboard.Children.Add(timeline);
        }
        _resetStoryboard.FillBehavior = FillBehavior.Stop;
        Storyboard.SetTarget(_scaleTransfromResetXAnimation, _scaleTransform);
        Storyboard.SetTargetProperty(_scaleTransfromResetXAnimation, nameof(ScaleTransform.ScaleX));
        Storyboard.SetTarget(_scaleTransfromResetYAnimation, _scaleTransform);
        Storyboard.SetTargetProperty(_scaleTransfromResetYAnimation, nameof(ScaleTransform.ScaleY));
        Storyboard.SetTarget(_translateTransformResetXAnimation, _translateTransform);
        Storyboard.SetTargetProperty(_translateTransformResetXAnimation, nameof(TranslateTransform.X));
        Storyboard.SetTarget(_translateTransformResetYAnimation, _translateTransform);
        Storyboard.SetTargetProperty(_translateTransformResetYAnimation, nameof(TranslateTransform.Y));
        Storyboard.SetTarget(_pressTranformResetXAnimation, _pressTranform);
        Storyboard.SetTargetProperty(_pressTranformResetXAnimation, nameof(ScaleTransform.ScaleX));
        Storyboard.SetTarget(_pressTranformResetYAnimation, _pressTranform);
        Storyboard.SetTargetProperty(_pressTranformResetYAnimation, nameof(ScaleTransform.ScaleY));

        foreach (Timeline timeline in new Timeline[]
        {
            _pressTranformXAnimation,
            _pressTranformYAnimation
        })
        {
            _pressStoryboard.Children.Add(timeline);
        }
        Storyboard.SetTarget(_pressTranformXAnimation, _pressTranform);
        Storyboard.SetTargetProperty(_pressTranformXAnimation, nameof(ScaleTransform.ScaleX));
        Storyboard.SetTarget(_pressTranformYAnimation, _pressTranform);
        Storyboard.SetTargetProperty(_pressTranformYAnimation, nameof(ScaleTransform.ScaleY));

        ResetAnimationSeconds = 0.382;
        ExpandAnimationSeconds = 0.25;
    }

    private readonly FrameworkElement _target;
    private readonly TransformGroup _transformGroup;

    private readonly ScaleTransform _scaleTransform = new();
    private readonly DoubleAnimation _scaleTransfromResetXAnimation = new() { To = 1 };
    private readonly DoubleAnimation _scaleTransfromResetYAnimation = new() { To = 1 };

    private readonly TranslateTransform _translateTransform = new();
    private readonly DoubleAnimation _translateTransformResetXAnimation = new() { To = 0 };
    private readonly DoubleAnimation _translateTransformResetYAnimation = new() { To = 0 };

    private readonly ScaleTransform _pressTranform = new();
    private readonly DoubleAnimation _pressTranformXAnimation = new();
    private readonly DoubleAnimation _pressTranformYAnimation = new();
    private readonly DoubleAnimation _pressTranformResetXAnimation = new() { To = 1 };
    private readonly DoubleAnimation _pressTranformResetYAnimation = new() { To = 1 };

    private readonly EasingFunctionBase _easingFunction = new BackEase
    {
        EasingMode = EasingMode.EaseOut,
        Amplitude = 0.5
    };

    private readonly Storyboard _pressStoryboard = new();
    private readonly Storyboard _resetStoryboard = new();
    private readonly LiquidGlassLikeStretchCalculator _calculator = new();

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

    public double ExpandAnimationSeconds
    {
        get => field;
        set
        {
            field = value;
            OnExpandAnimationSecondsChanged();
        }
    }

    public void Begin()
    {
        _pressTranformXAnimation.To = _calculator.ExpandScale;
        _pressTranformYAnimation.To = _calculator.ExpandScale;
        _pressStoryboard.Begin();
    }

    public void Reset()
    {
        _dragDelta = new Point(0, 0);
        _resetStoryboard.Begin();
        _scaleTransform.ScaleX = 1;
        _scaleTransform.ScaleY = 1;
        _translateTransform.X = 0;
        _translateTransform.Y = 0;
    }

    private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e)
    {
        _calculator.OriginalSize = e.NewSize;
    }

    private void OnResetAnimationSecondsChanged()
    {
        TimeSpan duration = TimeSpan.FromSeconds(ResetAnimationSeconds);
        _scaleTransfromResetXAnimation.Duration = duration;
        _scaleTransfromResetYAnimation.Duration = duration;
        _translateTransformResetXAnimation.Duration = duration;
        _translateTransformResetYAnimation.Duration = duration;
        _pressTranformResetXAnimation.Duration = duration;
        _pressTranformResetYAnimation.Duration = duration;
    }

    private void OnExpandAnimationSecondsChanged()
    {
        TimeSpan duration = TimeSpan.FromSeconds(ExpandAnimationSeconds);
        _pressTranformXAnimation.Duration = duration;
        _pressTranformYAnimation.Duration = duration;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnDragDeltaChanged()
    {
        _calculator.DragDelta = DragDelta;
        _calculator.Calculate();
        RefreshScaleTranform();
        RefreshTranslateTransform();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RefreshScaleTranform()
    {
        _scaleTransform.ScaleX = 1 + _calculator.StretchX / _target.ActualWidth;
        _scaleTransform.ScaleY = 1 + _calculator.StretchY / _target.ActualHeight;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RefreshTranslateTransform()
    {
        _translateTransform.X = Math.Sign(DragDelta.X) * _calculator.OffsetX;
        _translateTransform.Y = Math.Sign(DragDelta.Y) * _calculator.OffsetY;
    }
}
