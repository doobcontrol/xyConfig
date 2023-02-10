using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xiyuansoft.xyConfig
{
    public class xConfig
    {
        public static IPersistent Persistent = new XMLPersistent();
        public static string getOnePar(string parName, string defaultValue)
        {
            string retStr = defaultValue;

            string tStr = Persistent.getOnePar(parName);
            if (tStr != null)
            {
                retStr = tStr;
            }

            return retStr;
        }
        public static string getOnePar(string parName)
        {
            return Persistent.getOnePar(parName);
        }
        public static void setOnePar(string parName, string parValue)
        {
            Persistent.setOnePar(parName, parValue);
        }


        public static Dictionary<string, Dictionary<string, string>> 
            getTabledPars(string parTableName)
        {
            return Persistent.getTabledPars(parTableName);
        }
        public static Dictionary<string, string>
            getTabledRowPars(string parTableName, string parRowName)
        {
            return Persistent.getTabledRowPars(parTableName, parRowName);
        }


        public static void newTabledParsRow(
            string parTableName,
            string parRowName,
            Dictionary<string, string> parsRow)
        {
            Persistent.newTabledParsRow(parTableName, parRowName, parsRow);
        }
        public static void editTabledParsRow(
            string parTableName,
            string parRowName,
            Dictionary<string, string> parsRow)
        {
            Persistent.editTabledParsRow(parTableName, parRowName, parsRow);
        }
        public static void editTabledPar(
            string parTableName,
            string parRowName,
            string parName,
            string parValue)
        {
            Persistent.editTabledPar(
                parTableName,
                parRowName,
                parName,
                parValue);
        }
        public static void delTabledParsRow(
            string parTableName,
            string parRowName)
        {
            Persistent.delTabledParsRow(parTableName, parRowName);
        }

        public static void clean()
        {
            Persistent.clean();
        }
    }
}
