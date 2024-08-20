using Newtonsoft.Json;
using SharpTrooper.Entities;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpTrooper.Core
{
    public class SharpTrooperCore
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private readonly string apiUrl = "http://swapi.dev/api";
        private readonly string _proxyName;
        private readonly HttpClient httpClient;

        public SharpTrooperCore()
        {
            httpClient = new HttpClient();
        }

        public SharpTrooperCore(string proxyName)
        {
            _proxyName = proxyName;

            if (!string.IsNullOrEmpty(_proxyName))
            {
                var handler = new HttpClientHandler
                {
                    Proxy = new WebProxy(_proxyName, 80) { Credentials = CredentialCache.DefaultCredentials }
                };
                httpClient = new HttpClient(handler);
            }
            else
            {
                httpClient = new HttpClient();
            }
        }

        #region Private

        private async Task<string> RequestAsync(string url, HttpMethod httpMethod, string data = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new System.Net.Http.HttpMethod(httpMethod.ToString()), url);

            if (data != null)
            {
                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            return parameters.Remove(parameters.Length - 1, 1).ToString();
        }

        private async Task<T> GetSingleAsync<T>(string endpoint, Dictionary<string, string> parameters = null) where T : SharpEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            return await GetSingleByUrlAsync<T>(url: string.Format("{0}{1}{2}", apiUrl, endpoint, serializedParameters));
        }

        private async Task<SharpEntityResults<T>> GetMultipleAsync<T>(string endpoint, Dictionary<string, string> parameters = null) where T : SharpEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            string json = await RequestAsync(string.Format("{0}{1}{2}", apiUrl, endpoint, serializedParameters), HttpMethod.GET);
            SharpEntityResults<T> swapiResponse = JsonConvert.DeserializeObject<SharpEntityResults<T>>(json);
            return swapiResponse;
        }

        private NameValueCollection GetQueryParameters(string dataWithQuery)
        {
            NameValueCollection result = new NameValueCollection();
            string[] parts = dataWithQuery.Split('?');
            if (parts.Length > 0)
            {
                string QueryParameter = parts.Length > 1 ? parts[1] : parts[0];
                if (!string.IsNullOrEmpty(QueryParameter))
                {
                    string[] p = QueryParameter.Split('&');
                    foreach (string s in p)
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            string[] temp = s.Split('=');
                            result.Add(temp[0], temp[1]);
                        }
                        else
                        {
                            result.Add(s, string.Empty);
                        }
                    }
                }
            }
            return result;
        }

        private async Task<SharpEntityResults<T>> GetAllPaginatedAsync<T>(string entityName, string pageNumber = "1") where T : SharpEntity
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "page", pageNumber }
            };

            SharpEntityResults<T> result = await GetMultipleAsync<T>(entityName, parameters);

            result.nextPageNo = string.IsNullOrEmpty(result.next) ? null : GetQueryParameters(result.next)["page"];
            result.previousPageNo = string.IsNullOrEmpty(result.previous) ? null : GetQueryParameters(result.previous)["page"];

            return result;
        }

        #endregion

        #region Public

        public async Task<T> GetSingleByUrlAsync<T>(string url) where T : SharpEntity
        {
            string json = await RequestAsync(url, HttpMethod.GET);
            T swapiResponse = JsonConvert.DeserializeObject<T>(json);
            return swapiResponse;
        }

        // People
        public async Task<People> GetPeopleAsync(string id)
        {
            return await GetSingleAsync<People>("/people/" + id);
        }

        public async Task<SharpEntityResults<People>> GetAllPeopleAsync(string pageNumber = "1")
        {
            return await GetAllPaginatedAsync<People>("/people/", pageNumber);
        }

        // Film
        public async Task<Film> GetFilmAsync(string id)
        {
            return await GetSingleAsync<Film>("/films/" + id);
        }

        public async Task<SharpEntityResults<Film>> GetAllFilmsAsync(string pageNumber = "1")
        {
            return await GetAllPaginatedAsync<Film>("/films/", pageNumber);
        }

        // Planet
        public async Task<Planet> GetPlanetAsync(string id)
        {
            return await GetSingleAsync<Planet>("/planets/" + id);
        }

        public async Task<SharpEntityResults<Planet>> GetAllPlanetsAsync(string pageNumber = "1")
        {
            return await GetAllPaginatedAsync<Planet>("/planets/", pageNumber);
        }

        // Specie
        public async Task<Specie> GetSpecieAsync(string id)
        {
            return await GetSingleAsync<Specie>("/species/" + id);
        }

        public async Task<SharpEntityResults<Specie>> GetAllSpeciesAsync(string pageNumber = "1")
        {
            return await GetAllPaginatedAsync<Specie>("/species/", pageNumber);
        }

        // Starship
        public async Task<Starship> GetStarshipAsync(string id)
        {
            return await GetSingleAsync<Starship>("/starships/" + id);
        }

        public async Task<SharpEntityResults<Starship>> GetAllStarshipsAsync(string pageNumber = "1")
        {
            return await GetAllPaginatedAsync<Starship>("/starships/", pageNumber);
        }

        // Vehicle
        public async Task<Vehicle> GetVehicleAsync(string id)
        {
            return await GetSingleAsync<Vehicle>("/vehicles/" + id);
        }

        public async Task<SharpEntityResults<Vehicle>> GetAllVehiclesAsync(string pageNumber = "1")
        {
            return await GetAllPaginatedAsync<Vehicle>("/vehicles/", pageNumber);
        }

        #endregion
    }
}
