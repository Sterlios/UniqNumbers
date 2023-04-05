namespace UniqNumbers.Menu.Controller.Type
{
    class GeneratorFilesMenu : MenuController
    {
        private static readonly string _title = "Создать файлы";

        public GeneratorFilesMenu() : base(_title, new IMenuController[] { }) { }

        public override void Enable()
        {
            Disable();
        }
    }
}
