using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public string UserlMail { get; set; }
        public string UserDOB { get; set; }
        public int UserSex { get; set; }
    }
}
