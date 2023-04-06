using System;
using System.Collections.Generic;
using System.Text;

namespace UniqNumbers.DataHandler.Filter
{
    class UniqHandler : FilterHandler
    {
        private Dictionary<int, bool> _numbers;

        public UniqHandler()
        {
            _numbers = new Dictionary<int, bool>();
        }

        public override bool Handle(int number, List<int> numbers)
        {
            if (_numbers.ContainsKey(number))
            {
                _numbers[number] = false;
                numbers.Remove(number);
            }
            else
            {
                _numbers.Add(number, true);
                numbers.Add(number);
            }

            return _numbers[number];
        }
    }
}
