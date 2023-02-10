using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace com.xiyuansoft.xyConfig
{
    public class XMLPersistent : IPersistent
    {
        public static string rootName = "xConfig";
        public static string SignleParsNodeName = "SignlePars";
        public static string TableParsNodeName = "TablePars";
        public static string RowNodeNamePrefix = "r"; //行名称传入时可能为数字或其它非法字符

        public string fullName = "xyApp.config";
        public string appDataPath =
            System.IO.Path.GetDirectoryName(
            new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
                .LocalPath);
        public string fullConfigFileName;

        bool CheckSave = true;
        public XMLPersistent()
        {
            fullConfigFileName = System.IO.Path.Combine(appDataPath, fullName);
        }
        ~XMLPersistent()
        {
            if (myDoc != null && docChanged)
            {
                myDoc.Save(fullConfigFileName);
            }
            CheckSave = false;
            myDoc = null;
        }
        public void clean()
        {
            CheckSave = false;
        }

        bool docChanged = false;
        public void Save()
        {
            docChanged = true;
        }

        XmlDocument myDoc;
        public XmlDocument getXyConfigDoc()
        {
            if(myDoc == null)
            {
                if (!Directory.Exists(appDataPath))
                {
                    Directory.CreateDirectory(appDataPath);
                }

                myDoc = new XmlDocument();

                if (File.Exists(fullConfigFileName))
                {
                    myDoc.Load(fullConfigFileName);
                }
                else
                {
                    XmlElement newNode = myDoc.CreateElement(rootName);
                    myDoc.AppendChild(newNode);
                }

                //启动定期保存线程
                Thread cmdThread = new Thread(new ThreadStart(
                    () => {
                        while (CheckSave)
                        {
                            Thread.Sleep(1000);
                            if (myDoc != null && docChanged)
                            {
                                myDoc.Save(fullConfigFileName);
                                docChanged = false;
                            }
                        }
                    }
                    ));
                cmdThread.Start();
            }

            return myDoc;
        }       
        public XmlElement creatNewNode(XmlNode pNode, string nodeName)
        {
            XmlElement newNode = getXyConfigDoc().CreateElement(nodeName);
            pNode.AppendChild(newNode);
            return newNode;
        }
        public XmlElement getOrCreatNewNode(XmlElement pNode, string nodeName, bool createNew = false)
        {
            XmlElement retXe = null;
            if (pNode.GetElementsByTagName(nodeName).Count != 0)
            {
                retXe = pNode.GetElementsByTagName(nodeName)[0] as XmlElement;
            }
            else if(createNew)
            {
                retXe = creatNewNode(pNode, nodeName);
            }
            return retXe;
        }
        public XmlElement get1LevelNode(string nName)
        {
            XmlDocument doc = getXyConfigDoc();
            if (doc.GetElementsByTagName(nName).Count == 0)
            {
                creatNewNode(doc.DocumentElement, nName);
            }
            return doc.GetElementsByTagName(nName)[0] as XmlElement; ;
        }
        
        public XmlElement getSignleParsNode()
        {
            return get1LevelNode(SignleParsNodeName);
        }
        
        public XmlElement getTableParsNode()
        {
            return get1LevelNode(TableParsNodeName);
        }
        public XmlElement getParTableNode(string parTableName, bool createNew = false)
        {
            return getOrCreatNewNode(getTableParsNode(), parTableName, createNew);
        }
        public XmlElement getParTableRowNode(
            string parTableName, 
            string parTableRowName)
        {
            XmlElement parTable = getParTableNode(parTableName, true);
            return getOrCreatNewNode(parTable, parTableRowName, true);
        }

        public string removeRowNodeNamePrefix(string RowNodeName)
        {
            return RowNodeName.Substring(RowNodeNamePrefix.Length);
        }
        public string addRowNodeNamePrefix(string RowNodeName)
        {
            return RowNodeNamePrefix + RowNodeName;
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
            Save();
        }

        public Dictionary<string, Dictionary<string, string>> getTabledPars(string parTableName)
        {
            XmlElement ParTableNode = getParTableNode(parTableName);
            if (ParTableNode == null)
            {
                return null;
            }
            Dictionary<string, Dictionary<string, string>> tableParsDic 
                = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> rowParsMap;
            foreach (XmlElement rowNode in ParTableNode.ChildNodes)
            {
                rowParsMap = new Dictionary<string, string>();
                tableParsDic.Add(
                    removeRowNodeNamePrefix(rowNode.Name), 
                    rowParsMap);
                foreach(XmlAttribute xa in rowNode.Attributes)
                {
                    rowParsMap.Add(xa.Name, xa.Value);
                }
            }
            return tableParsDic;
        }

        public Dictionary<string, string> getTabledRowPars(string parTableName, string parRowName)
        {
            parRowName = addRowNodeNamePrefix(parRowName);
            XmlElement ParTableNode = getParTableNode(parTableName);
            if (ParTableNode == null)
            {
                return null;
            }
            Dictionary<string, string> rowParsMap = null;
            foreach (XmlElement rowNode in ParTableNode.ChildNodes)
            {
                if(rowNode.Name== parRowName)
                {
                    rowParsMap = new Dictionary<string, string>();
                    foreach (XmlAttribute xa in rowNode.Attributes)
                    {
                        rowParsMap.Add(xa.Name, xa.Value);
                    }
                    break;
                }
            }
            return rowParsMap;
        }

        public void newTabledParsRow(
            string parTableName, 
            string parRowName, 
            Dictionary<string, string> parsRow)
        {
            parRowName = addRowNodeNamePrefix(parRowName);
            XmlElement ParTableNode = getParTableNode(parTableName, true);
            XmlElement newTabledRowNode = creatNewNode(
                ParTableNode, 
                parRowName);
            foreach(string key in parsRow.Keys)
            {
                newTabledRowNode.SetAttribute(key, parsRow[key]);
            }
            Save();
        }

        public void editTabledParsRow(
            string parTableName, 
            string parRowName, 
            Dictionary<string, string> parsRow)
        {
            parRowName = addRowNodeNamePrefix(parRowName);
            XmlElement editTabledRowNode = getParTableRowNode(
                parTableName, 
                parRowName);
            foreach (string key in parsRow.Keys)
            {
                editTabledRowNode.SetAttribute(key, parsRow[key]);
            }
            Save();
        }

        public void delTabledParsRow(string parTableName, string parRowName)
        {
            parRowName = addRowNodeNamePrefix(parRowName);
            XmlElement ParTableNode = getParTableRowNode(
                parTableName, 
                parRowName);
            ParTableNode.ParentNode.RemoveChild(ParTableNode);
            Save();
        }

        public void editTabledPar(
            string parTableName, 
            string parRowName, 
            string parName, 
            string parValue)
        {
            parRowName = addRowNodeNamePrefix(parRowName);
            XmlElement ParTableNode = getParTableRowNode(
                parTableName, 
                parRowName);
            ParTableNode.SetAttribute(parName, parValue);
            Save();
        }

        #endregion
    }
}
