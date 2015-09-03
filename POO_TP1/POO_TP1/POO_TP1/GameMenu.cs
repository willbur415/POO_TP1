using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_TP1
{
    class GameMenu
    {
        private static GameMenu menu;

        public static GameMenu GetInstance()
        {
            if (menu == null)
            {
                menu = new GameMenu();
            }

            return menu;
        }

        public void CallMenu()
        {

        }
    }
}
