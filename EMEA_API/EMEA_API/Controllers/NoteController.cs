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
    public class NoteController : ControllerBase
    {
        int result = -1;
        //get all note
        [HttpGet("allnotes")]
        public IActionResult GetAllNote()
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Note> note = new List<Note>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTES", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                note.Add(new Note
                {
                    NoteId = oracleDataReader.GetInt32(0),
                    NoteName = oracleDataReader.GetString(1),
                    NoteDetail = oracleDataReader.GetString(2),
                    NoteDate = oracleDataReader.GetString(3),
                    IsFavor = oracleDataReader.GetInt32(4),
                    NByNtb = oracleDataReader.GetInt32(5),
                    NByUser = oracleDataReader.GetInt32(6)
                });
            }
            if (note.Count > 0)
            {
                return Ok(note);
            }
            else
            {
                return NotFound();
            }
        }
        //get all from user
        [HttpGet("getbyuser/{userid}")]
        public IActionResult GetNoteByUser(int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Note> note = new List<Note>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTES WHERE NByUser={userid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                note.Add(new Note
                {
                    NoteId = oracleDataReader.GetInt32(0),
                    NoteName = oracleDataReader.GetString(1),
                    NoteDetail = oracleDataReader.GetString(2),
                    NoteDate = oracleDataReader.GetString(3),
                    IsFavor = oracleDataReader.GetInt32(4),
                    NByNtb = oracleDataReader.GetInt32(5),
                    NByUser = oracleDataReader.GetInt32(6)
                });
            }
            if (note.Count > 0)
            {
                return Ok(note);
            }
            else
            {
                return NotFound();
            }
        }
        //Get note by Notebook
        [HttpGet("getbyntb/{ntbid}")]
        public IActionResult GetNoteByNtbId(int ntbid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Note> note = new List<Note>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTES WHERE NByNtb={ntbid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                note.Add(new Note
                {
                    NoteId = oracleDataReader.GetInt32(0),
                    NoteName = oracleDataReader.GetString(1),
                    NoteDetail = oracleDataReader.GetString(2),
                    NoteDate = oracleDataReader.GetString(3),
                    IsFavor = oracleDataReader.GetInt32(4),
                    NByNtb = oracleDataReader.GetInt32(5),
                    NByUser = oracleDataReader.GetInt32(6)
                });
            }
            if (note.Count > 0)
            {
                return Ok(note);
            }
            else
            {
                return NotFound();
            }
        }
        //Get 1 Note 
        [HttpGet("getnote/{noteid}")]
        public IActionResult GetNoteById(int noteid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Note> note = new List<Note>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTES WHERE NoteId={noteid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                note.Add(new Note
                {
                    NoteId = oracleDataReader.GetInt32(0),
                    NoteName = oracleDataReader.GetString(1),
                    NoteDetail = oracleDataReader.GetString(2),
                    NoteDate = oracleDataReader.GetString(3),
                    IsFavor = oracleDataReader.GetInt32(4),
                    NByNtb = oracleDataReader.GetInt32(5),
                    NByUser = oracleDataReader.GetInt32(6)

                });
            }
            if (note.Count > 0)
            {
                return Ok(note);
            }
            else
            {
                return NotFound();
            }
        }
        //get by favor
        [HttpGet("getfavor/{isfavor}/{userid}")]
        public IActionResult GetByFavor(int isfavor, int userid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            List<Note> note = new List<Note>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTES WHERE IsFavor={isfavor} AND NByUser={userid}", AbsDb.conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                note.Add(new Note
                {
                    NoteId = oracleDataReader.GetInt32(0),
                    NoteName = oracleDataReader.GetString(1),
                    NoteDetail = oracleDataReader.GetString(2),
                    NoteDate = oracleDataReader.GetString(3),
                    IsFavor = oracleDataReader.GetInt32(4),
                    NByNtb = oracleDataReader.GetInt32(5),
                    NByUser = oracleDataReader.GetInt32(6)

                });
            }
            if (note.Count > 0)
            {
                return Ok(note);
            }
            else
            {
                return NotFound();
            }
        }
        //Add note
        [HttpPost("addnote")]
        public IActionResult AddNote(Note note)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO NOTES " +
                    $"(NoteName, NoteDetail, NoteDate, IsFavor, NByNtb, NByUser) VALUES " +
                    $"('{note.NoteName}', '{note.NoteDetail}', '{note.NoteDate}', {note.IsFavor}, " +
                    $"{note.NByNtb}, {note.NByUser})", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNote();
            }
            else
            {
                return NotFound();
            }
        }
        //edit
        [HttpPut("editnote/{noteid}")]
        public IActionResult EditNote(int noteid, Note note)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE NOTES SET NoteName= '{note.NoteName}'," +
                $" NoteDetail= '{note.NoteDetail}', NoteDate='{note.NoteDate}'," +
                $" IsFavor={note.IsFavor}, NByNtb={note.NByNtb}, NByUser={note.NByUser}" +
                $" WHERE NoteId='{noteid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNote();
            }
            else
            {
                return NotFound();
            }
        }
        //Favor
        [HttpPut("favor/{noteid}")]
        public IActionResult Favor(int noteid, Note note)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE NOTES SET IsFavor={note.IsFavor}" +
                $" WHERE NoteId='{noteid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNote();
            }
            else
            {
                return NotFound();
            }
        }
        //Del
        [HttpDelete("delete/{noteid}")]
        public IActionResult DelNtb(int noteid)
        {
            if (AbsDb.conn == null || AbsDb.conn.State == ConnectionState.Closed)
            {
                AbsDb.conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM NOTES WHERE NoteId='{noteid}'", AbsDb.conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNote();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
