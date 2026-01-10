using CommunityToolkit.Mvvm.ComponentModel;

using SuGarToolkit.WinUI3.Controls.Validations;

using System;
using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.GalleryApp.ViewModels;

public partial class TextBoxValidationViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Input { get; set; }

    [ObservableProperty]
    public partial bool IsValid { get; set; }

    [ObservableProperty]
    public partial ValidationTrigger Trigger { get; set; }

    public Predicate<string> InputValidation => field ??= new Predicate<string>(Validate);

    public bool Validate(string input)
    {
        return string.Compare(input, "true", ignoreCase: true) == 0;
    }

    public static IEnumerable<ValidationTrigger> ValidationTriggers => field ??= Enum.GetValues<ValidationTrigger>();
}
