using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace APIAppX.RMS
{
    public class RMSCustomAuth
    {
        public string _server;
        private JsonData jsonData;
        private string _token;

        private string _dtAgentUrl;
        private int _sqlConnectionId;
        private string _sqlConnectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RMSRestAdapter"/> class.
        /// </summary>
        /// <param name="usr">The usr.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="server">The server.</param>
        /// <param name="tenant">The tenant.</param>
        public RMSCustomAuth()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RMSRestAdapter"/> class.
        /// </summary>
        /// <param name="usr">The usr.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="server">The server.</param>
        /// <param name="tenant">The tenant.</param>
        public RMSCustomAuth(string usr, string pwd, string server, string tenant)
        {
           
            jsonData = new JsonData();
            jsonData.username = usr;
            jsonData.password = pwd;
            jsonData.tenant = tenant;
            _server = server;

            GenerateToken();
        }

        private void GenerateToken()
        {
            System.Net.WebRequest httpWebRequest = generateWebRequest("ENTER RESTFUL URL");
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(jsonData));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    JsonToken result = JsonConvert.DeserializeObject<JsonToken>(streamReader.ReadToEnd());
                    _token = result.token;
                }
            }
            else
            {
                throw new Exception(httpResponse.StatusCode + " " + httpResponse.StatusDescription);
            }
        }

        

        private WebRequest generateWebRequest(string url)
        {
            WebRequest httpWebRequest = WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + _token);
            return httpWebRequest;
        }

        

        

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="usr">The usr.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="tsuccess">The tsuccess.</param>
        /// <returns></returns>
        public string GetToken()
        {
            //GetToken();
            return _token;
        }

        /// <summary>
        /// Ensures the token.
        /// </summary>
        public Boolean EnsureToken()
        {
            if (string.IsNullOrEmpty(_token))
            {
                return false;
            }
            return true;
        }
    }

    class JsonData
    {
        public string tenant;
        public string username;
        public string password;
    }

    class JsonToken
    {
        public string token;
    }

    class JsonSqlConnection
    {
        public int id;
        public JsonLinks[] links;
    }

    class JsonItems
    {
        public JsonSqlConnection[] items;
        public int count;
        public JsonLinks[] links;
    }

    class JsonLinks
    {
        public string href;
        public string rel;
    }

    class JsonMetaLinks
    {
        public JsonLinks[] links;
        public string meta;
    }

    class JsonImportResponse
    {
        public JsonMetaLinks metalinks;
        public string dtAgentExtractName;
        public string dtAgentFolder;

    }



}
