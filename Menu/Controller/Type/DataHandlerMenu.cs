namespace UniqNumbers.Menu.Controller.Type
{
    class DataHandlerMenu : MenuController
    {
        private static readonly string _title = "Обработать данные";

        public DataHandlerMenu() : base(_title, new IMenuController[] { }) { }

        public override void Enable()
        {
            Disable();
        }
    }
}
