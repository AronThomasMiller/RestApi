using NUnit.Framework;
using RestNew2.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2.Tests
{
    public class UsersTests
    {
        private ClientWrapper rest;
        private string ApiPosts = "/Users";

        [Test]
        public void RestTestPostUser()
        {
            var ApiTest = TestDataProvider.GetData<AddApiUser>("AddUser");
            rest = new ClientCreator().CreateClient(ApiPosts);
            var request = rest.PostRequest(ApiTest);
            var response = rest.GetResponse(rest , request);
            var content = rest.GetContent<AddApiUser>(response);
            Assert.AreEqual(ApiTest.Email, content.Email);
            Assert.AreEqual(ApiTest.Name, content.Name);
            Assert.AreEqual(ApiTest.Password, content.Password);
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void RestTestPostUserLogin()
        {
            var ApiTest = TestDataProvider.GetData<LoginModel>("LoginModel");
            rest = new ClientCreator().CreateClient($"{ApiPosts}/Login");
            var request = rest.PostRequest(ApiTest);
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<ApiUser>(response);
            Assert.AreEqual(ApiTest.Email, content.Email);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public ApiUser GetToken()
        {
            var ApiTest = TestDataProvider.GetData<LoginModel>("LoginModel");
            rest = new ClientCreator().CreateClient($"{ApiPosts}/Login");
            var request = rest.PostRequest(ApiTest);
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<ApiUser>(response);
            return content;
        }

        [Test]
        public void RestTestGetUserAuth()
        {
            var Token = GetToken();
            rest = new ClientCreator().CreateClient($"{ApiPosts}/TestAuth");
            rest.Redirects();
            var request = rest.GetRequest();
            request.AddHeader("authorization", $"bearer {Token.Token}");
            var response = rest.GetResponse(rest, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
