using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

using System;

namespace SuGarToolkit.WinUI3.Helpers;

public partial class DoubleToThicknessConverter : IValueConverter
{
    public static Thickness Convert(double value, ThicknessSides sides)
    {
        ThicknessSides[] allSides = [ThicknessSides.Left, ThicknessSides.Top, ThicknessSides.Right, ThicknessSides.Bottom];
        double[] result = new double[4];
        for (int i = 0; i < 4; i++)
        {
            result[i] = sides.HasFlag(allSides[i]) ? value : 0;
        }
        return new Thickness(result[0], result[1], result[2], result[3]);
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not double doubleValue)
            return value;

        return parameter switch
        {
            ThicknessSides sides => Convert(doubleValue, sides),
            int intValue => Convert(doubleValue, intValue.ToThicknessSides()),
            _ => Convert(doubleValue, ThicknessSides.Left | ThicknessSides.Top | ThicknessSides.Right | ThicknessSides.Bottom),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
