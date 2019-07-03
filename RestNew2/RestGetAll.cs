using NUnit.Framework;
using RestNew2.ApiModels;
using RestNew2.ApiTestData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2
{
    public class RestGetAll
    {
        private string ApiPosts = "/api/posts";

        [SetUp]
        public void Test()
        {
            PostInfoRewrite.Rewrite();
        }

        [Test]
        public void RestTestGetAll()
        {
            List<PostInfo> GetAll;
            using (StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\ApiTestData\\testdata.GetAll.json"))
            {
                string json = streamReader.ReadToEnd();
                GetAll = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PostInfo>>(json);
            }
            var rest = new ClientCreator().CreateClient(ApiPosts);
            var request = rest.GetRequest();
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<List<PostInfo>>(response);
            CollectionAssert.AreEqual(content, GetAll);
            Assert.That(response.IsSuccessful);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

