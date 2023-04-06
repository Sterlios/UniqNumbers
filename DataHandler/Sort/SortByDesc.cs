using System;
using System.Collections.Generic;
using System.Text;

namespace UniqNumbers.DataHandler.Sort
{
    class SortByDesc : ISortHandler
    {
        public void Sort(int number, List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (number > numbers[i])
                {
                    numbers.Insert(i, number);
                    return;
                }
            }

            numbers.Add(number);
        }
    }
}
