namespace UniqNumbers.Menu.Controller
{
    interface IMenuController
    {
        void SelectNext();
        void SelectPrevious();
        void Confirm(IMenuController selectedMenuController);
    }
}
