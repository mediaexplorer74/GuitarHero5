using Microsoft.Xna.Framework.Input;
using System;

#nullable disable
namespace com.glu.shared
{
    /// <summary>
    /// Класс для обработки ввода мыши и преобразования его в события касания
    /// </summary>
    public static class MouseInput
    {
        private static MouseState _currentMouseState;
        private static MouseState _previousMouseState;
        private static bool _mouseVisible = false;

        /// <summary>
        /// Свойство для управления видимостью курсора мыши
        /// </summary>
        public static bool MouseVisible
        {
            get { return _mouseVisible; }
            set
            {
                if (_mouseVisible != value)
                {
                    _mouseVisible = value;
                    if (CApplet.GetInstance() != null)
                    {
                        CApplet.GetInstance().IsMouseVisible = value;
                    }
                }
            }
        }

        /// <summary>
        /// Инициализация системы ввода мыши
        /// </summary>
        public static void Initialize()
        {
            _currentMouseState = Mouse.GetState();
            _previousMouseState = _currentMouseState;
            MouseVisible = true;
        }

        /// <summary>
        /// Обновление состояния мыши и генерация соответствующих событий касания
        /// </summary>
        /// <param name="applet">Экземпляр CApplet для отправки событий</param>
        public static void Update(CApplet applet)
        {
            if (applet == null) return;

            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            // Обработка нажатия левой кнопки мыши (эмуляция касания)
            if (_currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
            {
                // Событие нажатия (аналогично TouchLocationState.Pressed)
                int x = _currentMouseState.X;
                int y = _currentMouseState.Y;
                uint eventData = TouchUtil.TOUCH_EVENT_SET_Y(TouchUtil.TOUCH_EVENT_SET_X(0U, x), y);
                applet.QueueSystemEvent(902053462U, 0U, eventData);
            }
            // Обработка перемещения мыши при нажатой кнопке (эмуляция перемещения пальца)
            else if (_currentMouseState.LeftButton == ButtonState.Pressed && 
                    (_currentMouseState.X != _previousMouseState.X || _currentMouseState.Y != _previousMouseState.Y))
            {
                // Событие перемещения (аналогично TouchLocationState.Moved)
                int x = _currentMouseState.X;
                int y = _currentMouseState.Y;
                uint eventData = TouchUtil.TOUCH_EVENT_SET_Y(TouchUtil.TOUCH_EVENT_SET_X(0U, x), y);
                applet.QueueSystemEvent(902532892U, 0U, eventData);
            }
            // Обработка отпускания левой кнопки мыши (эмуляция отпускания касания)
            else if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                // Событие отпускания (аналогично TouchLocationState.Released)
                int x = _currentMouseState.X;
                int y = _currentMouseState.Y;
                uint eventData = TouchUtil.TOUCH_EVENT_SET_Y(TouchUtil.TOUCH_EVENT_SET_X(0U, x), y);
                applet.QueueSystemEvent(902008092U, 0U, eventData);
            }
        }

        /// <summary>
        /// Получить текущую позицию курсора мыши
        /// </summary>
        public static (int X, int Y) GetMousePosition()
        {
            return (_currentMouseState.X, _currentMouseState.Y);
        }
    }
}