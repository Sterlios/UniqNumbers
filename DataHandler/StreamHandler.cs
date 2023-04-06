using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UniqNumbers.Infomation.Presenter;

namespace UniqNumbers.DataHandler
{
    class StreamHandler : IHandler
    {
        private readonly int _defaultMinNumbersCount = 100;
        private readonly int _defaultMaxNumbersCount = 1000;
        private readonly int _defaultMinNumberRange = 0;
        private readonly int _defaultMaxNumberRange = 100000;

        private static Random _random = new Random();

        private FileHandler _reader;
        private FilterHandler[] _filters;
        private ArrayHandler _arrayHandler;

        private int _minNumbersCount;
        private int _maxNumbersCount;
        private int _minNumberRange;
        private int _maxNumberRange;

        private ParametersPresenter _parametersPresenter;

        private List<int> _result;

        public StreamHandler(FileHandler reader, FilterHandler[] filters, ArrayHandler arrayHandler)
        {
            _reader = reader;
            _filters = filters;
            _arrayHandler = arrayHandler;

            _result = new List<int>();

            _minNumbersCount = _defaultMinNumbersCount;
            _maxNumbersCount = _defaultMaxNumbersCount;
            _minNumberRange = _defaultMinNumberRange;
            _maxNumberRange = _defaultMaxNumberRange;
        }

        ~StreamHandler()
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
            List<int> temporaryArray = new List<int>();

            foreach (string fileName in fileNames)
                ReadFile(fileName, temporaryArray);

            return _result;
        }

        private void ReadFile(string fileName, List<int> temporaryArray)
        {
            _reader.Open(fileName);

            bool isFileOpen = true;

            while (isFileOpen)
            {
                string element = _reader.ReadElement();

                if (element is null)
                    isFileOpen = false;

                HandleElement(element, temporaryArray);
            }

            _reader.Close();
        }

        private void HandleElement(string element, List<int> temporaryArray)
        {
            if (int.TryParse(element, out int number))
            {
                bool isCorrectNumber = IsCorrectNumber(number, temporaryArray);

                _arrayHandler.HandleNumber(number, isCorrectNumber, _result);
            }
        }

        private bool IsCorrectNumber(int number, List<int> temporaryArray)
        {
            foreach (var filter in _filters)
                if (filter.Handle(number, temporaryArray) == false)
                    return false;

            return true;
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
    }
}
