using System;
using UniqNumbers.Menu.Model;
using UniqNumbers.Menu.View;

namespace UniqNumbers.Menu.Controller
{
    abstract class MenuController : IMenuController
    {
        private string _title;

        private MenuModel _menuModel;
        private MenuView _menuView;

        public MenuController(string title, IMenuController[] items)
        {
            _menuModel = new MenuModel(items);
            _menuView = new MenuView(title, items);
            _title = title;
        }

        public event Action Disabled;

        public virtual void Enable()
        {
            _menuView.Draw(_menuModel.SelectedItem);

            ControlListener.Confirmed += OnConfirmed;
            ControlListener.SelectedNext += OnSelectedNext;
            ControlListener.SelectedPrevious += OnSelectedPrevious;
        }

        public void SelectNext()
        {
            _menuModel.SelectNext();
            _menuView.Draw(_menuModel.SelectedItem);
        }

        public void SelectPrevious()
        {
            _menuModel.SelectPrevious();
            _menuView.Draw(_menuModel.SelectedItem);
        }

        public virtual void Confirm(IMenuController selectedMenuController)
        {

        }

        public virtual void Disable()
        {
            ControlListener.Confirmed -= OnConfirmed;
            ControlListener.SelectedNext -= OnSelectedNext;
            ControlListener.SelectedPrevious -= OnSelectedPrevious;

            Disabled?.Invoke();
        }

        public override string ToString()
        {
            return _title;
        }

        private void OnConfirmed()
        {
            Confirm(_menuModel.SelectedItem);
        }

        private void OnSelectedNext()
        {
            SelectNext();
        }

        private void OnSelectedPrevious()
        {
            SelectPrevious();
        }
    }
}
