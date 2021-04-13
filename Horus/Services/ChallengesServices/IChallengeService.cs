using Horus.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Horus.Services.ChallengesServices
{
	public interface IChallengeService
	{
		Task<Response> GetChallenges<T>();
	}
}
