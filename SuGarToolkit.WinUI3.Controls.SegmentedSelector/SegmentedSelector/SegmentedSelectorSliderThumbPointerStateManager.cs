using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;

using System;

namespace SuGarToolkit.WinUI3.Controls
{
    internal partial class SegmentedSelectorSliderThumbPointerStateManager : IDisposable
    {
        public SegmentedSelectorSliderThumbPointerStateManager(Thumb thumb)
        {
            _thumb = thumb;
            _pointerEnteredEventHandler = new PointerEventHandler(OnPointerEntered);
            _pointerExitedEventHandler = new PointerEventHandler(OnPointerExited);
            _thumb.AddHandler(UIElement.PointerEnteredEvent, _pointerEnteredEventHandler, handledEventsToo: true);
            _thumb.AddHandler(UIElement.PointerExitedEvent, _pointerExitedEventHandler, handledEventsToo: true);
            _thumb.DragStarted += OnDragStarted;
            _thumb.DragCompleted += OnDragCompleted;
        }

        private readonly Thumb _thumb;
        private readonly PointerEventHandler _pointerEnteredEventHandler;
        private readonly PointerEventHandler _pointerExitedEventHandler;
        private bool _isPoniterOver;

        public event EventHandler? StateChanged;

        public SegmentedSelectorSliderThumbPointerState State
        {
            get;
            private set
            {
                if (field == value)
                    return;

                field = value;
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _isPoniterOver = true;
            if (!_thumb.IsDragging)
            {
                State = SegmentedSelectorSliderThumbPointerState.PointerOver;
            }
        }

        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            _isPoniterOver = false;
            if (!_thumb.IsDragging)
            {
                State = SegmentedSelectorSliderThumbPointerState.Normal;
            }
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            State = SegmentedSelectorSliderThumbPointerState.Dragging;
        }

        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            State = _isPoniterOver ? SegmentedSelectorSliderThumbPointerState.PointerOver : SegmentedSelectorSliderThumbPointerState.Normal;
        }

        public void Dispose()
        {
            _thumb.RemoveHandler(UIElement.PointerEnteredEvent, _pointerEnteredEventHandler);
            _thumb.RemoveHandler(UIElement.PointerExitedEvent, _pointerExitedEventHandler);
            _thumb.DragStarted -= OnDragStarted;
            _thumb.DragCompleted -= OnDragCompleted;
        }
    }
}
