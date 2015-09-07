using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    class Scores
    {
        private static Scores score;
        private SpriteFont font;
        private Vector2 namePos;
        private Vector2 scorePos;
        private Vector2 origin;
        private GraphicsDeviceManager graphics;
        private int posCounter;
        private bool isScoreShowing;

        public static Scores GetInstance()
        {
            if (score == null)
            {
                score = new Scores();
            }

            return score;
        }

        public void Initialize(SpriteFont font, ref GraphicsDeviceManager graphics)
        {
            posCounter = 0;
            this.graphics = graphics;
            this.font = font;
            namePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2);
            scorePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 300,
                     graphics.GraphicsDevice.Viewport.Height / 2);
            isScoreShowing = false;

        }

        public void ShowScores(Dictionary<string,string> scoreList, ref SpriteBatch spriteBatch )
        {
            
            origin = font.MeasureString(scoreList.Keys.ElementAt(0));

            foreach (KeyValuePair<string, string> list in scoreList)
            {
                spriteBatch.DrawString(Scores.GetInstance().Font, list.Key, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                        graphics.GraphicsDevice.Viewport.Height / 2 + posCounter),
                                        Color.White, 0, origin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(Scores.GetInstance().Font, list.Value, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 300,
                        graphics.GraphicsDevice.Viewport.Height / 2 + posCounter),
                                        Color.White, 0, origin, 1.0f, SpriteEffects.None, 0.5f);
                posCounter += 75;
            }
            
            
        }

        public void saveToXML(string score, string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\res\XML\scorelog.xml");

            XmlNode entryNode = doc.CreateNode(XmlNodeType.Element, "Log", "");
            doc.DocumentElement.AppendChild(entryNode);

            XmlNode entryNameNode = doc.CreateNode(XmlNodeType.Element, "Player", "");
            entryNameNode.InnerText = name;
            entryNode.AppendChild(entryNameNode);

            XmlNode entryScoreNode = doc.CreateNode(XmlNodeType.Element, "Score", "");
            entryScoreNode.InnerText = score;
            entryNode.AppendChild(entryScoreNode);

            doc.Save(@"..\..\res\XML\scorelog.xml");
        }

        public SpriteFont Font
        {
            get { return font; }
        }
    }
}
