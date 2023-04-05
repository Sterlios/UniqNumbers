using System;

namespace UniqNumbers
{
    public static class ColorConfig
    {
        public readonly static ConsoleColor TextDefaultColor = ConsoleColor.White;
        public readonly static ConsoleColor SelectedTextColor = ConsoleColor.Black;

        public readonly static ConsoleColor BackgroundDefaultColor = ConsoleColor.Black;
        public readonly static ConsoleColor BackgroundSelectedTextColor = ConsoleColor.White;

        public static void SetColor(ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
        }
    }
}
