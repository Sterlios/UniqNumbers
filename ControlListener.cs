using System;
using UniqNumbers.Menu.Controller.Type;

namespace UniqNumbers
{
    class ControlListener
    {
        private bool _isWork;
        private MainMenu _mainMenu;

        public ControlListener(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
            _mainMenu.Exited += OnExited;
        }

        public static Action SelectedNext;
        public static Action SelectedPrevious;
        public static Action Confirmed;

        public void Run()
        {
            _isWork = true;

            while (_isWork)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == KeyConfig.DownCommand)
                    SelectedNext?.Invoke();

                if (key == KeyConfig.UpCommand)
                    SelectedPrevious?.Invoke();

                if (key == KeyConfig.ConfirmCommand)
                    Confirmed?.Invoke();

                if (key == KeyConfig.ExitCommand)
                    _isWork = false;
            }
        }

        private void OnExited()
        {
            _isWork = false;
        }
    }
}
