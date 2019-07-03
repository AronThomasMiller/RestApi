using NUnit.Framework;
using RestNew2.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2
{
    public class PostsTests
    {
        private ClientWrapper rest;
        private string ApiPosts = "/api/posts";

        [Test]
        public void RestTestPost()
        {
            var restApi = TestDataProvider.GetData<BasePost>("BasePost");
            rest = new ClientCreator().CreateClient(ApiPosts);
            var request = rest.PostRequest(restApi);
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<BasePost>(response);
            Assert.AreEqual(restApi.Text, content.Text);
            Assert.AreEqual(restApi.Title, content.Title);
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
        }

        [Test]
        public void RestTestGetById()
        {
            var restApi = TestDataProvider.GetData<PostInfo>("PostInfo");
            rest = new ClientCreator().CreateClient($"{ApiPosts}/{restApi.Id}");
            var request = rest.GetRequest();
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<PostInfo>(response);
            Assert.AreEqual(restApi.Id, content.Id);
            Assert.AreEqual(restApi.Rate, content.Rate);
            Assert.AreEqual(restApi.Text, content.Text);
            Assert.AreEqual(restApi.Title, content.Title);
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void RestTestPut()
        {
            var RestApi = TestDataProvider.GetData<PostInfo>("PutInfo");
            rest = new ClientCreator().CreateClient(ApiPosts);
            var request = rest.PutRequest(RestApi);
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<PostInfo>(response);
            Assert.AreEqual(RestApi.Id, content.Id);
            Assert.AreEqual(RestApi.Rate, content.Rate);
            Assert.AreEqual(RestApi.Text, content.Text);
            Assert.AreEqual(RestApi.Title, content.Title);
            Assert.That(response.IsSuccessful);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void RestTestDelete()
        {
            var RestApi = TestDataProvider.GetData<PostInfo>("PostInfo");
            rest = new ClientCreator().CreateClient(ApiPosts);
            var request = rest.DeleteRequest(RestApi.Id);
            var response = rest.GetResponse(rest, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.IsSuccessful);
        }

        [Test]
        public void RestTestPostRate()
        {
            var RestApi = TestDataProvider.GetData<BasePostRate>("RatePost");
            rest = new ClientCreator().CreateClient($"{ApiPosts}/rate");
            var request = rest.PostRequest(RestApi);
            var response = rest.GetResponse(rest, request);
            Assert.That(response.IsSuccessful);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void RestTestAvgRate()
        {
            var RestApi = TestDataProvider.GetData<BasePostRate>("RatePost");
            rest = new ClientCreator().CreateClient($"{ApiPosts}/rate");
            var request = rest.PostRequest(RestApi);
            var response = rest.GetResponse(rest, request);

            var RestApi1 = TestDataProvider.GetData<BasePostRate>("RatePost");
            var rest1 = new ClientCreator().CreateClient($"{ApiPosts}/rate");
            var request1 = rest1.PostRequest(RestApi1);
            var response1 = rest1.GetResponse(rest1, request1);

            var RestApi2 = TestDataProvider.GetData<PostInfo>("PostInfo");
            var rest2 = new ClientCreator().CreateClient($"{ApiPosts}/{RestApi2.Id}");
            var request2 = rest2.GetRequest();
            var response2 = rest.GetResponse(rest2, request2);
            var result = (Convert.ToInt32(RestApi.Rate) + Convert.ToInt32(RestApi1.Rate)) / 2;
            PostInfo content = rest2.GetContent<PostInfo>(response1);
            Assert.AreEqual(result, content.Rate);
        }
    }
}
