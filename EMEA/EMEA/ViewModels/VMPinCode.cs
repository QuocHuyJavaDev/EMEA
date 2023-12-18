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
    public class VMPinCode : IPinCode
    {
        public async Task<PinCode> GetPCByUser(int userid)
        {
            var pc = new List<PinCode>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "PinCode/getPCByUser/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                pc = JsonConvert.DeserializeObject<List<PinCode>>(content);
                return await Task.FromResult(pc.FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> InserPC(PinCode pc)
        {
            string json = JsonConvert.SerializeObject(pc);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "PinCode/insertPC");
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
    }
}
