using System;
using UniqNumbers.DataHandler;
using UniqNumbers.Infomation.Presenter;
using UniqNumbers.Infomation.View;
using UniqNumbers.Menu.Controller.Type;

namespace UniqNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            ParametersPresenter parametersPresenter = new ParametersPresenter();
            ArrayHandler handler = new ArrayHandler();
            FileManager fileManagerModel = new FileManager();
            InformationPresenter informationPresenter = new InformationPresenter(new InformationView(), fileManagerModel, handler);
            MainMenu mainMenu = new MainMenu(fileManagerModel, handler, parametersPresenter);
            ControlListener controlListener = new ControlListener(mainMenu);

            fileManagerModel.InitParametersPresenter(parametersPresenter);
            handler.InitParametersPresenter(parametersPresenter);
            fileManagerModel.Enable();
            mainMenu.Enable();

            controlListener.Run();
        }
    }
}
