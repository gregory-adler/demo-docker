using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;

namespace mvcdocker
{
    public class SubmitController : Controller
    {
        // Create HTTP Client
        private static readonly HttpClient client = new HttpClient();
        string userID = "74";

        // GET: /submit/
        public IActionResult Index()
        {
            return View();
        }

        // POST: submit/ReceiveUser
        public async Task<HttpResponseMessage> ReceiveUser()
        {
            ArrayList userParams = new ArrayList(2);

            // Reads in Post request from angular
            using (var reader = new StreamReader(Request.Body))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    userParams.Add(line);

                }
            }
            // Posts to skidata api endpoint
            var response = await PostUser(userParams);

            // Success
            if (response.StatusCode.ToString() == "OK")
            {
                Console.WriteLine("Success");
                response.ReasonPhrase = userID;
                return response;
            }

            // Failure
            else
            {
                Console.WriteLine("Failure");
                return response;
            }
        }

        public async Task<HttpResponseMessage> PostUser(ArrayList userParams)
        {

            // Formats parameters

            string username = userParams[0].ToString();

            string p = $@"{{'Username':'{ username}','RegistrationChannel': 'Example'}}";

            var content = new StringContent(p, Encoding.UTF8, "application/json");
            //var content = new FormUrlEncodedContent(values);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.skidataus.com/user/82/v1/user"),
                Headers = {
                    { "x-api-key", "kzD8ahr7ld8/78xfCLpUY9hkNPRZq2Yx7w5MQdyLTt8=" }
                },
            };

            httpRequestMessage.Content = content;

            // API call
            var response = client.SendAsync(httpRequestMessage).Result;
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            /* debug statements
			Console.WriteLine(response);
			Console.WriteLine(responseBody);
            Console.WriteLine("Response StatusCode");
            Console.WriteLine(response.StatusCode);
            */

            // sets new user id
            userID = json.First.ToString();

            return response;
        }
    }
}
