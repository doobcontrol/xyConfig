using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;

namespace com.xiyuansoft.xyAppConfig
{
    public class XyAppConfig
    {
        public static string orgSettingFileName = "xyApp";
        public static string fullName = "xyApp.config";

        //public static string appDataPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        //public static string appDataPath = Environment.CurrentDirectory;
        public static string appDataPath = 
            System.IO.Path.GetDirectoryName(
            new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

        public static string fullConfigFileName = System.IO.Path.Combine(appDataPath, fullName);

        static public XmlDocument getXyConfigDoc()
        {
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            //由于直接拷贝的不支持修改，因此应由程序拷贝生成??  拷贝后状态为系统未初始化：inited=false
            if (!File.Exists(fullConfigFileName))
            {
                File.Copy(System.IO.Path.Combine(appDataPath, orgSettingFileName), fullConfigFileName);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fullConfigFileName);

            return doc;
        }

        static public XmlNode getBizObjs()
        {
            //读取bizObjs配置
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;

            //bizObjs配置
            tempNode = Root["bizObjs"];

            return tempNode;
        }

        static public XmlNode getspaMap()
        {
            //读取bizObjs配置
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;

            //bizObjs配置
            tempNode = Root["spaMap"];

            return tempNode;
        }

        static public XmlNode ReturnDatas;
        static public XmlNode getReturnDatas()
        {
            if (ReturnDatas != null)  //避免每个用户登陆时都读一次
            {
                return ReturnDatas;
            }
            //读取bizObjs配置
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;

            //bizObjs配置
            tempNode = Root["ReturnDatas"];
            ReturnDatas = tempNode;

            return tempNode;
        }

        static public void setDbConconnectionString(string ConconnectionString)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["dbConnection"];
            tempNode.Attributes["connectionString"].Value = ConconnectionString;

            doc.Save(fullConfigFileName);
        }

        static public void setDbInfo(string ConconnectionString, string DataBaseType)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["dbConnection"];
            tempNode.Attributes["connectionString"].Value = ConconnectionString;
            tempNode.Attributes["accessClass"].Value = DataBaseType;

            doc.Save(fullConfigFileName);
        }
        static public string getDbConconnectionString()
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["dbConnection"];
            return tempNode.Attributes["connectionString"].Value;
        }
        static public string getDataBaseType()
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["dbConnection"];
            return tempNode.Attributes["accessClass"].Value;
        }

        static public void setAppinited()
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["systemStatus"];
            tempNode.Attributes["inited"].Value = "true";

            doc.Save(fullConfigFileName);
        }
        static public string getAppinited()
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["systemStatus"];
            return tempNode.Attributes["inited"].Value;
        }
        
        static public string getSocketPort()
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["socket"];
            return tempNode.Attributes["port"].Value;
        }

        static public string getSystemStatus(string statusType)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["systemStatus"];
            if (tempNode.Attributes[statusType] == null)
            {
                tempNode.Attributes.Append(doc.CreateAttribute(statusType));
                tempNode.Attributes[statusType].Value = "";
                doc.Save(fullConfigFileName);
            }
            return tempNode.Attributes[statusType].Value;
        }
        static public void setSystemStatus(string statusType, string statusValue)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["systemStatus"];
            if (tempNode.Attributes[statusType] == null)
            {
                tempNode.Attributes.Append(doc.CreateAttribute(statusType));
            }
            tempNode.Attributes[statusType].Value = statusValue;
            doc.Save(fullConfigFileName);
        }

        static public Dictionary<string,string> getSystemDic(string dicType)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            tempNode = Root["systemDics"][dicType];

            Dictionary<string, string> retDic = new Dictionary<string, string>();
            foreach(XmlNode xn in tempNode.ChildNodes)
            {
                retDic.Add(xn.Attributes["id"].Value, xn.Attributes["name"].Value);
            }

            return retDic;
        }
        static public string getSystemDicSelected(string dicType)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;
            if (Root["systemDics"] == null || Root["systemDics"][dicType]==null)
            {
                return null;
            }
            tempNode = Root["systemDics"][dicType];
            return tempNode.Attributes["selected"].Value;
        }
        /// <summary>
        /// 若已存在则会被先删除
        /// </summary>
        /// <param name="newDic"></param>
        static public void setSystemDic(Dictionary<string, string> newDic, string dicType, string selected)
        {
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode systemDicsNode;
            systemDicsNode = Root["systemDics"];
            if (systemDicsNode == null)
            {
                systemDicsNode = doc.CreateElement("systemDics");
                Root.AppendChild(systemDicsNode);
            }

            XmlNode dicTypeNode= systemDicsNode[dicType];
            if (dicTypeNode == null)
            {
                dicTypeNode = doc.CreateElement(dicType);
                systemDicsNode.AppendChild(dicTypeNode);
            }
            else
            {
                dicTypeNode.RemoveAll();
            }

            ((XmlElement)dicTypeNode).SetAttribute("selected", selected);

            XmlElement tempNode;
            foreach (string id in newDic.Keys)
            {
                tempNode = doc.CreateElement("item");
                dicTypeNode.AppendChild(tempNode);
                tempNode.SetAttribute("id", id);
                tempNode.SetAttribute("name", newDic[id]);
            }

            doc.Save(fullConfigFileName);
        }



        #region winformapp

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
        static public string getDataBaseInitClasss()
        {
            return getWinformappNode()["systemStatus"].Attributes["DataBaseInitClasss"].Value;
        }

        #endregion

        static public XmlNode UserLoginHandlers;
        static public XmlNode getUserLoginHandlers()
        {
            if (UserLoginHandlers != null)  //避免每个用户登陆时都读一次
            {
                return UserLoginHandlers;
            }
            //读取bizObjs配置
            XmlDocument doc = getXyConfigDoc();

            //get root element
            System.Xml.XmlElement Root = doc.DocumentElement;
            XmlNode tempNode;

            //bizObjs配置
            tempNode = Root["UserLoginHandlers"];
            UserLoginHandlers = tempNode;

            return tempNode;
        }
    }
}
