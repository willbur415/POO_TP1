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
        private string[] savedScores;
        private SpriteFont font;

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
            this.font = font;
        }

        public void ShowScores(Dictionary<string,string> scoreList, ref SpriteBatch spriteBatch )
        {
            foreach (KeyValuePair<string, string> list  in scoreList)
            {
                spriteBatch.DrawString(Scores.GetInstance().Font, list.Key, GameMenu.GetInstance().PlayButtonPos,
                                       Color.White, 0, GameMenu.GetInstance().FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(Scores.GetInstance().Font, list.Value, GameMenu.GetInstance().PlayButtonPos,
                                       Color.White, 0, GameMenu.GetInstance().FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
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
