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
    public class PinCodeController : ControllerBase
    {
        int rs = -1;
        //get all picod
        [HttpGet]
        public IActionResult GetAllPinCode()
        {

            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<PinCode> pc = new List<PinCode>();
            OracleCommand command = new OracleCommand("SELECT * FROM PINCODE", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                pc.Add(new PinCode
                {
                    PinID = oracleDataReader.GetInt32(0),
                    PinCodee = oracleDataReader.GetString(1),
                    PCByUser = oracleDataReader.GetInt32(2)
                });
            }
            if (pc.Count > 0)
            {
                return Ok(pc);
            }
            else
            {
                return NotFound();
            }
        }

        //get pin code by user
        [HttpGet("getPCByUser/{userid}")]
        public IActionResult GetPCByUsId(int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<PinCode> pc = new List<PinCode>();
            OracleCommand command = new OracleCommand($"SELECT * FROM PINCODE WHERE PCByUser={userid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                pc.Add(new PinCode
                {
                    PinID = oracleDataReader.GetInt32(0),
                    PinCodee = oracleDataReader.GetString(1),
                    PCByUser = oracleDataReader.GetInt32(2)

                });
            }
            if (pc.Count > 0)
            {
                return Ok(pc);
            }
            else
            {
                return NotFound();
            }
        }

        //inser PC
        [HttpPost("insertPC")]
        public IActionResult InsertPC(PinCode pc)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO PINCODE " +
                    $"(PinCodee, PCByUser) " +
                    $"VALUES ('{pc.PinCodee}', {pc.PCByUser})", AbsDb.conn);
            try
            {
                rs = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (rs > 0)
            {
                return GetAllPinCode();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
