using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2.ApiTestData
{
    public class PostInfoRewrite
    {
        public static void Rewrite()
        {
            using (StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\ApiTestData\\testdata.GetAll.json"))
            {
                string json = streamReader.ReadToEnd();
                using (StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug","") + "\\SimpleAPI\\Data\\PostInfo.jsno", false))
                {
                    streamWriter.WriteLine(json);
                }
            }
        }
    }
}
