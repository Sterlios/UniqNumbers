using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UniqNumbers.Infomation.Presenter;

namespace UniqNumbers.DataHandler
{
    class ArrayHandler
    {
        private readonly int _defaultMinNumbersCount = 100;
        private readonly int _defaultMaxNumbersCount = 1000;
        private readonly int _defaultMinNumberRange = 0;
        private readonly int _defaultMaxNumberRange = 100000;

        private static Random _random = new Random();

        private int _minNumbersCount;
        private int _maxNumbersCount;
        private int _minNumberRange;
        private int _maxNumberRange;
        private int _devider = 4;
        private int _remainder = 3;

        private ParametersPresenter _parametersPresenter;

        private List<int> _result;

        public ArrayHandler()
        {
            _minNumbersCount = _defaultMinNumbersCount;
            _maxNumbersCount = _defaultMaxNumbersCount;
            _minNumberRange = _defaultMinNumberRange;
            _maxNumberRange = _defaultMaxNumberRange;
        }

        ~ArrayHandler()
        {
            if (_parametersPresenter != null)
            {
                _parametersPresenter.EnteredMinNumberRange -= OnEnteredMinNumberRange;
                _parametersPresenter.EnteredMaxNumberRange -= OnEnteredMaxNumberRange;
                _parametersPresenter.EnteredMinNumbersCount -= OnEnteredMinNumbersCount;
                _parametersPresenter.EnteredMaxNumbersCount -= OnEnteredMaxNumbersCount;
            }
        }

        public event Action<TimeSpan> CalculatedTime;

        public void InitParametersPresenter(ParametersPresenter parametersPresenter)
        {
            _parametersPresenter = parametersPresenter;

            _parametersPresenter.EnteredMinNumberRange += OnEnteredMinNumberRange;
            _parametersPresenter.EnteredMaxNumberRange += OnEnteredMaxNumberRange;
            _parametersPresenter.EnteredMinNumbersCount += OnEnteredMinNumbersCount;
            _parametersPresenter.EnteredMaxNumbersCount += OnEnteredMaxNumbersCount;
        }

        public void GenerateData(List<string> fileFullName)
        {
            foreach (var fileName in fileFullName)
            {
                StreamWriter writer = new StreamWriter(fileName);
                int numbersCount = _random.Next(_minNumbersCount, _maxNumbersCount);

                for (int i = 0; i < numbersCount; i++)
                {
                    int number = _random.Next(_minNumberRange, _maxNumberRange);
                    writer.WriteLine(number);
                }

                writer.Close();
                writer.Dispose();
            }
        }

        public List<int> Handle(List<string> fileNames)
        {
            _result = GetNumbersWithModuloFromFiles(fileNames);

            _result = FilterUniqNumbers(_result);

            _result = Sort(_result);

            return _result;
        }

        private List<int> Sort(List<int> numbers)
        {
            Console.SetCursorPosition(20, 20);
            Console.WriteLine("Сортировка с помощью Linq. Нажмите клавишу ");
            Console.ReadKey();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<int> result = SortByLinq(numbers);
            stopwatch.Stop();
            CalculatedTime?.Invoke(stopwatch.Elapsed);

            Console.SetCursorPosition(20, 20);
            Console.WriteLine("Сортировка с помощью Merge. Нажмите клавишу");
            Console.ReadKey();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            List<int> result2 = SortByMerge(numbers);
            stopwatch.Stop();
            CalculatedTime?.Invoke(stopwatch.Elapsed);

            return result;
        }

        private List<int> ReadStream(StreamReader reader)
        {
            bool isEndFile = false;
            List<int> result = new List<int>();

            while (isEndFile == false)
            {
                string line = reader.ReadLine();

                isEndFile = string.IsNullOrEmpty(line);

                if (int.TryParse(line, out int number))
                    if (FilterByModulo(number))
                        result.Add(number);
            }

            return result;
        }

        private void OnEnteredMinNumberRange(string numberText)
        {
            if (int.TryParse(numberText, out int number))
                _minNumberRange = number;
            else
                _minNumberRange = _defaultMinNumberRange;
        }

        private void OnEnteredMaxNumberRange(string numberText)
        {
            if (int.TryParse(numberText, out int number) && number > _minNumberRange)
                _maxNumberRange = number;
            else
                _maxNumberRange = _minNumberRange + 1;
        }

        private void OnEnteredMinNumbersCount(string numberText)
        {
            if (int.TryParse(numberText, out int number) && number > 0)
                _minNumbersCount = number;
            else
                _minNumbersCount = _defaultMinNumbersCount;
        }

        private void OnEnteredMaxNumbersCount(string numberText)
        {
            if (int.TryParse(numberText, out int number) && number > _minNumbersCount)
                _maxNumbersCount = number;
            else
                _maxNumbersCount = _minNumbersCount;
        }

        private bool FilterByModulo(int number)
        {
            return number % _devider == _remainder;
        }

        private List<int> SortByLinq(List<int> numbers)
        {
            return numbers.OrderByDescending(number => number).ToList();
        }

        private List<int> SortByMerge(List<int> numbers)
        {
            int sortLimitCount = 2;

            if (numbers.Count < sortLimitCount)
                return numbers;

            if (numbers.Count == sortLimitCount)
            {
                (numbers[0], numbers[1]) = (Math.Max(numbers[0], numbers[1]), Math.Min(numbers[0], numbers[1]));

                return numbers;
            }

            int devider = 2;
            int firstSubArrayCount = numbers.Count / devider;
            int secondSubArrayCount = numbers.Count - firstSubArrayCount;

            List<int> firstSubArray = SortByMerge(numbers.GetRange(0, firstSubArrayCount));
            List<int> secondSubArray = SortByMerge(numbers.GetRange(firstSubArrayCount + 1, secondSubArrayCount - 1));

            List<int> result = new List<int>();
            int startIndex = 0;

            for (int i = 0; i < firstSubArray.Count; i++)
            {
                for (int j = startIndex; j < secondSubArray.Count; j++)
                {
                    if (secondSubArray[j] < firstSubArray[i])
                    {
                        result.Add(firstSubArray[i]);
                        startIndex = j;
                        break;
                    }
                    else
                    {
                        result.Add(secondSubArray[j]);
                    }
                }
            }

            return numbers;
        }

        private List<int> FilterUniqNumbers(List<int> numbers)
        {
            List<int> uniqueNumbers = numbers.Distinct().ToList();

            foreach (int currentNumber in numbers)
                if (numbers.Where(number => number == currentNumber).ToList().Count > 1)
                    numbers = numbers.Where(number => number != currentNumber).ToList();

            return numbers;
        }

        private List<int> GetNumbersWithModuloFromFiles(List<string> fileNames)
        {
            List<int> result = new List<int>();

            foreach (string fileName in fileNames)
            {
                var reader = new StreamReader(fileName);

                result.AddRange(ReadStream(reader));

                reader.Close();
                reader.Dispose();
            }

            return result;
        }
    }
}
