using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using POO_TP1;

namespace POO_TP1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ContentManager contentManager;
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;
        public static List<Bonus> bonusList;

        private SpriteFont font;
        private GameState gameState;
        private UI ui;
        private const float TIME_BETWEEN_SHOTS_SEC= 3;
        private bool pauseKeyDown;
        private double currentTime;
        private double recordedTime = 0;
        private Dictionary<string, string> scoreList;
        private Texture2D spacefield;
        private bool userScoreChecked;
        private bool isGameOver;
        
    
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO : ajouter la logique d’initialisation ici
            InitGraphicsMode(SCREENWIDTH, SCREENHEIGHT, false);
            base.Initialize();
            
        }

        private bool InitGraphicsMode(int width, int height, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    //if ((dm.Width == width) && (dm.Height == height))
                    //{
                    // The mode is supported, so set the buffer formats, apply changes and return
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                    //}
                }
            }
            return false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Kootenay");
            GameMenu.GetInstance().Initialize(font, ref graphics);
            ui = new UI();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameState = GameState.Menu;
            LevelManager.GetInstance().Initialize();
            spacefield = Content.Load<Texture2D>("Graphics\\background\\stars");
            PlayerShip.GetInstance().Initialize(Content.Load<Texture2D>("Graphics\\sprites\\PlayerShip"), new Vector2(SCREENWIDTH / 2, SCREENHEIGHT / 2));
            PlayerShip.GetInstance().InitBullets(Content);
            PlayerShip.GetInstance().AddObserver(ui);
            bonusList = new List<Bonus>();
            Bonus firstBonus = Factory.createBonus();
            firstBonus.AddObserver(ui);
            bonusList.Add(firstBonus);
            LoadAsteroids();
            LoadEnemyShips();
            Scores.GetInstance().Initialize(font,ref graphics);
            scoreList = Content.Load<Dictionary<string, string>>("scorelog");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.TotalMilliseconds;

            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState(PlayerIndex.One);

            CheckPauseKey(padOneState, keyboardState);

            if (gameState == GameState.InGame)
            {
                if (LevelManager.GetInstance().LevelFinish())
                {
                    ChangeLevel();
                }

                UpdatePlayer(padOneState, keyboardState);
                UpdateBullets();
                CheckPlayerCollision();
                CheckAsteroidsObserved();
                LevelManager.GetInstance().DeadAsteroids.Clear();
            }
            else if (gameState == GameState.Menu)
            {
                CheckMenuControls(ref padOneState, ref keyboardState, currentTime);
            }
            else if (gameState == GameState.Scores)
            {
                if (padOneState.Buttons.B == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                {
                    gameState = GameState.Menu;
                }
            }
            else if (gameState == GameState.GameOver)
            {
                if (!userScoreChecked)
                {
                    CheckUserScore();
                    userScoreChecked = true;
                }         
                isGameOver = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spacefield, Vector2.Zero, Color.White);

            if (gameState == GameState.InGame)
            {
                DrawAsteroids(spriteBatch);

                DrawEnemyShips(spriteBatch);

                if (PlayerShip.GetInstance().Alive)
                {
                    DrawPlayer(spriteBatch);
                }
                ui.draw(ref spriteBatch);
                DrawBonuses(spriteBatch);
            }
            else if (gameState == GameState.Menu)
            {
                GameMenu.GetInstance().DrawMenu(ref spriteBatch);     
            }
            else if (gameState == GameState.GameOver)
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2((SCREENWIDTH / 2) - 100, SCREENHEIGHT / 2), Color.White);
            }
            else if (gameState == GameState.Scores)
            {
                Scores.GetInstance().ShowScores(scoreList, ref spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        /// <summary>
        /// Checks the game menu choice.
        /// </summary>
        private void CheckGameMenuChoice()
        {
            if (GameMenu.GetInstance().SelectedItemIndex == 0 && !isGameOver)
            {
                gameState = GameState.InGame;
            }
            if (GameMenu.GetInstance().SelectedItemIndex == 1)
            {
                gameState = GameState.Scores;
            }
            if (GameMenu.GetInstance().SelectedItemIndex == 2)
            {
                Exit();
            }
        }

        /// <summary>
        /// Checks the pause key.
        /// </summary>
        /// <param name="gamePadState">State of the game pad.</param>
        /// <param name="keyboardState">State of the keyboard.</param>
        private void CheckPauseKey(GamePadState gamePadState, KeyboardState keyboardState)
        {
            bool pauseKeyDownThisFrame = (gamePadState.Buttons.Start == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape));
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (gameState != GameState.Menu)
                    gameState = GameState.Menu;
                else
                    gameState = GameState.InGame;
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }

        /// <summary>
        /// Checks the pad inputs.
        /// </summary>
        /// <param name="padState">State of the pad.</param>
        private void CheckPadInputs(GamePadState padState)
        {
            PlayerShip.GetInstance().RotationAngle += padState.ThumbSticks.Right.X / 16.0f;
            PlayerShip.GetInstance().Update(padState.ThumbSticks.Left.Y);
            if (padState.IsButtonDown(Buttons.RightTrigger)) PlayerShoot();
        }

        /// <summary>
        /// Checks the keyboard keys.
        /// </summary>
        /// <param name="keyboardState">State of the keyboard.</param>
        private void CheckKeyboardKeys(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                PlayerShip.GetInstance().Update(1.0f);
            }
            else
            {
                PlayerShip.GetInstance().Update(0);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                PlayerShip.GetInstance().Update(-1.0f);
            }
            else
            {
                PlayerShip.GetInstance().Update(0);
            }
            if (keyboardState.IsKeyDown(Keys.A)) PlayerShip.GetInstance().RotationAngle -= 0.05f;
            if (keyboardState.IsKeyDown(Keys.D)) PlayerShip.GetInstance().RotationAngle += 0.05f;
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                PlayerShoot();
            }
        }

        /// <summary>
        /// Checks the user score.
        /// </summary>
        private void CheckUserScore()
        {
            foreach (KeyValuePair<string, string> list in scoreList)
            {
                if (ui.Score > Convert.ToInt32(list.Value))
                {   //Doit demander le name avant d'enregistrer, sinon erreur de key a cause du dictionnary
                    //changement de container pour la sauvegarde de score??
                    //sinon 2 user ne pourront avoir le meme nom
                    //Scores.GetInstance().saveToXML(ui.Score.ToString(),AskUserName());
                    break;
                }
            }
        }

        /// <summary>
        /// Asks the name of the user.
        /// </summary>
        /// <returns></returns>
        private string AskUserName()
        {
            string name = "";

            return name;
        }

        /// <summary>
        /// Loads the asteroids.
        /// </summary>
        private void LoadAsteroids()
        {
            for (int i = 0; i < LevelManager.GetInstance().NbAsteroids; i++)
            {
                Asteroid ast = new Asteroid(Content.Load<Texture2D>("Graphics\\sprites\\asteroid_big"), Vector2.Zero, i, AsteroidSize.large);
                ast.AddObserver(ui);
                LevelManager.GetInstance().Asteroids.Add(ast);
            }
        }

        private void CheckAsteroidsObserved()
        {
            foreach (Asteroid ast in LevelManager.GetInstance().Asteroids)
            {
                if (!ast.IsUIObserver)
                {
                    ast.AddObserver(ui);
                    ast.IsUIObserver = true;
                }
            }
        }

        /// <summary>
        /// Loads the enemy ships.
        /// </summary>
        private void LoadEnemyShips()
        {
            EnemyShip newEnemyShip = Factory.CreateEnemyShip(TypeShip.bigBossShip, new Vector2(SCREENWIDTH, SCREENHEIGHT));
            newEnemyShip.AddObserver(ui);
            LevelManager.GetInstance().ShipsList.Add(newEnemyShip);

            for (int i = 0; i < LevelManager.GetInstance().ShipsList.Count; i++)
            {
                LevelManager.GetInstance().ShipsList[i].InitBullets(contentManager);
            }
        }

        /// <summary>
        /// make player shoot.
        /// </summary>
        private void PlayerShoot()
        {
            if (PlayerShip.GetInstance().Cooldown == 0)
            {
                PlayerShip.GetInstance().Shoot();
            }
        }

        /// <summary>
        /// Updates the player.
        /// </summary>
        /// <param name="padState">State of the pad.</param>
        /// <param name="keyboardState">State of the keyboard.</param>
        private void UpdatePlayer(GamePadState padState, KeyboardState keyboardState)
        {
            if (PlayerShip.GetInstance().IsAlive)
            {
                if (padState.IsConnected) CheckPadInputs(padState);
                else CheckKeyboardKeys(keyboardState);
            }
            else
            {
                PlayerShip.GetInstance().CheckRespawnTime();
            }
            if (PlayerShip.GetInstance().NumberOfLifes <= 0)
            {
                gameState = GameState.GameOver;
            }
        }

        /// <summary>
        /// Checks the player collision.
        /// </summary>
        private void CheckPlayerCollision()
        {
            foreach (Asteroid ast in LevelManager.GetInstance().Asteroids)
            {
                ast.Move();
                if (!PlayerShip.GetInstance().IsInvincible)
                {
                PlayerShip.GetInstance().CheckCollisionBox(ast);
                }
            }
            
            foreach (EnemyShip list in LevelManager.GetInstance().ShipsList)
            {
                list.Move();
                if (!PlayerShip.GetInstance().IsInvincible)
                {
                    PlayerShip.GetInstance().CheckCollisionBox(list);
                }
            }
            
            for (int i = 0; i < bonusList.Count; i++)
            {
                bonusList[i].CheckCollisionBox(PlayerShip.GetInstance());
            }
        }

        /// <summary>
        /// Updates the bullets.
        /// </summary>
        private void UpdateBullets()
        {
            foreach (Bullet bullet in PlayerShip.GetInstance().Bullets)
            {
                bullet.Update();
                if (bullet.IsShooted)
                {
                    //Pas un foreach parce qu'on modifie un élément de la liste qu'on incrémente
                    for (int i = 0; i < LevelManager.GetInstance().Asteroids.Count(); i++)
                    {
                        bullet.CheckCollisionBox(LevelManager.GetInstance().Asteroids[i]);
                    }
                    for (int i = 0; i < LevelManager.GetInstance().ShipsList.Count(); i++)
                    {
                        bullet.CheckCollisionBox(LevelManager.GetInstance().ShipsList[i]);
                        if (LevelManager.GetInstance().ShipsList[i].EnemyHP <= 0)
                        {
                            LevelManager.GetInstance().ShipsList.Remove(LevelManager.GetInstance().ShipsList[i]);
                        }
                    }

                    foreach(Asteroid deadAst in LevelManager.GetInstance().DeadAsteroids)
                    {
                        if (LevelManager.GetInstance().Asteroids.Contains(deadAst))
                        {
                            LevelManager.GetInstance().Asteroids.Remove(deadAst);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws the asteroids.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        private void DrawAsteroids(SpriteBatch spriteBatch)
        {
            foreach (Asteroid ast in LevelManager.GetInstance().Asteroids)
            {
                spriteBatch.Draw(ast.Image, ast.Boundings, Color.Red);
                spriteBatch.Draw(ast.Image, ast.Position, Color.White);
            }
        }

        /// <summary>
        /// Draws the bonuses.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        private void DrawBonuses(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < bonusList.Count; i++)
            {
                if (bonusList[i].Type != BonusType.none)
                {
                    spriteBatch.Draw(bonusList[i].Image, bonusList[i].Position, Color.White);
                }
            }
        }

        /// <summary>
        /// Draws the enemy ships.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        private void DrawEnemyShips(SpriteBatch spriteBatch)
        {
            foreach (EnemyShip ships in LevelManager.GetInstance().ShipsList)
            {
                spriteBatch.Draw(ships.Image,ships.Position,Color.White);
            }
        }

        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        private void DrawPlayer(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in PlayerShip.GetInstance().Bullets)
            {
                spriteBatch.Draw(bullet.Image, bullet.Position, Color.White);
            }
            
            PlayerShip.GetInstance().Draw(ref spriteBatch);
        }

        /// <summary>
        /// Checks the menu controls.
        /// </summary>
        /// <param name="padState">State of the pad.</param>
        /// <param name="keyboardState">State of the keyboard.</param>
        /// <param name="currentTime">The current time.</param>
        private void CheckMenuControls(ref GamePadState padState, ref KeyboardState keyboardState, double currentTime)
        {
           
            if (padState.DPad.Down == ButtonState.Pressed || padState.DPad.Up == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.Up))
            {
                if (currentTime > recordedTime + 150)
                {
                    GameMenu.GetInstance().UpdateMenu(ref padState, ref keyboardState);
                    recordedTime = currentTime;
                }
            }
            if (padState.Buttons.A == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Enter))
            {
                CheckGameMenuChoice();
            }
        }

        /// <summary>
        /// Changes the level.
        /// </summary>
        private void ChangeLevel()
        {
            LevelManager.GetInstance().ChangeLevel();
            PlayerShip.GetInstance().ResetPosition();
            LoadAsteroids();
            LoadEnemyShips();
            Bonus newBonus = Factory.createBonus();
            newBonus.AddObserver(ui);
            bonusList.Add(newBonus);
        }
    }
}
