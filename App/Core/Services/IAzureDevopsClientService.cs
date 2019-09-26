using System;
using AzureDevops.Client;

namespace AzureDevops.Services
{
	public interface IAzureDevopsClientService
	{
		IAzureDevopsClient Client { get; }

		void RegisterAzureDevopsClient(string organization, string personalAccessToken);
	}
}
