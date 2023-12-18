using EMEA.Models;
using EMEA.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.ViewModels
{
    public class VMUser : IUser
    {

        public async Task<User> Login(string username, string password)
        {
            var userInfor = new List<User>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "User/login/" + username + "/" + password);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                userInfor = JsonConvert.DeserializeObject<List<User>>(content);
                return await Task.FromResult(userInfor.FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Register(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "User/register");
            HttpResponseMessage responseMessage = await client.PostAsync("", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return false;
            }
        }

        public async Task<User> CheckAcc(string username, string useremail)
        {
            var userInfor = new List<User>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "User/checkpass/" + username + "/" + useremail);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                userInfor = JsonConvert.DeserializeObject<List<User>>(content);
                return await Task.FromResult(userInfor.FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdatePassword(int userid, User user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "User/UpdatePassword/" + userid);
            HttpResponseMessage responseMessage = await client.PutAsync("", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return false;
            }
        }
    }
}
