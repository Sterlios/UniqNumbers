using System;
using System.Collections.Generic;
using UniqNumbers.DataHandler;
using UniqNumbers.Infomation.View;

namespace UniqNumbers.Infomation.Presenter
{
    class InformationPresenter
    {
        private InformationView _view;
        private FileManager _fileManagerModel;
        private ArrayHandler _handler;

        public InformationPresenter(InformationView view, FileManager fileManagerModel, ArrayHandler handler)
        {
            _view = view;
            _fileManagerModel = fileManagerModel;
            _handler = handler;

            _handler.CalculatedTime += OnCalculatedTime;
            _fileManagerModel.ChangedPath += OnChangedPath;
            _fileManagerModel.NotChangedPath += OnNotChangedPath;
            _fileManagerModel.ReadFileNames += OnReadFileNames;
        }

        ~InformationPresenter()
        {
            _handler.CalculatedTime -= OnCalculatedTime;
            _fileManagerModel.ChangedPath -= OnChangedPath;
            _fileManagerModel.NotChangedPath -= OnNotChangedPath;
            _fileManagerModel.ReadFileNames -= OnReadFileNames;
        }

        private void OnChangedPath(string path)
        {
            _view.PathView.Text = $"Каталог:\n" +
                $"{path}";

            _view.Update();
        }

        private void OnNotChangedPath()
        {
            _view.MessageView.Text = $"Не удалось выбрать указанный каталог. " +
                $"Каталог установлен по умолчанию";

            _view.Update();
        }

        private void OnReadFileNames(List<string> fileNames)
        {
            _view.FileNamesView.Text = $"Файлы:\n";

            foreach (string fileName in fileNames)
                _view.FileNamesView.Text += $"{fileName}\n";

            _view.Update();
        }

        private void OnCalculatedTime(TimeSpan time)
        {
            _view.DiagnosticView.Text = $"Время выполнения: {time}";

            _view.Update();
        }
    }
}
