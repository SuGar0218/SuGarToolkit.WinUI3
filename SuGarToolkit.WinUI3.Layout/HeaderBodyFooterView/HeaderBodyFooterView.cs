using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;

namespace SuGarToolkit.WinUI3.Layout
{
    [TemplatePart(Name = nameof(PART_RootGrid), Type = typeof(HeaderBodyFooterView))]
    [TemplatePart(Name = nameof(PART_HeaderContentControl), Type = typeof(ContentControl))]
    [TemplatePart(Name = nameof(PART_BodyContentControl), Type = typeof(ContentControl))]
    [TemplatePart(Name = nameof(PART_FooterContentControl), Type = typeof(ContentControl))]
    public partial class HeaderBodyFooterView : ContentControl
    {
        public HeaderBodyFooterView()
        {
            DefaultStyleKey = typeof(HeaderBodyFooterView);
        }

        private Grid? PART_RootGrid;
        private ContentControl? PART_HeaderContentControl;
        private ContentControl? PART_BodyContentControl;
        private ContentControl? PART_FooterContentControl;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_RootGrid = GetTemplateChild(nameof(PART_RootGrid)) as Grid;
            PART_HeaderContentControl = GetTemplateChild(nameof(PART_HeaderContentControl)) as ContentControl;
            PART_BodyContentControl = GetTemplateChild(nameof(PART_BodyContentControl)) as ContentControl;
            PART_FooterContentControl = GetTemplateChild(nameof(PART_FooterContentControl)) as ContentControl;
            UpdateRootGridLayout();
        }

        private void UpdateRootGridLayout()
        {
            if (PART_RootGrid is null)
                return;

            switch (Orientation)
            {
                case Orientation.Vertical:
                    PART_RootGrid.ColumnDefinitions.Clear();
                    PART_RootGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
                    PART_RootGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    PART_RootGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
                    break;

                case Orientation.Horizontal:
                    PART_RootGrid.RowDefinitions.Clear();
                    PART_RootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
                    PART_RootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    PART_RootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
