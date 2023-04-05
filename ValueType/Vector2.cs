using System;
using System.Collections.Generic;
using System.Text;

namespace UniqNumbers.ValueType
{
    struct Vector2
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;

            if (!Validate(X))
                X = 0;

            if (!Validate(Y))
                Y = 0;
        }

        public void Set(int x, int y)
        {
            if (!Validate(x))
                return;

            if (!Validate(y))
                return;

            X = x;
            Y = y;
        }

        private bool Validate(int value)
        {
            return value >= 0;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
    }
}
