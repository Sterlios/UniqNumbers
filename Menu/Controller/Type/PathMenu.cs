namespace UniqNumbers.Menu.Controller.Type
{
    class PathMenu : MenuController
    {
        private static readonly string _title = "Задать каталог";

        public PathMenu() : base(_title, new IMenuController[] { }) { }

        public override void Enable()
        {
            Disable();
        }
    }
}
