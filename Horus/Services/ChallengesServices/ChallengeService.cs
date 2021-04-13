using Horus.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Horus.Services.ChallengesServices
{
	public class ChallengeService:IChallengeService
	{
		public async Task<Response> GetChallenges<T>()
		{
			try
			{
				var client = new RestClient("https://horuschallenges.azurewebsites.net/api/Challenges");
				client.Timeout = -1;
				var request = new RestRequest(Method.GET);
				var token = Application.Current.Properties["token"] as string;
				request.AddHeader("Authorization", token);
				request.AddHeader("Content-Type", "application/json");
				request.AddParameter("application/json", null, ParameterType.RequestBody);
				IRestResponse response = await client.ExecuteAsync(request);
				if (!response.IsSuccessful)
				{
					return new Response
					{
						IsSuccess = false,
						Message = response.Content
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
