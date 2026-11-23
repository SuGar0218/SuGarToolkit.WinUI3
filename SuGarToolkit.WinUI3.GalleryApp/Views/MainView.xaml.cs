using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SuGarToolkit.WinUI3.Controls.Windows;
using SuGarToolkit.WinUI3.GalleryApp.ViewModels;

using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        List<ElementTheme> themes = [ ElementTheme.Default, ElementTheme.Light, ElementTheme.Dark ];
        themeComboBox.ItemsSource = themes;
        themeComboBox.SelectedIndex = 0;
        themeComboBox.SelectionChanged += OnThemeComboBoxSelectionChanged;
    }

    private void OnThemeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox) sender;
        ContentWindow.GetContentWindow(this)?.RequestedTheme = (ElementTheme) comboBox.SelectedItem;
    }

    private void OnNavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is PageViewModel pageViewModel)
        {
            PART_Frame.Navigate(pageViewModel.PageType);
        }
    }
}
