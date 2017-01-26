using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIAppX.apiObjects;
using System.Threading.Tasks;
using Swashbuckle.Swagger.Annotations;
using TRex.Metadata;
using System.Data.SqlClient;
using System.Configuration;
using APIAppX.RMS;


namespace APIAppX.Controllers
{
    public class LogicAppController : ApiController
    {
        [SwaggerOperation("GetAuthenticationKey")]
        [Metadata("Enter your user name and password to Log In")]
        [SwaggerResponse( HttpStatusCode.OK,"Session Key",typeof(LoginResponse))]
        [SwaggerResponse(HttpStatusCode.Unauthorized)]
        public async Task<LoginResponse> Get([FromUri] string Username, [FromUri] string Password, [FromUri] string Tenant)
        {

            //var objAuth = new CustomAuthentication();
            //var sessionKey = await objAuth.GetSessionID(Username, Password);
            //return new LoginResponse { SessionNumber = sessionKey };
            var objAuth = new RMSCustomAuth(Username, Password, "", Tenant);
            String sToken = objAuth.GetToken();
            return new LoginResponse { SessionNumber = sToken };

        }
        
        // POST: api/LogicApp
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LogicApp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [SwaggerOperation("Delete currency ID ")]
        [Metadata("Enter Currency ID to delete")]
        [SwaggerResponse(HttpStatusCode.OK, "status", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        // DELETE: api/LogicApp/5
        public void Delete(int id)
        {

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string queryString = "delete from dbo.currencies";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.BeginExecuteNonQuery();

            }
                //delete from dbo.currencies

            }
        }
}
