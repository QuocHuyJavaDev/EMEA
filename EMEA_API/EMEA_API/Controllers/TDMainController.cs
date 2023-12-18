using EMEA_API.DBConnect;
using EMEA_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace EMEA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TDMainController : ControllerBase
    {
        int result = -1;
        //Get all Todo
        [HttpGet("allTodo")]
        public IActionResult GetAllTodo()
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<TodoMain> tdMain = new List<TodoMain>();
            OracleCommand command = new OracleCommand("SELECT * FROM TODOMAIN", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                tdMain.Add(new TodoMain
                {
                    MainId = oracleDataReader.GetInt32(0),
                    MainName = oracleDataReader.GetString(1),
                    DateMain = oracleDataReader.GetString(2),
                    MByUser = oracleDataReader.GetInt32(3)
                });
            }
            if (tdMain.Count > 0)
            {
                return Ok(tdMain);
            }
            else
            {
                return NotFound();
            }
        }
        //Get by Userid
        [HttpGet("{userid}")]
        public IActionResult GetTDByUser(int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<TodoMain> tdMain = new List<TodoMain>();
            OracleCommand command = new OracleCommand($"SELECT * FROM TODOMAIN WHERE MByUser={userid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                tdMain.Add(new TodoMain
                {
                    MainId = oracleDataReader.GetInt32(0),
                    MainName = oracleDataReader.GetString(1),
                    DateMain = oracleDataReader.GetString(2),
                    MByUser = oracleDataReader.GetInt32(3)

                });
            }
            if (tdMain.Count > 0)
            {
                return Ok(tdMain);
            }
            else
            {
                return NotFound();
            }
        }
        //Add todo
        [HttpPost("addtodo")]
        public IActionResult AddTodo(TodoMain tdMain)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO TODOMAIN " +
                    $"(MainName, DateMain, MByUser) VALUES " +
                    $"('{tdMain.MainName}', '{tdMain.DateMain}', {tdMain.MByUser})", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTodo();
            }
            else
            {
                return NotFound();
            }
        }
        //Edit todo Name
        [HttpPut("editTDName/{mainid}")]
        public IActionResult EditTDMain(int mainid, TodoMain tdMain)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE TODOMAIN SET MainName= '{tdMain.MainName}'," +
                $" DateMain= '{tdMain.DateMain}', MByUser={tdMain.MByUser}" +
                $" WHERE MainId='{mainid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTodo();
            }
            else
            {
                return NotFound();
            }
        }
        //Delete Todo
        [HttpDelete("delete/{mainid}")]
        public IActionResult DeleteTodo(int mainid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM TODOMAIN WHERE MainId='{mainid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTodo();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
