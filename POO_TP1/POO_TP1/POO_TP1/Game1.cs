using System;
using System.Collections.Generic;
using System.Linq;
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
        private SpriteFont font;
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;
        private GameState gameState;
        private const float TIME_BETWEEN_SHOTS_SEC= 3;
        private bool pauseKeyDown;
        private double currentTime;
        private double recordedTime = 0;
        private Dictionary<string, string> scoreList;
        private Bonus bonus;
        private Factory facto;

        private Texture2D spacefield;

        private Random rand;


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
            UI.GetInstance().Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameState = GameState.Menu;
            LevelManager.GetInstance().Initialize();
            facto = new Factory(Content);
            spacefield = Content.Load<Texture2D>("Graphics\\background\\stars");
            PlayerShip.GetInstance().Initialize(Content.Load<Texture2D>("Graphics\\sprites\\PlayerShip"), new Vector2(SCREENWIDTH / 4, SCREENHEIGHT / 2));
            PlayerShip.GetInstance().InitBullets(Content);
            bonus = Factory.createBonus(BonusType.slowDown);
            loadAsteroids();
            //scoreList = Content.Load<Dictionary<string, string>>("scorelog");
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
                    changeLevel();    
                }
                updatePlayer(padOneState, keyboardState);
                updateBullets();
                updateAsteroids();
                LevelManager.GetInstance().DeadAsteroids.Clear();
            }
            else if (gameState == GameState.Menu)
            {
                checkMenuControls(ref padOneState, ref keyboardState, currentTime);
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

                drawAsteroids(spriteBatch);

                if (PlayerShip.GetInstance().Alive)
                {
                    drawPlayer(spriteBatch);
                }
                UI.GetInstance().draw(ref spriteBatch);
            }
            else
            {
                GameMenu.GetInstance().DrawMenu(ref spriteBatch);     
            }

            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        private void CheckGameMenuChoice()
        {
            if (GameMenu.GetInstance().SelectedItemIndex == 0)
            {
                gameState = GameState.InGame;
            }
            if (GameMenu.GetInstance().SelectedItemIndex == 1)
            {

            }
            if (GameMenu.GetInstance().SelectedItemIndex == 2)
            {
                Exit();
            }
        }

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

        private void CheckPadInputs(GamePadState padState)
        {
            PlayerShip.GetInstance().RotationAngle += padState.ThumbSticks.Right.X / 16.0f;
            PlayerShip.GetInstance().Update(padState.ThumbSticks.Left.Y);
            if (padState.IsButtonDown(Buttons.RightTrigger)) playerShoot();
        }

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
                playerShoot();
            }
        }

        private void loadAsteroids()
        {
            rand = new Random();
            for (int i = 0; i < LevelManager.GetInstance().NbAsteroids; i++)
            {
                Asteroid ast = new Asteroid(Content.Load<Texture2D>("Graphics\\sprites\\asteroid_big"), Vector2.Zero, i, AsteroidSize.large);
                LevelManager.GetInstance().Asteroids.Add(ast);
                ast.AddObserver(UI.GetInstance());
            }
        }

        private void playerShoot()
        {
            if (PlayerShip.GetInstance().Cooldown == 0)
            {
                PlayerShip.GetInstance().Shoot();
            }
        }

        private void updatePlayer(GamePadState padState, KeyboardState keyboardState)
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
        }

        private void updateAsteroids()
        {
            foreach (Asteroid ast in LevelManager.GetInstance().Asteroids)
            {
                ast.Move();
                PlayerShip.GetInstance().CheckCollisionBox(ast);
            }
        }

        private void updateBullets()
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

        private void drawAsteroids(SpriteBatch spriteBatch)
        {
            foreach (Asteroid ast in LevelManager.GetInstance().Asteroids)
            {
                spriteBatch.Draw(ast.Image, ast.Position, Color.White);
            }
        }

        private void drawPlayer(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in PlayerShip.GetInstance().Bullets)
            {
                spriteBatch.Draw(bullet.Image, bullet.Position, Color.White);
            }
            spriteBatch.Draw(PlayerShip.GetInstance().Image, PlayerShip.GetInstance().Position, null, Color.White, PlayerShip.GetInstance().RotationAngle, PlayerShip.GetInstance().Offset, 1.0f, SpriteEffects.None, 0f);
        }

        private void checkMenuControls(ref GamePadState padState, ref KeyboardState keyboardState, double currentTime)
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

        private void changeLevel()
        {
            LevelManager.GetInstance().ChangeLevel();
            PlayerShip.GetInstance().ResetPosition();
            loadAsteroids();
        }
    }
}
