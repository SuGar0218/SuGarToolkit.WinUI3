using System;
using System.Runtime.CompilerServices;

using Windows.Foundation;

namespace SuGarToolkit.WinUI3.Controls.Decorators;

public class LiquidGlassLikeStretchCalculator
{
    public Size OriginalSize { get; set; }
    public Point DragDelta { get; set; }

    public double StretchX { get; private set; }
    public double StretchY { get; private set; }

    public double OffsetX { get; private set; }
    public double OffsetY { get; private set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Calculate()
    {
        double absDeltaX = Math.Abs(DragDelta.X);
        double absDeltaY = Math.Abs(DragDelta.Y);
        StretchX = _axialStretchingFunction.Calculate(absDeltaX) - _verticalShrinkFunction.Calculate(absDeltaY) * OriginalSize.Width;
        StretchY = _axialStretchingFunction.Calculate(absDeltaY) - _verticalShrinkFunction.Calculate(absDeltaX) * OriginalSize.Height;
        OffsetX = _offsetFunction.Calculate(absDeltaX);
        OffsetY = _offsetFunction.Calculate(absDeltaY);
    }

    /// <summary>
    /// Absolutely stretch.
    /// </summary>
    private static readonly SaturatingFunction _axialStretchingFunction = new()
    {
        Limit = 16,
        Growth = -618
    };

    /// <summary>
    /// Relatively shrink.
    /// </summary>
    private static readonly SaturatingFunction _verticalShrinkFunction = new()
    {
        Limit = 0.382,
        Growth = -618
    };

    /// <summary>
    /// Absolutely offset.
    /// </summary>
    private static readonly SaturatingFunction _offsetFunction = new()
    {
        Limit = 24,
        Growth = -618
    };
}
