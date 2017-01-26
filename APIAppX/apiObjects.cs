using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace APIAppX.apiObjects
{
    public class LoginResponse
    {

        public string SessionNumber { get; set; }
         


    }

    public class CustomAuthentication
    {

        public async Task<string> GetSessionID( string username, string password)
        {
            // Call the custom authentication web service to get a session id
            return "nigfngdfkngflkgfgfhghggthgt";

        }


    }




}