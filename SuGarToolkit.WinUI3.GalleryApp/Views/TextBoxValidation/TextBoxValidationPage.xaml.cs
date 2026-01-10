using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using SuGarToolkit.WinUI3.Controls.Validations;

using System;

namespace SuGarToolkit.WinUI3.GalleryApp.Views;

public sealed partial class TextBoxValidationPage : Page
{
    public TextBoxValidationPage()
    {
        InitializeComponent();
        ValidationSucceed = OnValidationSucceed;
        ValidationFailed = OnValidationFailed;
    }

    private readonly Predicate<string> InputValidation = input => input == "true";

    private readonly Action ValidationSucceed;

    private readonly Action ValidationFailed;

    private void OnValidationSucceed()
    {
        PART_ValidationResultTextBlock.Text = "Valid";
        PART_ValidationResultTextBlock.Foreground = _validForeground;
        ValidationResultTip.Title = "Valid";
        ValidationResultTip.Content = "TextBox validation succeed";
        ValidationResultTip.Target = PART_InputTextBox;
        ValidationResultTip.IsOpen = true;
    }

    private void OnValidationFailed()
    {
        PART_ValidationResultTextBlock.Text = "Invalid";
        PART_ValidationResultTextBlock.Foreground = _invalidForeground;
        ValidationResultTip.Title = "Invalid";
        ValidationResultTip.Content = "TextBox validation failed";
        ValidationResultTip.Target = PART_InputTextBox;
        ValidationResultTip.IsOpen = true;
    }

    private static readonly SolidColorBrush _validForeground = new(Colors.Green);
    private static readonly SolidColorBrush _invalidForeground = new(Colors.Red);

    private void OnValidateButtonClick(object sender, RoutedEventArgs e)
    {
        TextBoxValidation.Validate(PART_InputTextBox);
    }
}
