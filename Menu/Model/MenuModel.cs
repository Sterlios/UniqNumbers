using UniqNumbers.Menu.Controller;

namespace UniqNumbers.Menu.Model
{
    class MenuModel
    {
        private readonly IMenuController[] _items;
        private int _selectedIndex = 0;

        public MenuModel(IMenuController[] items)
        {
            _items = items;
        }

        public IMenuController SelectedItem => _items[_selectedIndex];

        public void SelectNext()
        {
            _selectedIndex++;

            if (_selectedIndex >= _items.Length)
                _selectedIndex = 0;
        }

        public void SelectPrevious()
        {
            _selectedIndex--;

            if (_selectedIndex < 0)
                _selectedIndex = _items.Length - 1;
        }
    }
}
