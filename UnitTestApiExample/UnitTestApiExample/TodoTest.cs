using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;

namespace UnitTestApiExample
{
    [TestClass]
    public class TodoTest
    {
        /// <summary>
        /// Helper method that gets a test user to work with
        /// We don't test anything here because it's tested elsewhere (in the UsersTest class)
        /// We just want something to test the todo functionality with
        /// </summary>
        /// <returns></returns>
        private static dynamic GetTestUser()
        {
            const string user1Uri = @"http://jsonplaceholder.typicode.com/users/1";

            var client = new WebClient();
            var responseString = client.DownloadString(user1Uri);

            return JsonConvert.DeserializeObject(responseString);
            
        }
        /// <summary>
        /// This helper method takes a uri, downloads the content and returns a dynamic object.
        /// Same code as in the other class.  Should really share this code.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static dynamic GetDynamicResponse(string uri)
        {
            var client = new WebClient();
            var responseString = client.DownloadString(uri);

            return JsonConvert.DeserializeObject(responseString);
        }

        [TestMethod]
        public void ShouldReturnMoreThanOneTodoItemForAUser()
        {
            var userToTestWith = GetTestUser();

            const string allTodos = @"http://jsonplaceholder.typicode.com/todos";

            dynamic response = GetDynamicResponse(allTodos);

            // now we have a response which is a list of all todos, we want to filter the ones out where the user id is the one we're looking for

            var userId = userToTestWith.id;

            // this next line takes our dynamic object, tells C# to treat it as a list (IEnumerable), and filter out only ones with our userId
            var todosForUser = ((IEnumerable<dynamic>)response).Where(x => x.userId == userId);

            Assert.IsTrue(todosForUser.Any());
        }
    }
}
