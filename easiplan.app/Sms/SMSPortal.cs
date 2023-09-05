using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using easiplan.app.Extensions;
using easiplan.domain;
using easiplan.domain.Entities;
using Newtonsoft.Json;

namespace Finx.App.Sms
{
    public class SmsPortal
    {
        public SmsConfiguration smsConfig { get; set; }
        public SmsToken smsToken { get; set; }
        public SmsPortal(SmsConfiguration smsConfiguration)
        {

            smsConfig = smsConfiguration;
            smsToken = new SmsToken();
        }

        public BalanceResult GetBalance()
        {            
           
            try
            {
                smsToken.Authenticate(smsConfig);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(smsConfig.baseRestUri + "Balance");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add("Authorization:Bearer "+ smsToken.Token);

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                   
                    var result = streamReader.ReadToEnd();
                   
                    return result.FromJson<BalanceResult>();
                }
            }catch(Exception ex) {
                throw;
            }
           
        }

        public void SendBulkMessages(SmsSendOptions sendOptions,IList<SmsMessage> messages)
        {
            try{ 
            BulkMessage Msg = new BulkMessage(sendOptions);
            Msg.messages = messages;

            var sMsg = JsonConvert.SerializeObject(Msg, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            sMsg = sMsg.Replace("\\\"", "\"").Replace("\"[", "[").Replace("]\"", "]");

            smsToken.Authenticate(smsConfig); 

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(smsConfig.baseRestUri + "BulkMessages");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + smsToken.Token);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(sMsg);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

            }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static string CreateRecipientList(string to)
        {
            string[] tmp = to.Split(',');
            to = "[\"";
            to = to + string.Join("\",\"", tmp);
            to = to + "\"]";
            return to;
        }


    }
    public class SmsConfiguration: BaseEntity<int>
    {
        string baseUri = "https://rest.smsportal.com/";

        public string ClientKey { get; set; }
        public string SecretKey { get; set; }

        string _baseRestUri;
        public string baseRestUri { get { return _baseRestUri; } set {if(!string.IsNullOrEmpty(value)) _baseRestUri=value; } }
        public string PortalLink { get; set; }
        public bool IsConfigured { get; set; }

        public SmsConfiguration()
        {
            baseRestUri = baseUri;
        }
    }
    public class SmsToken
    {
        public string Token { get; set; }
      
        public bool Authenticate(SmsConfiguration SmsConfig)
        {
			
			Token = EncodeTo64(string.Format("{0}:{1}", SmsConfig.ClientKey, SmsConfig.SecretKey));
            try
            {
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(SmsConfig.baseRestUri + "Authentication");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add("Authorization:Basic "+ Token);

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                   
                    var result = streamReader.ReadToEnd();

					SmsTokenResult tokenResult = JsonConvert.DeserializeObject<SmsTokenResult>(result);

                    Token = tokenResult.Token;

                }
                return true;
            }
            catch (Exception x1) { throw new Exception("Failed to authenticate with SMS provider using the supplied credentials."); }

        }

        string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
        string DecodeFrom64(string encodedData)

        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }

      
    }
    public class SmsSendOptions
    {
        public string senderId { get; set; } //max 11 chars
        public string duplicateCheck { get; set; } //None
        public string startDeliveryUtc { get; set; } //max 3 months in advance

        public string endDeliveryUtc { get; set; }

        /// <summary>
        /// Replies that are received as a result of
        //the send are then subject to rules configured in the reply rule set governing auto forwards and auto
        //responses
        /// </summary>
        public string replyRuleSetName { get; set; }//a rule set that must be used for the send

        public string campaignName { get; set; }//reporting purposes(100 chars)

        public string costCentre { get; set; }//reporting purposes ( 100 chars)

        public bool checkOptOuts { get; set; }//True or False - if true sms will not be sent to this number

        public SmsSendOptions()
        {

        }
    }
    public class BulkMessage
    {
        public SmsSendOptions sendOptions { get; set; }
        public IList<SmsMessage> messages { get; set; }

        public BulkMessage()
        {
            messages = new List<SmsMessage>();
        }
        public BulkMessage(SmsSendOptions SendOptions) : this()
        {
            sendOptions = SendOptions;

        }
    }
    public class SmsMessage
    {
        public string content { get; set; }
        public string destination { get; set; }
        public string customerId { get; set; }
        public string SendDate { get; set; }
    }
    public class SmsGroupMesssage
    {
        public string Message { get; set; }
        public string[] Groups { get; set; }
    }
    public class BalanceResult{
        public double Balance { get; set; }
    }
    public class SmsTokenResult{
        public string Token { get; set; }
		public string Schema { get; set; }
		public int ExpiresInMinutes { get; set; }
	}
}
