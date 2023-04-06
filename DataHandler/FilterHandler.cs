using System;
using System.Collections.Generic;
using System.Text;

namespace UniqNumbers.DataHandler
{
    abstract class FilterHandler : IHandler
    {
        public abstract bool Handle(int number, List<int> numbers);
    }
}
