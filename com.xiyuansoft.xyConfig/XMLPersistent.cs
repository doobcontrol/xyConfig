using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace com.xiyuansoft.xyConfig
{
    public class XMLPersistent : IPersistent
    {
        public static string rootName = "xConfig";
        public static string SignleParsNodeName = "SignlePars";

        public string fullName = "xyApp.config";
        public string appDataPath =
            System.IO.Path.GetDirectoryName(
            new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
                .LocalPath);
        public string fullConfigFileName;

        public XMLPersistent()
        {
            fullConfigFileName = System.IO.Path.Combine(appDataPath, fullName);
        }
        public void Save(XmlDocument doc)
        {
            doc.Save(fullConfigFileName);
        }
        public void Save(XmlElement xe)
        {
            Save(xe.OwnerDocument);
        }
        public XmlDocument getXyConfigDoc()
        {
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            XmlDocument doc = new XmlDocument();

            if (File.Exists(fullConfigFileName))
            {
                doc.Load(fullConfigFileName);
            }
            else
            {
                creatNewNode(doc, doc, rootName);
            }

            return doc;
        }       
        public XmlElement creatNewNode(XmlDocument doc, XmlNode pNode, string nodeName)
        {
            XmlElement newNode = doc.CreateElement(nodeName);
            pNode.AppendChild(newNode);
            Save(doc);
            return newNode;
        }
        public XmlElement get1LevelNode(string nName)
        {
            XmlDocument doc = getXyConfigDoc();
            if (doc.GetElementsByTagName(nName).Count == 0)
            {
                creatNewNode(doc, doc.GetElementsByTagName(rootName)[0] as XmlElement, nName);
            }
            return doc.GetElementsByTagName(nName)[0] as XmlElement; ;
        }
        public XmlElement getSignleParsNode()
        {
            return get1LevelNode(SignleParsNodeName);
        }
        
        #region IPersistent
        
        public string getOnePar(string parName)
        {
            XmlElement xe = getSignleParsNode();

            string retValue=null;

            if (xe.Attributes[parName] != null)
            {
                retValue = xe.GetAttribute(parName);
            }

            return retValue;
        }

        public void setOnePar(string parName, string parValue)
        {
            XmlElement xe = getSignleParsNode();
            xe.SetAttribute(parName, parValue);
            Save(xe);
        }

        #endregion
    }
}
