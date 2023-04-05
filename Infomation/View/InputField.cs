using System;
using System.Linq;
using UniqNumbers.ValueType;

namespace UniqNumbers.Infomation.View
{
    class InputField : Field
    {
        public InputField(Vector2 position) : base(position) { }

        public string DefaultText { get; set; }

        public string GetLine()
        {
            Clear();

            Text = DefaultText;

            ColorConfig.SetColor(TextColor, BackgroundColor);
            Console.SetCursorPosition(Position.X, Position.Y);

            Console.Write(Text);

            string text = Console.ReadLine();

            Text += text;

            Lines = Text.Split('\n').ToList();

            return text;
        }
    }
}
