using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static com.glu.shared.Color;

#nullable disable
namespace com.glu.shared
{
    /// <summary>
    /// Класс для отображения курсора мыши
    /// </summary>
    public static class MouseCursor
    {
        private static Texture2D _cursorTexture;
        private static SpriteBatch _spriteBatch;
        private static bool _initialized = false;
        private static Vector2 _hotSpot = new Vector2(0, 0);

        /// <summary>
        /// Инициализация курсора мыши
        /// </summary>
        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            if (_initialized) return;

            _spriteBatch = new SpriteBatch(graphicsDevice);
            
            // Создаем простую текстуру для курсора (белый квадрат 16x16 пикселей)
            _cursorTexture = new Texture2D(graphicsDevice, 16, 16);
            Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[16 * 16];
            
            // Заполняем текстуру курсора
            for (int i = 0; i < data.Length; i++)
            {
                // Создаем простой курсор в виде стрелки
                int x = i % 16;
                int y = i / 16;
                
                // Основная часть курсора (белая)
                if (x == 0 || y == 0 || (x < 8 && y < 8 && x == y))
                {
                    data[i] = default;//new Microsoft.Xna.Framework.Color(255, 255, 255);
                }
                // Черная обводка
                else if ((x == 1 && y <= 8) || (y == 1 && x <= 8) || (x == y + 1 && x < 9) || (x == y - 1 && y < 9))
                {
                    data[i] = new Microsoft.Xna.Framework.Color(0, 0, 0);
                }
                else
                {
                    data[i] = new Microsoft.Xna.Framework.Color(0, 0, 0);//Color.Transparent; 
                }
            }
            
            _cursorTexture.SetData(data);
            _initialized = true;
        }

        /// <summary>
        /// Отрисовка курсора мыши
        /// </summary>
        public static void Draw()
        {
            if (!_initialized || !MouseInput.MouseVisible) return;

            var position = MouseInput.GetMousePosition();
            Vector2 cursorPosition = new Vector2(position.X - _hotSpot.X, position.Y - _hotSpot.Y);

            _spriteBatch.Begin();
            //_spriteBatch.Draw(_cursorTexture, cursorPosition, new Microsoft.Xna.Framework.Color(255, 255, 255));
            _spriteBatch.End();
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public static void Dispose()
        {
            if (_cursorTexture != null)
            {
                _cursorTexture.Dispose();
                _cursorTexture = null;
            }

            if (_spriteBatch != null)
            {
                _spriteBatch.Dispose();
                _spriteBatch = null;
            }

            _initialized = false;
        }
    }
}