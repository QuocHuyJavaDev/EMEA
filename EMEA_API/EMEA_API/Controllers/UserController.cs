using EMEA_API.Models;
using EMEA_API.DBConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace EMEA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        int rs = -1;
        //get all users
        [HttpGet]
        public IActionResult GetAllUser()
        {

            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<User> user = new List<User>();
            OracleCommand command = new OracleCommand("SELECT * FROM USERS", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                user.Add(new User
                {
                    UserId = oracleDataReader.GetInt32(0),
                    UserName = oracleDataReader.GetString(1),
                    UserPass = oracleDataReader.GetString(2),
                    UserFName = oracleDataReader.GetString(3),
                    UserLName = oracleDataReader.GetString(4),
                    UserlMail = oracleDataReader.GetString(5),
                    UserDOB = oracleDataReader.GetString(6),
                    UserSex = oracleDataReader.GetInt32(7)
                });
            }
            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        //Login
        [HttpGet("login/{username}/{userpass}")]
        public IActionResult Login(string username, string userpass)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<User> user = new List<User>();
            OracleCommand command = new OracleCommand($"SELECT * FROM USERS WHERE UserName='{username}'" +
                $" AND UserPass='{userpass}'", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                user.Add(new User
                {
                    UserId = oracleDataReader.GetInt32(0),
                    UserName = oracleDataReader.GetString(1),
                    UserPass = oracleDataReader.GetString(2),
                    UserFName = oracleDataReader.GetString(3),
                    UserLName = oracleDataReader.GetString(4),
                    UserlMail = oracleDataReader.GetString(5),
                    UserDOB = oracleDataReader.GetString(6),
                    UserSex = oracleDataReader.GetInt32(7)
                });
            }
            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        //Register
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO USERS " +
                    $"(UserName, UserPass, UserFName, UserLName, UserlMail, UserDOB, UserSex) " +
                    $"VALUES ('{user.UserName}', '{user.UserPass}', '{user.UserFName}', '{user.UserLName}', " +
                    $"'{user.UserlMail}', '{user.UserDOB}', '{user.UserSex}')", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllUser();
            }
            else
            {
                return NotFound();
            }
        }

        //Del
        [HttpDelete("delete/{userid}")]
        public IActionResult DelNtb(int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM USERS WHERE UserId='{userid}'", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllUser();
            }
            else
            {
                return NotFound();
            }
        }

        //check pass
        [HttpGet("checkpass/{username}/{useremail}")]
        public IActionResult CheckAcc(string username, string useremail)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<User> user = new List<User>();
            OracleCommand command = new OracleCommand($"SELECT * FROM USERS WHERE UserName='{username}'" +
                $" AND UserlMail='{useremail}'", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                user.Add(new User
                {
                    UserId = oracleDataReader.GetInt32(0),
                    UserName = oracleDataReader.GetString(1),
                    UserPass = oracleDataReader.GetString(2),
                    UserFName = oracleDataReader.GetString(3),
                    UserLName = oracleDataReader.GetString(4),
                    UserlMail = oracleDataReader.GetString(5),
                    UserDOB = oracleDataReader.GetString(6),
                    UserSex = oracleDataReader.GetInt32(7)
                });
            }
            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        //UpdatePassword
        [HttpPut("UpdatePassword/{userid}")]
        public IActionResult UpdatePassword(int userid, User user)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE USERS SET UserPass= '{user.UserPass}'" +
                $" WHERE UserId='{userid}'", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllUser();
            }
            else
            {
                return NotFound();
            }
        }

        //Update all infor
        [HttpPut("UpdateUserInfor/{userid}")]
        public IActionResult UpdateUserInfor(int userid, User user)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE USERS SET UserName= '{user.UserName}', " +
                $" UserPass= '{user.UserPass}', UserFName= '{user.UserFName}', UserLName= '{user.UserLName}', " +
                $" UserlMail= '{user.UserlMail}', UserDOB= '{user.UserDOB}', UserSex= {user.UserSex}, " +
                $" WHERE UserId='{userid}'", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllUser();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
