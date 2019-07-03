using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2
{
    public class ClientWrapper
    {
        private RestClient client;
        private RestRequest request;

        public ClientWrapper(RestClient client)
        {
            this.client = client;
        }

        public RestRequest GetRequest()
        {
            request = new RestRequest(Method.GET);
            return request;
        }

        public RestRequest PostRequest(object obj)
        {
            request = new RestRequest(Method.POST);
            request.AddJsonBody(obj);
            return request;
        }

        public RestRequest DeleteRequest(string id)
        {
            request = new RestRequest("/{id}", Method.DELETE);
            request.AddUrlSegment("id", $"{id}");
            return request;
        }

        public RestRequest PutRequest(object obj)
        {
            request = new RestRequest(Method.PUT);
            request.AddJsonBody(obj);
            return request;
        }

        public IRestResponse GetResponse(ClientWrapper restClient, RestRequest request)
        {
            return restClient.Execute(request);
        }

        public IRestResponse Execute(RestRequest request)
        {
            IRestResponse resp = client.Execute(request);
            return resp;
        }

        public T GetContent<T>(IRestResponse response)
        {
            var content = response.Content;
            T deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            return deserializeObject;
        }

        public void Url(string url)
        {
            client.BaseUrl = new Uri(url);
        }

        public bool Redirects()
        {
            return client.FollowRedirects = false;
        }
    }
}

