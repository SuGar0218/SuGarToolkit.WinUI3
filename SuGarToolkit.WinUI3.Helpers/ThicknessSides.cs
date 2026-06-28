using System;

namespace SuGarToolkit.WinUI3.Helpers;

[Flags]
public enum ThicknessSides
{
    None = 0b0000,
    Left = 0b1000,
    Top = 0b0100,
    Right = 0b0010,
    Bottom = 0b00001
}

public static class ThicknessSidesExtensions
{
    public static ThicknessSides ToThicknessSides(this int value)
    {
        ThicknessSides sides = ThicknessSides.None;
        foreach (ThicknessSides side in Enum.GetValues<ThicknessSides>())
        {
            if (((int) side & value) != 0)
            {
                sides |= side;
            }
        }
        return sides;
    }
}
