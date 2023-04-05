using System;
using System.Collections.Generic;
using UniqNumbers.ValueType;

namespace UniqNumbers.Infomation.View
{
    abstract class Field
    {
        public Field(Vector2 position)
        {
            Position = position;
            Lines = new List<string>();
        }

        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        public string Text { get; set; } = "";
        protected Vector2 Position { get; private set; }
        protected List<string> Lines { get; set; }

        protected void Clear()
        {
            if (Lines.Count > 0)
                for (int i = 0; i < Lines.Count; i++)
                {
                    Console.SetCursorPosition(Position.X, Position.Y + i);

                    foreach (char letter in Lines[i])
                        Console.Write(' ');
                }
        }
    }
}
