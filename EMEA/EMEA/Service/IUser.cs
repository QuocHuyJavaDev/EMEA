using EMEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.Service
{
    public interface IUser
    {
        Task<User> Login(string username, string password);
        Task<bool> Register(User user);
        Task<User> CheckAcc(string username, string useremail);
        Task<bool> UpdatePassword(int userid, User user);
    }
}
