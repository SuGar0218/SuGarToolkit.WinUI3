using System;
using System.Runtime.CompilerServices;

namespace SuGarToolkit.WinUI3.Controls.Decorators;

/// <summary>
/// y = kx / (x - b), x ≥ 0, b ≤ 0.
/// <br/>
/// The k is <see cref="Limit"/>. As x increasing, y appoaches to k.
/// <br/>
/// The b is <see cref="Growth"/>. The larger b is, the faster the growth.
/// </summary>
public class SaturatingFunction
{
    private double k = 1;
    private double b = -1;

    private double F(double x) => k * x / (x - b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Calculate(double x)
    {
#if DEBUG
        if (x < 0)
            throw new InvalidOperationException("x ≥ 0");
#endif
        if (b == 0)
            return Limit;

        if (x == 0)
            return 0;

        return F(x);
    }

    public double Limit
    {
        get => k;
        set => k = value;
    }

    public double Growth
    {
        get => b;
        set
        {
#if DEBUG
            if (b > 0)
                throw new InvalidOperationException("b ≤ 0");
#endif
            b = value;
        }
    }
}
