using System;
using UniqNumbers.DataHandler;
using UniqNumbers.Infomation.Presenter;

namespace UniqNumbers.Menu.Controller.Type
{
    class MainMenu : MenuController
    {
        private static readonly string _title = "Главное меню";
        private static readonly IMenuController[] _items = new IMenuController[]
        {
            new PathMenu(),
            new ParametersMenu(),
            new GeneratorFilesMenu(),
            new DataHandlerMenu(),  
            new ExitMenu()
        };
        private FileManager _fileManagerModel;
        private ArrayHandler _handler;
        private ParametersPresenter _parametersPresenter;

        private MenuController _selectedItem;

        public MainMenu(FileManager fileManagerModel, ArrayHandler handler, ParametersPresenter parametersPresenter) : base(_title, _items)
        {
            _fileManagerModel = fileManagerModel;
            _handler = handler;
            _parametersPresenter = parametersPresenter;
        }

        public event Action Exited;

        public override void Confirm(IMenuController selectedItem)
        {
            _selectedItem = (MenuController)selectedItem;

            if (_selectedItem is PathMenu)
            {
                Disable();
                _selectedItem.Disabled += OnDisabled;
                _fileManagerModel.SetPath();
                _selectedItem.Enable();
            }

            if (_selectedItem is GeneratorFilesMenu)
            {
                Disable();
                _selectedItem.Disabled += OnDisabled;
                _fileManagerModel.GenerateFiles();
                _handler.GenerateData(_fileManagerModel.FileNames);
                _selectedItem.Enable();
            }

            if (_selectedItem is ParametersMenu)
            {
                Disable();
                _selectedItem.Disabled += OnDisabled;
                _parametersPresenter.Enable();
                _selectedItem.Enable();
            }

            if (_selectedItem is DataHandlerMenu)
            {
                Disable();
                _selectedItem.Disabled += OnDisabled;
                var result = _handler.Handle(_fileManagerModel.FileNames);
                _fileManagerModel.CreateResultFile(result);
                _selectedItem.Enable();
            }

            if (_selectedItem is ExitMenu)
            {
                Disable();
                Exited?.Invoke();
            }
        }

        private void OnDisabled()
        {
            _selectedItem.Disabled -= OnDisabled;
            Enable();
        }
    }
}
