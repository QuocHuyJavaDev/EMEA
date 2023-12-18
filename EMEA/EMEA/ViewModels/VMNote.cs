using EMEA.Models;
using EMEA.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.ViewModels
{
    public class VMNote : INote
    {
        public async Task<bool> AddNote(Notes note)
        {
            string json = JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/addnote");
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

        public async Task<bool> DeleteNote(int noteid)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/delete/" + noteid);
            HttpResponseMessage responseMessage = await client.DeleteAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> FavorChange(int noteid, Notes note)
        {
            string json = JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/favor/" + noteid);
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

        public async Task<List<Notes>> GetByNtbId(int ntbid)
        {
            var note = new List<Notes>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/getbyntb/" + ntbid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                note = await responseMessage.Content.ReadFromJsonAsync<List<Notes>>();

            }
            return await Task.FromResult(note);
        }

        public async Task<List<Notes>> GetByUserId(int userid)
        {
            var note = new List<Notes>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/getbyuser/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                note = await responseMessage.Content.ReadFromJsonAsync<List<Notes>>();

            }
            return await Task.FromResult(note);
        }

        public async Task<List<Notes>> GetFavor(int isfavor, int userid)
        {
            var note = new List<Notes>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/getfavor/" + isfavor + "/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                note = await responseMessage.Content.ReadFromJsonAsync<List<Notes>>();

            }
            return await Task.FromResult(note);
        }

        public async Task<bool> UpdNote(int noteid, Notes note)
        {
            string json = JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "Note/editnote/" + noteid);
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
