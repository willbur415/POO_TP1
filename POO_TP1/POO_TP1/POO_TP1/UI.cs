using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    class UI
    {
        private static UI ui;
        private int numberOfLife;
        private int score;

        public static UI GetInstance()
        {
            if (ui == null)
            {
                ui = new UI();
            }
            return ui;
        }

        public void Initialize()
        {
            this.numberOfLife = 3;
            this.score = 3;
        }

        
    }
}
