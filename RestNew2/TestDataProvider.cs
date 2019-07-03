using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2
{
    public class TestDataProvider
    {
        public static T GetData<T>(string model) where T : new()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var info = new FileInfo(path);
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            var dataFile = new FileInfo(dir + $"\\ApiTestData\\testdata.{model}.json");
            if (dataFile.Exists)
            {
                T testData;
                using (StreamReader streamReader = new StreamReader(Convert.ToString(dataFile), Encoding.UTF8))
                {
                    string json = streamReader.ReadToEnd();
                    testData = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                }
                return testData;
            }
            else
            {
                throw new FileNotFoundException(dataFile.FullName);
            }
        }
    }
}
