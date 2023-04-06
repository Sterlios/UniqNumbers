using System;
using System.Collections.Generic;
using System.Text;
using UniqNumbers.DataHandler.Sort;

namespace UniqNumbers.DataHandler
{
    class ArrayHandler : IHandler
    {
        private ISortHandler _sorter;

        public ArrayHandler(ISortHandler sorter)
        {
            _sorter = sorter;
        }

        public void HandleNumber(int number, bool isCorrectNumber, List<int> numbers)
        {
            if (isCorrectNumber)
                _sorter.Sort(number, numbers);
            else
                numbers.Remove(number);
        }
    }
}
