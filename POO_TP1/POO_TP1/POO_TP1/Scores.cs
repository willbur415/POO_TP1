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
        private Vector2 origin;
        private GraphicsDeviceManager graphics;
        private int posCounter;

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
            this.graphics = graphics;
            this.font = font;
        }

        public void ShowScores(Dictionary<string,string> scoreList, ref SpriteBatch spriteBatch )
        {
            
            origin = font.MeasureString(scoreList.Keys.ElementAt(0));
            posCounter = 0;
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
            doc.Load(@"..\..\..\..\POO_TP1Content\scorelog.xml");
            XmlNode assetNode = doc.SelectSingleNode("XnaContent/Asset");

            XmlNode entryNode = doc.CreateNode(XmlNodeType.Element, "Item", "");
            assetNode.AppendChild(entryNode);

            XmlNode entryNameNode = doc.CreateNode(XmlNodeType.Element, "Key", "");
            entryNameNode.InnerText = name;
            entryNode.AppendChild(entryNameNode);

            XmlNode entryScoreNode = doc.CreateNode(XmlNodeType.Element, "Value", "");
            entryScoreNode.InnerText = score;
            entryNode.AppendChild(entryScoreNode);

            doc.Save(@"..\..\..\..\POO_TP1Content\scorelog.xml");
        }

        public SpriteFont Font
        {
            get { return font; }
        }
    }
}
