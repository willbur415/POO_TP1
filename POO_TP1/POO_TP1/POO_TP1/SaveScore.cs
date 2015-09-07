using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace POO_TP1
{
    class SaveScore
    {
        public SaveScore()
        {

        }

        public void saveToXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\res\XML\scorelog.xml");

            XmlNode entryNode = doc.CreateNode(XmlNodeType.Element, "Score", "");
            doc.DocumentElement.AppendChild(entryNode);

            XmlNode entryNameNode = doc.CreateNode(XmlNodeType.Element, "User", "");
            //entryNameNode.InnerText = fileName;
            entryNode.AppendChild(entryNameNode);

            XmlNode entryDateNode = doc.CreateNode(XmlNodeType.Element, "Date", "");
            entryDateNode.InnerText = DateTime.Now.ToString();
            entryNode.AppendChild(entryDateNode);

            doc.Save(@"..\..\res\XML\scorelog.xml");
        }
    }
}
