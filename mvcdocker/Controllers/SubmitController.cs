﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvcdocker
{
    public class SubmitController : Controller
    {
        // Create HTTP Client
        private static readonly HttpClient client = new HttpClient();

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
            if (response.StatusCode.ToString() == "OK"){
                // Console.WriteLine("Success");
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
            var values = new Dictionary<string, string>
            {
                 { "Username", userParams[0].ToString()},
                 { "ReferredBy",  "0" },
                 { "RegistrationChannel", "string"  },
                 { "CreateRegistrationToken" , "true" },
                 { "CodeChallenge", "string" },
                 { "RegistrationToken", "string"},
                 { "SendRegistrationEmail", "true" },
                 { "UserProfileProperties", "None" }
            };


            var content = new FormUrlEncodedContent(values);

			var httpRequestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://api.skidataus.com/user/82/v1/user"),
				Headers = {
				{ "x-api-key", "5aKKx1iG4+V7S1qtug1DVBTPMs0u/F4sQ0Z0PBnmos8" }
			},
				Content = content
			};

			/* statement to print parameters
            foreach (KeyValuePair<string, string> pair in values)
                {
                    Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                }
            */

			// API call
			var response = client.SendAsync(httpRequestMessage).Result;

			/* Prints response, response body, and status code (debugging)
			Console.WriteLine(response);
             string responseBody = await response.Content.ReadAsStringAsync();
             Console.WriteLine("content");
             Console.WriteLine(responseBody);


			Console.WriteLine("Response StatusCode");
            Console.WriteLine(response.StatusCode);

            return response;
            */

		}
	}
}
