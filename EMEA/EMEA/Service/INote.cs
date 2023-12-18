using EMEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.Service
{
    public interface INote
    {
        Task<List<Notes>> GetByUserId(int userid);
        Task<List<Notes>> GetByNtbId(int ntbid);
        Task<List<Notes>> GetFavor(int isfavor, int userid);
        Task<bool> AddNote(Notes note);
        Task<bool> UpdNote(int noteid, Notes note);

        Task<bool> FavorChange(int noteid, Notes note);
        Task<bool> DeleteNote(int noteid);
    }
}
