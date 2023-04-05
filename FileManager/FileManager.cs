using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniqNumbers.Infomation.Presenter;
using UniqNumbers.Infomation.View;
using UniqNumbers.ValueType;

namespace UniqNumbers
{
    class FileManager
    {
        private readonly int _defaultFilesCount = 100;
        private string _extension;
        private InputField _inputField;
        private string _path;
        private string _resultFileName;
        private List<string> _fileNames;
        private ParametersPresenter _parametersPresenter;

        private int _filesCount;

        public FileManager()
        {
            _fileNames = new List<string>();
            _inputField = new InputField(new Vector2(1, 8));
            _extension = ".txt";
            _resultFileName = "Result";
            _filesCount = _defaultFilesCount;
        }

        public event Action<string> ChangedPath;
        public event Action NotChangedPath;
        public event Action<List<string>> ReadFileNames;

        ~FileManager()
        {
            if(_parametersPresenter != null)
                _parametersPresenter.EnteredCountFiles -= OnEnteredCountFiles;
        }

        public void InitParametersPresenter(ParametersPresenter parametersPresenter)
        {
            _parametersPresenter = parametersPresenter;

            _parametersPresenter.EnteredCountFiles += OnEnteredCountFiles;
        }

        public List<string> FileNames
        {
            get
            {
                List<string> fullFileNames = new List<string>();

                if (_fileNames.Count > 0)
                    foreach (var fileName in _fileNames)
                        fullFileNames.Add($"{_path}/{fileName}");

                return fullFileNames;
            }
        }

        public void Enable()
        {
            SetDefaultPath();
            UpdateFilesList();
        }

        public void SetPath()
        {
            try
            {
                _inputField.Text = "Введите каталог: ";

                string path = _inputField.GetLine();

                if (!Directory.Exists(path))
                    _path = Directory.CreateDirectory(path).FullName;
                else
                    _path = new DirectoryInfo(path).FullName;

                ChangedPath?.Invoke(_path);
            }
            catch (Exception)
            {
                SetDefaultPath();
                NotChangedPath?.Invoke();
            }
            finally
            {
                UpdateFilesList();
            }
        }

        public void GenerateFiles()
        {
            if (_fileNames.Count > 0)
                DeleteTXTFiles();

            for (int i = 0; i < _filesCount; i++)
                CreateFile($"{_path}/{i}{_extension}");

            UpdateFilesList();
        }

        public void CreateResultFile(List<int> result)
        {
            StreamWriter writer = new StreamWriter($"{_path}{_resultFileName}{_extension}");

            foreach (int number in result)
                writer.WriteLine(number);

            writer.Close();
            writer.Dispose();
        }

        private void OnEnteredCountFiles(string numberText)
        {
            if (int.TryParse(numberText, out int number) && number > 0)
                _filesCount = number;
            else
                _filesCount = _defaultFilesCount;
        }

        private void CreateFile(object fileName)
        {
            FileStream file = File.Create($"{fileName}");
            file.Close();
            file.Dispose();
        }

        private void DeleteTXTFiles()
        {
            foreach (var file in FileNames)
                if (Path.GetExtension(file) == _extension)
                    File.Delete(file);
        }

        private void SetDefaultPath()
        {
            _path = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Source/").FullName;
            ChangedPath?.Invoke(_path);
        }

        private void UpdateFilesList()
        {
            List<string> fullNames = Directory.GetFiles(_path).ToList();
            _fileNames.Clear();

            foreach (var fullName in fullNames)
                if (Path.GetExtension(fullName) == _extension)
                    _fileNames.Add(Path.GetFileName(fullName));

            ReadFileNames?.Invoke(_fileNames);
        }
    }
}
