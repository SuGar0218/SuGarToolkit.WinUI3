using Microsoft.UI.Xaml.Controls;

using System;

namespace SuGarToolkit.WinUI3.Samples.Dialogs.ViewModels;

public class PageViewModel
{
    public static PageViewModel Create<TPage>(string name) where TPage : Page
    {
        return new(typeof(TPage), name);
    }

    private PageViewModel(Type type, string name)
    {
        PageType = type;
        Name = name;
    }

    public Type PageType { get; set; }

    public string Name { get; set; }
}
