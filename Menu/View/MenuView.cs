using System;
using UniqNumbers.Menu.Controller;

namespace UniqNumbers.Menu.View
{
    class MenuView
    {
        private readonly IMenuController[] _items;
        private readonly string _title;

        public MenuView(string title, IMenuController[] items)
        {
            _title = title;
            _items = items;
        }

        public void Draw(IMenuController selectedItem)
        {
            Console.SetCursorPosition(0, 0);

            Console.WriteLine(_title);
            Console.WriteLine();

            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == selectedItem)
                    ColorConfig.SetColor(ColorConfig.SelectedTextColor, ColorConfig.BackgroundSelectedTextColor);
                else
                    ColorConfig.SetColor(ColorConfig.TextDefaultColor, ColorConfig.BackgroundDefaultColor);

                Console.WriteLine(_items[i]);
            }

            ColorConfig.SetColor(ColorConfig.TextDefaultColor, ColorConfig.BackgroundDefaultColor);
        }
    }
}
