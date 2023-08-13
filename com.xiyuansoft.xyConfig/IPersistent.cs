using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xiyuansoft.xyConfig
{
    public interface IPersistent
    {
        void clean();
        string getOnePar(string parName);
        void setOnePar(string parName, string parValue);

        Dictionary<string, Dictionary<string, string>> getList(string parListName);
        void editListPar(string parListName, Dictionary<string, string> parsRow);
        void editListPar(string parListName, string parsRowID, string parName, string parValue);
        void delListPar(string parListName, string parsRowID);

        Dictionary<string, Dictionary<string, string>> getTabledPars(string parTableName);
        Dictionary<string, string> getTabledRowPars(string parTableName, string parRowName);
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
