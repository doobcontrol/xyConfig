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


    }
}
