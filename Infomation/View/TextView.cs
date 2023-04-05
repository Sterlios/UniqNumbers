using System;
using System.Linq;
using UniqNumbers.ValueType;

namespace UniqNumbers.Infomation.View
{
    class TextView : Field
    {
        public TextView(Vector2 position) : base(position) { }

        public void Update()
        {
            Clear();

            Lines = Text.Split('\n').ToList();

            for (int i = 0; i < Lines.Count; i++)
            {
                ColorConfig.SetColor(TextColor, BackgroundColor);
                Console.SetCursorPosition(Position.X, Position.Y + i);
                Console.WriteLine(Lines[i]);
            }
        }
    }
}
