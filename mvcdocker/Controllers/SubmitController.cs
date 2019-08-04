using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mvcdocker
{
    public class SubmitController : Controller
    {
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
            using (var reader = new StreamReader(Request.Body))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    userParams.Add(line);

                }
            }
            var response = await PostUser(userParams);

            if (response.StatusCode.ToString() == "OK"){
                Console.WriteLine("Success");
                // Receive Success back to angular
                // var angularCall = await ReturnUser("Success");
                return response;
            }
            else
            {
                Console.WriteLine("Failure");
                // Return Failure back to angular
                // var angularCall = await ReturnUser("Failure");
                return response;
            }
        }

        public async Task<HttpResponseMessage> PostUser(ArrayList userParams)
        {

            var values = new Dictionary<string, string>
            {
                 { "Username", userParams[0].ToString()},
                 { "EmailAddress", userParams[1].ToString() },
                 { "Password", userParams[2].ToString() },
                 { "ReferredBy",  "a" },
                 { "RegistrationChannel", "the"  },
                 { "CreateRegistrationToken" , "No" },
                 {"CodeChallenge", "yes" },
                 { "SendRegistrationEmail", "No" }
             };


            foreach (KeyValuePair<string, string> pair in values)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("API CALL");

            var response = await client.PostAsync("https://developer.skidata-loyalty.com/user/82/v1/user", content);

            // var response = await client.PostAsync("https://testportal.skidataus.com/user/82/v1/user", content);

            Console.WriteLine("response");

            Console.WriteLine(response);

            // string responseBody = await response.Content.ReadAsStringAsync();
            // Console.WriteLine("content");
            //  Console.WriteLine(responseBody);

            Console.WriteLine("Response StatusCode");
            Console.WriteLine(response.StatusCode);

            return response;

        }
    }
}
