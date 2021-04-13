using Horus.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Horus.Services.LoginServices
{
	public class LoginService:ILoginService
	{
		public async Task<Response> Login<T>(string user,string password)
		{
			try
			{
				var client = new RestClient("https://horuschallenges.azurewebsites.net/api/UserSignIn");
				client.Timeout = -1;
				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "application/json");
				//request.AddParameter("application/json", "{\r\n\"Email\": \"\",\"Password\": \"Password2\"\r\n}", ParameterType.RequestBody);
				request.AddParameter("application/json", "{\"Email\":\""+user+"\",\"Password\": \""+password+"\"}", ParameterType.RequestBody);
				IRestResponse response = await client.ExecuteAsync(request);
				if (!response.IsSuccessful)
				{
					return new Response
					{
						IsSuccess = false,
						Message = "Sus credenciales son incorrectas"
					};
				}
				var result = JsonConvert.DeserializeObject<T>(response.Content);
				return new Response
				{
					IsSuccess = true,
					Result = result
				};
			}
			catch (Exception e)
			{
				return new Response
				{
					IsSuccess=false,
					Message=e.Message
				};
			}
		}
	}
}
