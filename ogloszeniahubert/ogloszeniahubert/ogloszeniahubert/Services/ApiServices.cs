using ogloszeniahubert.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace ogloszeniahubert.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterUser(string email, string password, string confirmpassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmpassword
             };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://ogloszeniahubert1.azurewebsites.net/api/Account/Register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username",email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "https://ogloszeniahubert1.azurewebsites.net/Token");
            request.Content = new FormUrlEncodedContent(keyvalues);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content);
            var accessToken = jObject.Value<string>("access_token");
            Settings.AccessToken = accessToken;
            Settings.UserName = email;
            Settings.Password = password;
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OgloszeniaUser>> FindItem(string category, string wojewodztwo)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var json = await httpClient.GetStringAsync("https://ogloszeniahubert1.azurewebsites.net/api/OgloszeniaUsers?category="+category+"&wojewodztwo="+wojewodztwo);
            return JsonConvert.DeserializeObject<List<OgloszeniaUser>>(json); 
        }

        public async Task<List<OgloszeniaUser>> LastestItem()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var json = await httpClient.GetStringAsync("https://ogloszeniahubert1.azurewebsites.net/api/OgloszeniaUsers");
            return JsonConvert.DeserializeObject<List<OgloszeniaUser>>(json);
        }

        public async Task<bool> AddItem(OgloszeniaUser ogloszeniaUser)
        {
            var json = JsonConvert.SerializeObject(ogloszeniaUser);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await httpClient.PostAsync("https://ogloszeniahubert1.azurewebsites.net/api/OgloszeniaUsers",content);
            return response.IsSuccessStatusCode;
        }
    }
}
