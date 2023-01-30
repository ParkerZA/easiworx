using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using easiplan.app.Extensions;
using easiplan.app.Models;
using easiplan.domain.Entities;
using Finx.App;
using Finx.App.Models;
using my.domain.lib.core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace easiplan.app.Services
{
    public class UserAuthenticationService
    {       
        private static readonly HttpClient _client =new HttpClient();

        private static string _jwtToken;

        public UserAuthenticationService()
        {
            _client.BaseAddress = new Uri(Global.gLicenseUrl);
            //Set TLS protocols for windows7 machines
            System.Net.ServicePointManager.SecurityProtocol =
                       SecurityProtocolType.Tls |
                       SecurityProtocolType.Tls11 |
                       SecurityProtocolType.Tls12;


            ServicePointManager.FindServicePoint(_client.BaseAddress).ConnectionLeaseTimeout = 10 * 1000;
        }
        public void Authenticate(ref User user)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new WarningException("User Name/Email is required");

            if (string.IsNullOrEmpty(user.Password))
                throw new WarningException("Password is required");

            var model = new UserAuthenticationRequestModel
            {
                username = user.Username,
                password = user.Password,
                rememberMe = true
            };
            try
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _client.PostAsync("authenticate", content).Result;

                var responseBody = response.Content.ReadAsStreamAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new MyValidationException("The Authentication Service could not be found or is unavailable. Please try again later.");

                    var msg = response.Content.ReadAsStringAsync().Result;
                    var message = response.Content.ReadContentAsJson<HttpResponseError>().Result;

                    throw new MyValidationException(message.Message);
                }

                //Get JWT Bearer token from Header 
                IEnumerable<string> headerValues = new List<string>();
                if (response.Headers.TryGetValues("Authorization", out headerValues))
                {
                    _jwtToken = headerValues.First();

                    LoggedInUserModel loggedInUser = GetLoggedInUser(_jwtToken);

                    string fullName = loggedInUser.fullName;
                    string firstName = fullName;
                    string lastName = fullName;

                    var names = fullName.Split(' ');
                    if (names.Length > 0) firstName = names[0];
                    if (names.Length > 1) lastName = names[names.Length-1];

                    user.Firstname = firstName;
                    user.Surname = lastName;
                    user.Designation = loggedInUser.role;                   
                    user.IsAdministrator = loggedInUser.role == "Company Admin";
                    //TO DO : YJ 2021-09-30 Get designations from API
                    user.Designation += ",Advisor";
                    //user.IsActive = true;
                    user.IsAdvisor = true; //user.Designation.contains("Advisor");
                    user.Status = "Active";
                    user.CompanyName = loggedInUser.companyName;

                    user.Calculate();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new MyValidationException(exception.Message);
            }
        }
        private LoggedInUserModel GetLoggedInUser(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("Authorization", token);

                var response = _client.GetAsync("users/me").Result;

                var responseBody = response.Content.ReadAsStreamAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new MyValidationException("The Authentication Service could not be found or is unavailable. Please try again later.");


                    var msg = response.Content.ReadAsStringAsync().Result;
                    var message = response.Content.ReadContentAsJson<HttpResponseError>().Result;

                    throw new MyValidationException(message.Message);
                }

                return response.Content.ReadContentAsJson<LoggedInUserModel>().Result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new MyValidationException(exception.Message);
            }
        }
    }
}