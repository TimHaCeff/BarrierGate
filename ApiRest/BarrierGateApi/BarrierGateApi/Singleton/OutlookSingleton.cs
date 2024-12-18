using Azure.Identity;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Office.Interop.Outlook;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BarrierGateApi.Singleton
{
    public class OutlookSingleton
    {
        public static OutlookSingleton instance = null;
        protected static readonly object threadSafeLocker = new object();
        private OutlookSingleton() { }
        public static OutlookSingleton Instance 
        {
            get 
            {
                lock (threadSafeLocker)
                {
                    if (instance == null)
                    {
                        instance = new OutlookSingleton();
                        instance.GraphClient = instance.CreateGraphClient(tenantId, clientId, clientSecretId);
                    }
                    return instance;
                }
            }

            set => instance = value;
        }

        protected HttpClient httpClient { get; set; } = new HttpClient();


        protected const string tenantId = "b7fe6897-1a60-4ace-82dd-ad4dd23d3e60"; //82476f9e-4f28-4397-bbf7-7ac7fd019e4b
        protected const string clientId = "";
        protected const string clientSecretId = "";

        public GraphServiceClient GraphClient { get; set; }

        protected GraphServiceClient CreateGraphClient(string tenantId, string clientId, string clientSecret) 
        {
            TokenCredentialOptions options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);
            string[] scopes = { "https://graph.microsoft.com/.default" };

            return new GraphServiceClient(clientSecretCredential, scopes);
        }

        public async Task<User?> GetUser(string userPrincipalName) 
        {
            return await GraphClient.Users[userPrincipalName].GetAsync();
        }

        public async Task<(IEnumerable<Site>?, IEnumerable<Site>?)> GetSharePoint()
        {
            List<Site> sites = (await GraphClient.Sites.GetAllSites.GetAsync())?.Value;
            if(sites == null) 
            {
                return (null, null);
            }
            sites.RemoveAll(x => string.IsNullOrEmpty(x.DisplayName));

            List<Site> apSites = new List<Site>();
            List<Site> oneDriveSites = new List<Site>();

            foreach (Site site in sites)
            {
                if (site == null) continue;

                var compare = site.WebUrl?.Split(site.SiteCollection?.Hostname)[1].Split("/");
                if (compare.All(x => !string.IsNullOrEmpty(x) || compare.Length > 1)) 
                {
                    continue;
                }

                if (compare[1] == "sites" || string.IsNullOrEmpty(compare[1])) 
                {
                    apSites.Add(site);
                }else if (compare[1] == "personal")
                {
                    oneDriveSites.Add(site);
                }
            }
            return (apSites, oneDriveSites);
        }

        public string GetItems() 
        {
            //TokenCredentialOptions options = new TokenCredentialOptions
            //{
            //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            //};

            //ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecretId);
            //var scopes = new[] { "https://graph.microsoft.com/.default" };
            httpClient.BaseAddress = new Uri("https://graph.microsoft.com/v1.0/me/events?$select=subject,body,bodyPreview,organizer,attendees,start,end,location");
            HttpResponseMessage result = httpClient.GetAsync("").Result;
            string content = result.Content.ReadAsStringAsync().Result;


            return content;
        }
        


    }
}