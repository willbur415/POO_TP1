﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_TP1
{    
    public class LevelManager
    {
        private const int NB_MAX_ASTEROIDS = 12;
        private const int NB_ASTEROIDS_INCR = 3;
        private int nbAsteroids;
        private int currentLevel;
        private List<Asteroid> asteroids;
        private List<Asteroid> deadAsteroids;
        private List<EnemyShip> shipsList;

        private static LevelManager levelManager;

        public static LevelManager GetInstance()
        {
            if (levelManager == null)
            {
                levelManager = new LevelManager();
            }
            return levelManager;
        }

        private LevelManager()
        {
            
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            asteroids = new List<Asteroid>();
            deadAsteroids = new List<Asteroid>();
            shipsList = new List<EnemyShip>();
            currentLevel = 1;
            ChangeNbAsteroids();
        }

        public List<Asteroid> Asteroids
        {
            get
            {
                return asteroids;
            }
        }

        public List<Asteroid> DeadAsteroids
        {
            get
            {
                return deadAsteroids;
            }
        }

        public List<EnemyShip> ShipsList
        {
            get { return shipsList;}
            
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

        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
            }
        }

        /// <summary>
        /// Changes the level.
        /// </summary>
        public void ChangeLevel()
        {
            asteroids.Clear();
            deadAsteroids.Clear();
            currentLevel++;
            ChangeNbAsteroids();
        }

        /// <summary>
        /// Checks if level is finished.
        /// </summary>
        /// <returns></returns>
        public bool LevelFinish()
        {
            if (asteroids.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes the number of asteroids.
        /// </summary>
        private void ChangeNbAsteroids()
        {
            nbAsteroids = currentLevel + NB_ASTEROIDS_INCR;
        }
    }
}
