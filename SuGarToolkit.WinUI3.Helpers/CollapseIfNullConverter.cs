using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

using System;

namespace SuGarToolkit.WinUI3.Helpers;

public partial class CollapseIfNullConverter : IValueConverter
{
    public static Visibility Convert(object? value) => value is null ? Visibility.Collapsed : Visibility.Visible;

    public object Convert(object value, Type targetType, object parameter, string culture) => Convert(value);

    public object ConvertBack(object value, Type targetType, object parameter, string culture) => throw new NotSupportedException();
}
