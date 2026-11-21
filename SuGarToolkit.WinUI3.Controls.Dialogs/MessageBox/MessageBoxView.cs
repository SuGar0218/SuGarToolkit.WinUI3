using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;

namespace SuGarToolkit.WinUI3.Controls.Dialogs;

[TemplatePart(Name = nameof(PART_ContentDialogView), Type = typeof(ContentDialogView))]
[TemplateVisualState(GroupName = "IconStates", Name = VisualStates.None)]
[TemplateVisualState(GroupName = "IconStates", Name = VisualStates.Information)]
[TemplateVisualState(GroupName = "IconStates", Name = VisualStates.Question)]
[TemplateVisualState(GroupName = "IconStates", Name = VisualStates.Error)]
[TemplateVisualState(GroupName = "IconStates", Name = VisualStates.Warning)]
[TemplateVisualState(GroupName = "IconStates", Name = VisualStates.Success)]
public partial class MessageBoxView : Control
{
    public MessageBoxView()
    {
        DefaultStyleKey = typeof(MessageBoxView);
        Loaded += OnLoaded;
        SizeChanged += OnSizeChanged;
    }

    #region DependencyProperty

    public string? Title
    {
        get => (string?) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(MessageBoxView),
        new PropertyMetadata(default(string))
    );

    public string? Message
    {
        get => (string?) GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
        nameof(Message),
        typeof(string),
        typeof(MessageBoxView),
        new PropertyMetadata(default(string))
    );

    public MessageBoxIcon Icon
    {
        get => (MessageBoxIcon) GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(MessageBoxIcon),
        typeof(MessageBoxView),
        new PropertyMetadata(default(MessageBoxIcon), OnIconChanged)
    );

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        MessageBoxView self = (MessageBoxView) d;
        self.DetermineIconState();
    }

    public MessageBoxButtons Buttons
    {
        get => (MessageBoxButtons) GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register(
        nameof(Buttons),
        typeof(MessageBoxButtons),
        typeof(MessageBoxView),
        new PropertyMetadata(default(MessageBoxButtons))
    );

    public MessageBoxDefaultButton DefaultButton
    {
        get => (MessageBoxDefaultButton) GetValue(DefaultButtonProperty);
        set => SetValue(DefaultButtonProperty, value);
    }

    public static readonly DependencyProperty DefaultButtonProperty = DependencyProperty.Register(
        nameof(DefaultButton),
        typeof(MessageBoxDefaultButton),
        typeof(MessageBoxView),
        new PropertyMetadata(default(MessageBoxDefaultButton))
    );

    #endregion

    public event EventHandler? ResultChanged;

    public MessageBoxResult Result
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            ResultChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private ContentDialogView? PART_ContentDialogView;

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        PART_ContentDialogView = GetTemplateChild(nameof(PART_ContentDialogView)) as ContentDialogView;
        if (PART_ContentDialogView is not null)
        {
            PART_ContentDialogView.PrimaryButtonClick += OnPrimaryButtonClick;
            PART_ContentDialogView.SecondaryButtonClick += OnSecondaryButtonClick;
            PART_ContentDialogView.CloseButtonClick += OnCloseButtonClick;
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        //PART_ContentDialogView.MaxWidth = double.PositiveInfinity;
        DetermineIconState();
        DetermineButtons();
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (IsLoaded)
        {
            PART_ContentDialogView?.MaxWidth = double.PositiveInfinity;
        }
    }

    private void OnPrimaryButtonClick(object? sender, EventArgs e)
    {
        Result = Buttons switch
        {
            MessageBoxButtons.OK => MessageBoxResult.OK,
            MessageBoxButtons.OKCancel => MessageBoxResult.OK,
            MessageBoxButtons.AbortRetryIgnore => MessageBoxResult.Abort,
            MessageBoxButtons.YesNoCancel => MessageBoxResult.Yes,
            MessageBoxButtons.YesNo => MessageBoxResult.Yes,
            MessageBoxButtons.RetryCancel => MessageBoxResult.Retry,
            MessageBoxButtons.CancelTryContinue => MessageBoxResult.Continue,
            _ => MessageBoxResult.None,
        };
    }

    private void OnSecondaryButtonClick(object? sender, EventArgs e)
    {
        Result = Buttons switch
        {
            MessageBoxButtons.OK => MessageBoxResult.OK,
            MessageBoxButtons.OKCancel => MessageBoxResult.Cancel,
            MessageBoxButtons.AbortRetryIgnore => MessageBoxResult.Retry,
            MessageBoxButtons.YesNoCancel => MessageBoxResult.No,
            MessageBoxButtons.YesNo => MessageBoxResult.No,
            MessageBoxButtons.RetryCancel => MessageBoxResult.Cancel,
            MessageBoxButtons.CancelTryContinue => MessageBoxResult.TryAgain,
            _ => MessageBoxResult.None,
        };
    }

    private void OnCloseButtonClick(object? sender, EventArgs e)
    {
        Result = Buttons switch
        {
            MessageBoxButtons.OK => MessageBoxResult.OK,
            MessageBoxButtons.OKCancel => MessageBoxResult.Cancel,
            MessageBoxButtons.AbortRetryIgnore => MessageBoxResult.Ignore,
            MessageBoxButtons.YesNoCancel => MessageBoxResult.Cancel,
            MessageBoxButtons.YesNo => MessageBoxResult.No,
            MessageBoxButtons.RetryCancel => MessageBoxResult.Cancel,
            MessageBoxButtons.CancelTryContinue => MessageBoxResult.Cancel,
            _ => MessageBoxResult.None,
        };
    }

    private void DetermineIconState()
    {
        IconState = Icon.ToString();
    }

    private void DetermineButtons()
    {
        if (PART_ContentDialogView is null)
            return;

        switch (Buttons)
        {
            case MessageBoxButtons.OK:
                {
                    MessageBoxButtonString okString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.OK);
                    PART_ContentDialogView.CloseButtonContent = okString.Text;
                    PART_ContentDialogView.CloseButtonAccessKey = okString.Key;
                    PART_ContentDialogView.DefaultButton = ContentDialogButton.Close;
                    break;
                }

            case MessageBoxButtons.OKCancel:
                {
                    MessageBoxButtonString okString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.OK);
                    MessageBoxButtonString cancelString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Cancel);
                    PART_ContentDialogView.PrimaryButtonContent = okString.Text;
                    PART_ContentDialogView.PrimaryButtonAccessKey = okString.Key;
                    PART_ContentDialogView.CloseButtonContent = cancelString.Text;
                    PART_ContentDialogView.CloseButtonAccessKey = cancelString.Key;
                    PART_ContentDialogView.DefaultButton = ContentDialogButton.Close;
                    break;
                }

            case MessageBoxButtons.YesNo:
                {
                    MessageBoxButtonString yesString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Yes);
                    MessageBoxButtonString noString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.No);
                    PART_ContentDialogView.PrimaryButtonContent = yesString.Text;
                    PART_ContentDialogView.PrimaryButtonAccessKey = yesString.Key;
                    PART_ContentDialogView.SecondaryButtonContent = noString.Text;
                    PART_ContentDialogView.SecondaryButtonAccessKey = noString.Key;
                    break;
                }

            case MessageBoxButtons.YesNoCancel:
                {
                    MessageBoxButtonString yesString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Yes);
                    MessageBoxButtonString noString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.No);
                    MessageBoxButtonString cancelString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Cancel);
                    PART_ContentDialogView.PrimaryButtonContent = yesString.Text;
                    PART_ContentDialogView.PrimaryButtonAccessKey = yesString.Key;
                    PART_ContentDialogView.SecondaryButtonContent = noString.Text;
                    PART_ContentDialogView.SecondaryButtonAccessKey = noString.Key;
                    PART_ContentDialogView.CloseButtonContent = cancelString.Text;
                    PART_ContentDialogView.CloseButtonAccessKey = cancelString.Key;
                    break;
                }

            case MessageBoxButtons.AbortRetryIgnore:
                {
                    MessageBoxButtonString abortString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Abort);
                    MessageBoxButtonString retryString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Retry);
                    MessageBoxButtonString ignoreString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Ignore);
                    PART_ContentDialogView.PrimaryButtonContent = abortString.Text;
                    PART_ContentDialogView.PrimaryButtonAccessKey = abortString.Key;
                    PART_ContentDialogView.SecondaryButtonContent = retryString.Text;
                    PART_ContentDialogView.SecondaryButtonAccessKey = retryString.Key;
                    PART_ContentDialogView.CloseButtonContent = ignoreString.Text;
                    PART_ContentDialogView.CloseButtonAccessKey = ignoreString.Key;
                    break;
                }

            case MessageBoxButtons.RetryCancel:
                {
                    MessageBoxButtonString retryString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Retry);
                    MessageBoxButtonString cancelString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Cancel);
                    PART_ContentDialogView.PrimaryButtonContent = retryString.Text;
                    PART_ContentDialogView.PrimaryButtonAccessKey = retryString.Key;
                    PART_ContentDialogView.SecondaryButtonContent = cancelString.Text;
                    PART_ContentDialogView.SecondaryButtonAccessKey = cancelString.Key;
                    break;
                }

            case MessageBoxButtons.CancelTryContinue:
                {
                    MessageBoxButtonString continueString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Continue);
                    MessageBoxButtonString tryString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.TryAgain);
                    MessageBoxButtonString cancelString = MessageBoxButtonString.FromUser32(NativeMessageBoxButtonStringLoader.Cancel);
                    PART_ContentDialogView.PrimaryButtonContent = continueString.Text;
                    PART_ContentDialogView.PrimaryButtonAccessKey = continueString.Key;
                    PART_ContentDialogView.SecondaryButtonContent = tryString.Text;
                    PART_ContentDialogView.SecondaryButtonAccessKey = tryString.Key;
                    PART_ContentDialogView.CloseButtonContent = cancelString.Text;
                    PART_ContentDialogView.CloseButtonAccessKey = cancelString.Key;
                    PART_ContentDialogView.DefaultButton = ContentDialogButton.Close;
                    break;
                }
        }
    }

    private string? IconState
    {
        get => field;
        set
        {
            field = value;
            VisualStateManager.GoToState(this, value, false);
        }
    }

    protected static class VisualStates
    {
        public const string None = nameof(MessageBoxIcon.None);
        public const string Information = nameof(MessageBoxIcon.Information);
        public const string Question = nameof(MessageBoxIcon.Question);
        public const string Error = nameof(MessageBoxIcon.Error);
        public const string Warning = nameof(MessageBoxIcon.Warning);
        public const string Success = nameof(MessageBoxIcon.Success);
    }

    private readonly struct MessageBoxButtonString
    {
        public MessageBoxButtonString(string text, string key)
        {
            Text = text;
            Key = key;
        }

        public static MessageBoxButtonString FromUser32(string loadedString)
        {
            string text;
            string key;
            int i = loadedString.IndexOf('&');
            if (i == -1)
            {
                text = loadedString;
                key = string.Empty;
            }
            else
            {
                text = loadedString.Remove(i, 1);
                key = loadedString[i + 1].ToString();  // For letters, VirtualKey enum value is the same as unicode.
            }
            return new MessageBoxButtonString(text, key);
        }

        public string Text { get; }

        public string Key { get; }
    }
}
