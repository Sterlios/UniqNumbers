using UniqNumbers.Menu.Controller;

namespace UniqNumbers.Menu.Controller.Type
{
    class ParametersMenu: MenuController
    {
        private static readonly string _title = "Задать параметры";

        public ParametersMenu() : base(_title, new IMenuController[] { }) { }

        public override void Enable()
        {
            Disable();
        }
    }
}
