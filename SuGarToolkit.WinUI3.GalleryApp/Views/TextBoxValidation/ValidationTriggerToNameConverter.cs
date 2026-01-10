using Microsoft.UI.Xaml.Data;

using SuGarToolkit.WinUI3.Controls.Validations;

using System;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

internal partial class ValidationTriggerToNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => Convert((ValidationTrigger) value);

    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();

    public static string Convert(ValidationTrigger value) => value switch
    {
        ValidationTrigger.Default => nameof(ValidationTrigger.Default) + " (LostFocus)",
        ValidationTrigger.PropertyChanged => nameof(ValidationTrigger.PropertyChanged),
        ValidationTrigger.LostFocus => nameof(ValidationTrigger.LostFocus),
        ValidationTrigger.Explicit => nameof(ValidationTrigger.Explicit),
        _ => throw new ArgumentOutOfRangeException(nameof(value)),
    };
}
