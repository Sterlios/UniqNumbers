using System;
using UniqNumbers.Infomation.View;

namespace UniqNumbers.Infomation.Presenter
{
    class ParametersPresenter
    {
        private ParametersView _view;

        public ParametersPresenter()
        {
            _view = new ParametersView();
        }

        public event Action<string> EnteredCountFiles;
        public event Action<string> EnteredMinNumbersCount;
        public event Action<string> EnteredMaxNumbersCount;
        public event Action<string> EnteredMinNumberRange;
        public event Action<string> EnteredMaxNumberRange;

        public void Enable()
        {
            _view.EnteredCountFiles += OnEnteredCountFiles;
            _view.EnteredMinNumbersCount += OnEnteredMinNumbersCount;
            _view.EnteredMaxNumbersCount += OnEnteredMaxNumbersCount;
            _view.EnteredMinNumberRange += OnEnteredMinNumberRange;
            _view.EnteredMaxNumberRange += OnEnteredMaxNumberRange;

            _view.Update();

            Disable();
        }

        private void Disable()
        {
            _view.EnteredCountFiles -= OnEnteredCountFiles;
            _view.EnteredMinNumbersCount -= OnEnteredMinNumbersCount;
            _view.EnteredMaxNumbersCount -= OnEnteredMaxNumbersCount;
            _view.EnteredMinNumberRange -= OnEnteredMinNumberRange;
            _view.EnteredMaxNumberRange -= OnEnteredMaxNumberRange;
        }

        private void OnEnteredCountFiles(string text)
        {
            EnteredCountFiles?.Invoke(text);
        }

        private void OnEnteredMinNumbersCount(string text)
        {
            EnteredMinNumbersCount?.Invoke(text);
        }

        private void OnEnteredMaxNumbersCount(string text)
        {
            EnteredMaxNumbersCount?.Invoke(text);
        }

        private void OnEnteredMinNumberRange(string text)
        {
            EnteredMinNumberRange?.Invoke(text);
        }

        private void OnEnteredMaxNumberRange(string text)
        {
            EnteredMaxNumberRange?.Invoke(text);
        }
    }
}
