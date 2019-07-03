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
    public class PostComment
    {
        private ClientWrapper rest;
        private string ApiPosts = "/api/comments";

        public ApiUser GetToken()
        {
            var ApiTest = TestDataProvider.GetData<LoginModel>("LoginModel");
            rest = new ClientCreator().CreateClient("/Users/Login");
            var request = rest.PostRequest(ApiTest);
            var response = rest.GetResponse(rest, request);
            var content = rest.GetContent<ApiUser>(response);
            return content;
        }

        [Test]
        public void RestTestPostComment()
        {
            var token = GetToken();
            var restApi = TestDataProvider.GetData<CommentBase>("CommentBase");
            rest = new ClientCreator().CreateClient(ApiPosts);
            var request = rest.PostRequest(restApi);
            request.AddHeader("Authorization", $"Bearer {token.Token}");
            var response = rest.GetResponse(rest, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void RestTestGetCommentById()
        {
            var token = GetToken();
            var restApi = TestDataProvider.GetData<CommentBase>("CommentBase");
            rest = new ClientCreator().CreateClient($"{ApiPosts}/{restApi.PostId}");
            var request = rest.GetRequest();
            request.AddHeader("Authorization", $"Bearer {token.Token}");
            var response = rest.GetResponse(rest, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}