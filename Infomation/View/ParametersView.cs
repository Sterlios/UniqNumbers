using System;
using UniqNumbers.ValueType;

namespace UniqNumbers.Infomation.View
{
    class ParametersView
    {
        public ParametersView()
        {
            Init();
        }

        public event Action<string> EnteredCountFiles;
        public event Action<string> EnteredMinNumbersCount;
        public event Action<string> EnteredMaxNumbersCount;
        public event Action<string> EnteredMinNumberRange;
        public event Action<string> EnteredMaxNumberRange;

        public InputField CountFilesField { get; private set; } = new InputField(new Vector2(30, 1));
        public InputField MinNumbersCountField { get; private set; } = new InputField(new Vector2(30, 3));
        public InputField MaxNumbersCountField { get; private set; } = new InputField(new Vector2(30, 5));
        public InputField MinNumberRangeField { get; private set; } = new InputField(new Vector2(30, 7));
        public InputField MaxNumberRangeField { get; private set; } = new InputField(new Vector2(30, 9));

        private void Init()
        {
            CountFilesField.DefaultText = "Количество файлов: ";
            MinNumbersCountField.DefaultText = "Мин количество чисел в файле: ";
            MaxNumbersCountField.DefaultText = "Макс количество чисел в файле: ";
            MinNumberRangeField.DefaultText = "Мин число: ";
            MaxNumberRangeField.DefaultText = "Макс число: ";
        }

        public void Update()
        {
            EnteredCountFiles?.Invoke(CountFilesField.GetLine());
            EnteredMinNumbersCount?.Invoke(MinNumbersCountField.GetLine());
            EnteredMaxNumbersCount?.Invoke(MaxNumbersCountField.GetLine());
            EnteredMinNumberRange?.Invoke(MinNumberRangeField.GetLine());
            EnteredMaxNumberRange?.Invoke(MaxNumberRangeField.GetLine());
        }
    }
}
