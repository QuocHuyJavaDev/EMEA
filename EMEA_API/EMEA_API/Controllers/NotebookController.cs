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
    public class NotebookController : ControllerBase
    {
        int rs = -1;
        [HttpGet]
        public IActionResult GetAllNtb()
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Notebook> ntb = new List<Notebook>();
            OracleCommand command = new OracleCommand("SELECT * FROM NOTEBOOKS", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                ntb.Add(new Notebook
                {
                    NotebookId = oracleDataReader.GetInt32(0),
                    NotebookName = oracleDataReader.GetString(1),
                    DateCreate = oracleDataReader.GetString(2),
                    NtbByUser = oracleDataReader.GetInt32(3)

                });
            }
            if (ntb.Count > 0)
            {
                return Ok(ntb);
            }
            else
            {
                return NotFound();
            }
        }
        //GetByUserId
        [HttpGet("{userid}")]
        public IActionResult GetByUsId(int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Notebook> ntb = new List<Notebook>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTEBOOKS WHERE NtbByUser={userid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                ntb.Add(new Notebook
                {
                    NotebookId = oracleDataReader.GetInt32(0),
                    NotebookName = oracleDataReader.GetString(1),
                    DateCreate = oracleDataReader.GetString(2),
                    NtbByUser = oracleDataReader.GetInt32(3)

                });
            }
            if (ntb.Count > 0)
            {
                return Ok(ntb);
            }
            else
            {
                return NotFound();
            }
        }
        //Insert
        [HttpPost("addntb")]
        public IActionResult AddNtb(Notebook ntb)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO NOTEBOOKS " +
                    $"(NotebookName, DateCreate, NtbByUser) VALUES " +
                    $"('{ntb.NotebookName}', '{ntb.DateCreate}', {ntb.NtbByUser})", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllNtb();
            }
            else
            {
                return NotFound();
            }
        }

        //update ntb
        [HttpPut("{ntbid}")]
        public IActionResult UpdateNtb(int ntbid, Notebook ntb)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE NOTEBOOKS SET  NotebookName= '{ntb.NotebookName}'," +
                $" DateCreate= '{ntb.DateCreate}', NtbByUser={ntb.NtbByUser}" +
                $" WHERE NotebookId='{ntbid}'", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllNtb();
            }
            else
            {
                return NotFound();
            }
        }

        //Del
        [HttpDelete("delete/{ntbid}")]
        public IActionResult DelNtb(int ntbid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM NOTEBOOKS WHERE NotebookId='{ntbid}'", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllNtb();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
