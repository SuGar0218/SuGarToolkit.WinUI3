using Microsoft.UI.Xaml.Controls;

using SuGarToolkit.WinUI3.Controls.Decorators;
using SuGarToolkit.WinUI3.Helpers;

namespace SuGarToolkit.WinUI3.GalleryApp.Views.LiquidGlassLike;

public sealed partial class LiquidGlassLikeInteractionPage : Page
{
    public LiquidGlassLikeInteractionPage()
    {
        InitializeComponent();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Button button = new()
                {
                    Width = 64,
                    Height = 64,
                    Content = new TextBlock
                    {
                        Text = (i * 3 + j + 1).ToString(),
                        FontSize = 32
                    }
                };
                CapsuleShapeHelper.SetShapeIntoCapsule(button, true);
                LiquidGlassLikeInteractionDecorator decorator = new() { Content = button };
                Grid.SetRow(decorator, i);
                Grid.SetColumn(decorator, j);
                PART_DialGrid.Children.Add(decorator);
            }
        }
    }
}
