using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTestApiExample
{
    [TestClass]
    public class UsersTest
    {
        /// <summary>
        /// This helper method takes a uri, downloads the content and returns a dynamic object
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static dynamic GetDynamicResponse(string uri)
        {
            var client = new WebClient();
            var responseString = client.DownloadString(uri);

            return JsonConvert.DeserializeObject(responseString);
        }

        /// <summary>
        /// Example of a test method.  Gets the data from one user and makes sure the username is correct
        /// </summary>
        [TestMethod]
        public void ShouldReturnCorrectUserName()
        {
            const string user2Uri = @"http://jsonplaceholder.typicode.com/users/2";

            dynamic response = GetDynamicResponse(user2Uri);

            Assert.AreEqual("Antonette", response.username.ToString());
        }

        /// <summary>
        /// Example of a test method.  Gets the data for all users and makes sure we get the right number back
        /// </summary>
        [TestMethod]
        public void ShouldReturnCorrectNumberOfUsers()
        {
            const string allUsersUri = @"http://jsonplaceholder.typicode.com/users";

            dynamic response = GetDynamicResponse(allUsersUri);

            Assert.AreEqual(10, response.Count);
        }
    }
}
