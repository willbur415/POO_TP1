using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_TP1
{    
    public class Level
    {
        private const int NB_MAX_ASTEROIDS = 12;
        private int nbAsteroids;

        public Level()
        {
            
        }

        public int NbAsteroids
        {
            get
            {
                return nbAsteroids;
            }
            set
            {
                nbAsteroids = value;
            }
        }
    }
}
