using EMEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.Service
{
    public interface IPinCode
    {
        Task<PinCode> GetPCByUser(int userid);
        Task<bool> InserPC(PinCode pc);
    }
}
