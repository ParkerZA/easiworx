using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using easiplan.app.Extensions;
using Finx.App;
using Finx.App.Models;
using my.domain.lib.core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace easiplan.app.Services
{
    public class LicenseIntegration
    {       
        private static readonly HttpClient _client =new HttpClient();
               
        public LicenseIntegration()
        {
            _client.BaseAddress = new Uri(Global.gLicenseUrl); ;

            //Set TLS protocols for windows7 machines
            System.Net.ServicePointManager.SecurityProtocol =
                       SecurityProtocolType.Tls |
                       SecurityProtocolType.Tls11 |
                       SecurityProtocolType.Tls12;

            ServicePointManager.FindServicePoint(_client.BaseAddress).ConnectionLeaseTimeout = 10 * 1000;
        }
        public LicenseKeyModel Activate(string machineCode, string licenseKey)
        {
            var model = new LicenseActivation
            {
                MachineCode = machineCode,
                LicenseKey = licenseKey
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

                var response = _client.PostAsync("licenses/activate", content).Result;

                var responseBody = response.Content.ReadAsStreamAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new MyValidationException("The licensing service could not be found or is unavailable. Please try again later.");

                    var msg = response.Content.ReadAsStringAsync().Result;
                    var message = response.Content.ReadContentAsJson<HttpResponseError>().Result;

                    throw new MyValidationException(message.Message);
                }

                return response.Content.ReadContentAsJson<LicenseKeyModel>().Result;
            }
            catch(AggregateException agx)
            {
                Console.WriteLine(agx);
                throw new MyValidationException("The licensing service could not be found or is unavailable. Please try again later.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new MyValidationException(exception.Message);
            }
        }
        public LicenseKeyModel Validate(string machineCode, string licenseKey)
        {
            var model = new LicenseActivation
            {
                MachineCode = machineCode,
                LicenseKey = licenseKey
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

                var response = _client.GetAsync(string.Format("licenses/{0}/{1}/validate", licenseKey, machineCode)).Result;

                var responseBody = response.Content.ReadAsStreamAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new MyValidationException("The licensing service could not be found or is unavailable. Please try again later.");

                    var msg = response.Content.ReadAsStringAsync().Result;
                    var message = response.Content.ReadContentAsJson<HttpResponseError>().Result;

                    throw new MyValidationException(message.Message);
                }

                var msg1 = response.Content.ReadAsStringAsync().Result;

                return response.Content.ReadContentAsJson<LicenseKeyModel>().Result;
            }
            catch (AggregateException agx)
            {
                Console.WriteLine(agx);
                throw new MyValidationException("The licensing service could not be found or is unavailable. Please try again later.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new MyValidationException(exception.Message);
            }
        }

        //public async Task<XDocument> ActivateAsync(string machineCode, string licenseKey)
        //{
        //    var model = new LicenseActivation
        //    {
        //        MachineCode = machineCode,
        //        LicenseKey = licenseKey
        //    };
        //    try
        //    {
        //        DefaultContractResolver contractResolver = new DefaultContractResolver
        //        {
        //            NamingStrategy = new CamelCaseNamingStrategy()
        //        };

        //        var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
        //        {
        //            ContractResolver = contractResolver,
        //            Formatting = Formatting.Indented
        //        });

        //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //        //var response = await _client.PostAsync("/licenses/activate", content);

        //        var response = await _client.GetAsync(string.Format("licenses/{0}/{1}/validate",licenseKey,machineCode));

        //        var responseBody = await response.Content.ReadAsStreamAsync();

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var message = await response.Content.ReadContentAsJson<HttpResponseError>();
        //            throw new MyValidationException(message.Message);
        //        }
        //        var incomingXml = XDocument.Load(responseBody);
        //        return incomingXml;
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception);
        //        throw new MyValidationException(exception.Message);
        //    }
        //}
        //public XDocument Activate(string machineCode, string licenseKey)
        //{
        //    var model = new LicenseActivation
        //    {
        //        MachineCode = machineCode,
        //        LicenseKey = licenseKey
        //    };
        //    try
        //    {
        //        DefaultContractResolver contractResolver = new DefaultContractResolver
        //        {
        //            NamingStrategy = new CamelCaseNamingStrategy()
        //        };

        //        var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
        //        {
        //            ContractResolver = contractResolver,
        //            Formatting = Formatting.Indented
        //        });

        //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = _client.PostAsync("licenses/activate", content).Result;               

        //        var responseBody =  response.Content.ReadAsStreamAsync().Result;

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            if (response.StatusCode == HttpStatusCode.NotFound)
        //                throw new MyValidationException("The licensing service could not be found or is unavailable. Please try again later.");

        //            var msg = response.Content.ReadAsStringAsync().Result;
        //            var message =  response.Content.ReadContentAsJson<HttpResponseError>().Result;

        //            throw new MyValidationException(message.Message);
        //        }
        //       return XDocument.Load(responseBody);
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception);
        //        throw new MyValidationException(exception.Message);
        //    }
        //}

        //public async Task<bool> ValidateAsync(string machineCode, string licenseKey)
        //{
        //    try
        //    {
        //        //var response = await _client.GetAsync(_apiUrl + "/licenses/" + licenseKey + "/" + machineCode + "/validate");
        //        var response = await _client.GetAsync(string.Format("licenses/{0}/{1}/validate", licenseKey, machineCode));

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var message = await response.Content.ReadContentAsJson<HttpResponseError>();
        //            throw new MyValidationException(message.Message);
        //        }

        //        var responseBody = await response.Content.ReadContentAsJson<LicenseActiveModel>();
        //        return false;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new MyValidationException(exception.Message);
        //    }
        //}

        //public bool Validate(string machineCode, string licenseKey)
        //{
        //    try
        //    {

        //        var response = _client.GetAsync(string.Format("licenses/{0}/{1}/validate", licenseKey, machineCode)).Result;

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var message = response.Content.ReadContentAsJson<HttpResponseError>().Result;
        //            throw new MyValidationException(message.Message);
        //        }

        //        var responseBody =  response.Content.ReadContentAsJson<LicenseActiveModel>().Result;
        //        return false;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new MyValidationException(exception.Message);
        //    }
        //}

        //public LicenseActiveModel Validate(string machineCode, string licenseKey)
        //{
        //    try
        //    {
        //        var response = _client.GetAsync(string.Format("licenses/{0}/{1}/validate", licenseKey, machineCode)).Result;

        //        var responseBody = response.Content.ReadAsStreamAsync().Result;

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            if (response.StatusCode == HttpStatusCode.NotFound)
        //                throw new MyValidationException("The licensing service could not be found or is unavailable. Please try again later.");

        //            var msg = response.Content.ReadAsStringAsync().Result;
        //            var message = response.Content.ReadContentAsJson<HttpResponseError>().Result;

        //            throw new MyValidationException(message.Message);
        //        }

        //        //var msg1 = response.Content.ReadAsStringAsync().Result;

        //        return response.Content.ReadContentAsJson<LicenseActiveModel>().Result;
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception);
        //        throw new MyValidationException(exception.Message);
        //    }
        //}
    }
}