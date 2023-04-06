using System;
using System.Collections.Generic;
using System.Text;

namespace UniqNumbers.DataHandler.Filter
{
    class ModuloHandler : FilterHandler
    {
        private int _devider = 4;
        private int _remainder = 3;

        public override bool Handle(int number, List<int> numbers)
        {
            return number % _devider == _remainder;
        }
    }
}
