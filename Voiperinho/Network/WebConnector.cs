using System;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text;
using Voiperinho.Models;
using System.Collections.Generic;
using System.IO;

namespace Voiperinho.Network
{
    public static class WebConnector
    {
        private static WebClient webClient;
        private const string baseUrl = "http://thedevspot.xyz:90/user/";
        private const string baseRequestUrl = "http://thedevspot.xyz:90/requests";
        private const string baseContactsUrl = "http://thedevspot.xyz:90/contacts";

        private static string SendData(string url, NameValueCollection postData) 
        {
            string jsonString = string.Empty;

            try
            {
                using (webClient = new WebClient())
                {
                    if (postData != null)
                    {
                        byte[] result = webClient.UploadValues(url, postData);
                        jsonString = Encoding.UTF8.GetString(result);
                    }
                    else
                    {
                        jsonString = webClient.DownloadString(url);
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = (HttpWebResponse)ex.Response;

                    if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                    {
                        MemoryStream ms = new MemoryStream();
                        ex.Response.GetResponseStream().CopyTo(ms);

                        byte[] data = ms.ToArray();
                        jsonString = Encoding.UTF8.GetString(data);
                    }
                }

                Console.WriteLine("An error occurred while fetching server data: " + ex.Message);
            }

            return jsonString;
        }

        public static ContactsInformation GetContactsData(int accountId)
        {
            string data = SendData(baseUrl + accountId + "/contacts", null);
            ContactsInformation contacts = null;

            try 
	        {
                 contacts = JsonConvert.DeserializeObject<ContactsInformation>(data);		
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine("An error occured while parsing received JSON data: " + ex.Message);
            }

            return contacts;
        }

        public static List<RequestInformation> GetAvailableRequests(int accountId)
        {
            string data = SendData(baseUrl + accountId + "/requests", null);
            BaseResponseList<RequestInformation> requests = null;

            try
            {
                requests = JsonConvert.DeserializeObject<BaseResponseList<RequestInformation>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while parsing received requets JSON data: " + ex.Message);
            }

            return requests.Data;
        }

        public static List<AccountInformation> GetAvailableUsers(string username, int requesterID)
        {
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("username", username);
            postParams.Add("id", requesterID.ToString());

            string data = SendData(baseUrl + "list", postParams);
            BaseResponseList<AccountInformation> users = null;

            try
            {
                users = JsonConvert.DeserializeObject<BaseResponseList<AccountInformation>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while parsing received JSON data: " + ex.Message);
            }

            if (users.Status != 200)
            {
                Console.WriteLine(users.ErrorMessage);
                return null;
            }

            return users.Data;
        }

        public static bool SendRequest(string requestText, int requesterId, int userId)
        {
            string data = string.Empty;
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("userId", userId.ToString());
            postParams.Add("requesterId", requesterId.ToString());
            postParams.Add("requestText", requestText);

            data = SendData(baseRequestUrl + "/insert", postParams);

            BaseResponse<string> response = null;

            if (data != string.Empty)
            {
                try
                {
                    response = JsonConvert.DeserializeObject<BaseResponse<string>>(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured while parsing received request JSON data: " + ex.Message);
                    return false;
                }
            }

            if (response != null && response.Status == 200)
            {
                return true;
            }

            return false;
        }

        public static bool DeleteRequest(int requestId)
        {
            string data = string.Empty;
            BaseResponse<string> response = null;
            NameValueCollection postParams = new NameValueCollection();

            postParams.Add("request_id", requestId.ToString());

            data = SendData(baseRequestUrl + "/edit", postParams);

            if (data != string.Empty)
            {
                try
                {
                    response = JsonConvert.DeserializeObject<BaseResponse<string>>(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured while parsing received request deletion JSON data: " + ex.Message);
                    return false;
                }
            }

            if (response != null && response.Status == 200) return true;

            return false;
        }

        public static bool AddContact(int requester, int contact)
        {
            string data = string.Empty;
            NameValueCollection postParams = new NameValueCollection();
            BaseResponse<string> response = null;

            postParams.Add("userId", requester.ToString());
            postParams.Add("contactId", contact.ToString());

            data = SendData(baseContactsUrl + "/insert", postParams);

            if (data != string.Empty)
            {
                try 
	            {
                    response = JsonConvert.DeserializeObject<BaseResponse<string>>(data);		
	            }
	            catch (Exception ex)
	            {
		            Console.WriteLine("An error occured while parsing received contacts insert JSON data: " + ex.Message);
                    return false;
	            }
            }

            return false;
        }
    }
}
