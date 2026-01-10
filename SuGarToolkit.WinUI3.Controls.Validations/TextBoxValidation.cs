using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;

namespace SuGarToolkit.WinUI3.Controls.Validations;

public class TextBoxValidation
{
    public static Predicate<string> GetValidation(TextBox target) => (Predicate<string>) target.GetValue(ValidationProperty);
    public static void SetValidation(TextBox target, Predicate<string> value) => target.SetValue(ValidationProperty, value);

    public static readonly DependencyProperty ValidationProperty = DependencyProperty.RegisterAttached(
        "Validation",
        typeof(Predicate<string>),
        typeof(TextBox),
        new PropertyMetadata(default(Predicate<string>), OnValidationChanged)
    );

    public static ValidationTrigger GetValidationTrigger(TextBox target) => (ValidationTrigger) target.GetValue(ValidationTriggerProperty);
    public static void SetValidationTrigger(TextBox target, ValidationTrigger value) => target.SetValue(ValidationTriggerProperty, value);

    public static readonly DependencyProperty ValidationTriggerProperty = DependencyProperty.RegisterAttached(
        "ValidationTrigger",
        typeof(ValidationTrigger),
        typeof(TextBox),
        new PropertyMetadata(default(ValidationTrigger), OnValidationTriggerChanged)
    );

    public static Action GetValidationSucceed(TextBox target) => (Action) target.GetValue(ValidationSucceedProperty);
    public static void SetValidationSucceed(TextBox target, Action value) => target.SetValue(ValidationSucceedProperty, value);

    public static readonly DependencyProperty ValidationSucceedProperty = DependencyProperty.RegisterAttached(
        "ValidationSucceed",
        typeof(Action),
        typeof(TextBox),
        new PropertyMetadata(default(Action))
    );

    public static Action GetValidationFailed(TextBox target) => (Action) target.GetValue(ValidationFailedProperty);
    public static void SetValidationFailed(TextBox target, Action value) => target.SetValue(ValidationFailedProperty, value);

    public static readonly DependencyProperty ValidationFailedProperty = DependencyProperty.RegisterAttached(
        "ValidationFailed",
        typeof(Action),
        typeof(TextBox),
        new PropertyMetadata(default(Action))
    );

    public static bool Validate(TextBox textBox)
    {
        if (GetValidation(textBox).Invoke(textBox.Text))
        {
            GetValidationSucceed(textBox).Invoke();
            return true;
        }
        else
        {
            GetValidationFailed(textBox).Invoke();
            return false;
        }
    }

    private static void OnTextChanged(DependencyObject sender, DependencyProperty dp)
    {
        Validate((TextBox) sender);
    }

    private static void OnLostFocus(object sender, RoutedEventArgs e)
    {
        Validate((TextBox) sender);
    }

    private static void AddPropertyChangedValidation(TextBox target)
    {
        if (!_textChangedCallbackTokens.TryGetValue(target, out long token))
        {
            token = target.RegisterPropertyChangedCallback(TextBox.TextProperty, OnTextChanged);
            _textChangedCallbackTokens.Add(target, token);
        }
    }

    private static void RemovePropertyChangedValidation(TextBox target)
    {
        if (_textChangedCallbackTokens.TryGetValue(target, out long token))
        {
            target.UnregisterPropertyChangedCallback(TextBox.TextProperty, token);
        }
    }

    private static void AddLostFocusValidation(TextBox target)
    {
        if (!_lostFocusHandlers.TryGetValue(target, out RoutedEventHandler? handler) || handler is null)
        {
            handler = OnLostFocus;
            target.LostFocus += handler;
            _lostFocusHandlers.Add(target, handler);
        }
    }

    private static void RemoveLostFocusValidation(TextBox target)
    {
        if (_lostFocusHandlers.TryGetValue(target, out RoutedEventHandler? handler) && handler is not null)
        {
            target.LostFocus -= handler;
            _lostFocusHandlers.Remove(target);
        }
    }

    private static void OnValidationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TextBox target = (TextBox) d;
        Predicate<string>? value = e.NewValue is null ? null : (Predicate<string>) e.NewValue;
        switch ((GetValidationTrigger(target)))
        {
            case ValidationTrigger.PropertyChanged:
                if (value is null)
                {
                    RemovePropertyChangedValidation(target);
                }
                else
                {
                    AddPropertyChangedValidation(target);
                }
                break;

            case ValidationTrigger.Default:
            case ValidationTrigger.LostFocus:
                if (value is null)
                {
                    RemoveLostFocusValidation(target);
                }
                else
                {
                    AddLostFocusValidation(target);
                }
                break;

            case ValidationTrigger.Explicit:
                break;

            default:
                throw new InvalidOperationException(nameof(ValidationTrigger));
        }
    }

    private static void OnValidationTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TextBox target = (TextBox) d;
        Predicate<string>? validation = GetValidation(target);
        if (validation is null)
            return;

        switch ((ValidationTrigger) e.NewValue)
        {
            case ValidationTrigger.PropertyChanged:
                RemoveLostFocusValidation(target);
                AddPropertyChangedValidation(target);
                break;

            case ValidationTrigger.Default:
            case ValidationTrigger.LostFocus:
                RemovePropertyChangedValidation(target);
                AddLostFocusValidation(target);
                break;

            case ValidationTrigger.Explicit:
                RemovePropertyChangedValidation(target);
                RemoveLostFocusValidation(target);
                break;

            default:
                throw new InvalidOperationException(nameof(ValidationTrigger));
        }
    }

    private static readonly Dictionary<TextBox, long> _textChangedCallbackTokens = [];
    private static readonly Dictionary<TextBox, RoutedEventHandler> _lostFocusHandlers = [];
}
