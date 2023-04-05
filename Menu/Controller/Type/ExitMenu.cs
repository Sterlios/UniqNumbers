using UniqNumbers.Menu.Controller;

namespace UniqNumbers.Menu.Controller.Type
{
    class ExitMenu : MenuController
    {
        private static readonly string _title = "Выход";

        public ExitMenu() : base(_title, new IMenuController[] { }) { }
    }
}
