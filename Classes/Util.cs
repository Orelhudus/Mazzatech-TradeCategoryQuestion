using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace TradeCategoryQuestion.Classes
{
    static class Util
    {

        public static string GetApplicationPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;  

            if (Debugger.IsAttached)
                path = path.Substring(0, path.IndexOf("\\bin"));
            
            return  path ;
        }

    }
}
