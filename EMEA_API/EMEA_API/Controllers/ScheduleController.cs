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
    public class ScheduleController : ControllerBase
    {
        int result = -1;
        [HttpGet("allsche")]
        public IActionResult GetAllEvent()
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Schedule> even = new List<Schedule>();
            OracleCommand command = new OracleCommand("SELECT * FROM SCHEDULE", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                even.Add(new Schedule
                {
                    EventId = oracleDataReader.GetInt32(0),
                    EventStart = oracleDataReader.GetString(1),
                    EventEnd = oracleDataReader.GetString(2),
                    EventName = oracleDataReader.GetString(3),
                    EByUser = oracleDataReader.GetInt32(4)

                });
            }
            if (even.Count > 0)
            {
                return Ok(even);
            }
            else
            {
                return NotFound();
            }
        }
        //get by user
        //GetById
        [HttpGet("sche/{userid}")]
        public IActionResult GetEvenByUser(int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Schedule> schedules = new List<Schedule>();
            OracleCommand command = new OracleCommand($"SELECT * FROM SCHEDULE WHERE EByUser={userid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                schedules.Add(new Schedule
                {
                    EventId = oracleDataReader.GetInt32(0),
                    EventStart = oracleDataReader.GetString(1),
                    EventEnd = oracleDataReader.GetString(2),
                    EventName = oracleDataReader.GetString(3),
                    EByUser = oracleDataReader.GetInt32(4)

                });
            }
            if (schedules.Count > 0)
            {
                return Ok(schedules);
            }
            else
            {
                return NotFound();
            }
        }
        //Insert

        [HttpPost("addsche")]
        public IActionResult AddSche(Schedule sche)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO SCHEDULE " +
                    $"(EventStart, EventEnd, EventName, EByUser) VALUES " +
                    $"('{sche.EventStart}', '{sche.EventEnd}', '{sche.EventName}', " +
                    $"{sche.EByUser})", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllEvent();
            }
            else
            {
                return NotFound();
            }
        }
        //edit
        [HttpPut("editsche/{scheid}")]
        public IActionResult UpdateNtb(int scheid, Schedule sche)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE SCHEDULE SET EventStart= '{sche.EventStart}'," +
                $" EventEnd= '{sche.EventEnd}', EventName='{sche.EventName}', EByUser={sche.EByUser}" +
                $" WHERE EventId='{scheid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllEvent();
            }
            else
            {
                return NotFound();
            }
        }
        //Del
        [HttpDelete("delete/{scheid}")]
        public IActionResult DelNtb(int scheid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM SCHEDULE WHERE EventId='{scheid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllEvent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
