using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2
{
    public class ClientCreator
    {
        private string BaseUrl = "http://localhost:5000";

        public ClientWrapper CreateClient (string partUrl)
        {
            var client = new RestClient(BaseUrl + partUrl);
            return new ClientWrapper(client);
        }
    }
}
