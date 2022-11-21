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


    }
}
