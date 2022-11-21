using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xiyuansoft.xyConfig
{
    public interface IPersistent
    {
        string getOnePar(string parName);
        void setOnePar(string parName, string parValue);

        Dictionary<string, Dictionary<string, string>> getTabledPars(string parTableName); 
        void newTabledParsRow(
            string parTableName,
            string parRowName,
            Dictionary<string, string> parsRow);
        void editTabledParsRow(
            string parTableName,
            string parRowName,
            Dictionary<string, string> parsRow);
        void delTabledParsRow(
            string parTableName,
            string parRowName);
        void editTabledPar(
            string parTableName,
            string parRowName,
            string parName,
            string parValue);
    }
}
