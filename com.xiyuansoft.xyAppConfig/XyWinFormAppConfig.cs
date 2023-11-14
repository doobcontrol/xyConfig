using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace com.xiyuansoft.xyAppConfig
{
    public class XyWinFormAppConfig
    {
        static private XmlDocument xyConfigDoc;
        static public XmlDocument getXyConfigDoc()
        {
            if (xyConfigDoc == null)
            {
                xyConfigDoc = new XmlDocument();
                try
                {
                    xyConfigDoc.Load("xyApp.config");
                }
                catch(XmlException e)
                {
                    throw new Exception("配置文件损坏", e);
                }
                catch (FileNotFoundException e)
                {
                    throw new Exception("未找到配置文件", e);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            return xyConfigDoc;
        }

        static public XmlNode getWinformappNode()
        {
            //读取bizObjs配置
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;

            //bizObjs配置
            tempNode = Root["winformapp"];

            return tempNode;
        }

        static public XmlNode getConnecter()
        {
            return getWinformappNode()["connecter"];
        }

        static public string getAppName()
        {
            return getWinformappNode()["systemStatus"].Attributes["appName"].Value;
        }
    }
}
