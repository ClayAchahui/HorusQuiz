using Horus.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Horus.Services.LoginServices
{
	public interface ILoginService
	{
		Task<Response> Login<T>(string user, string password);
	}
}
