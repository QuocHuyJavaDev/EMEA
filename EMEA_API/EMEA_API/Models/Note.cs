namespace EMEA_API.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDetail { get; set; }
        public string NoteDate { get; set; }
        public int IsFavor { get; set; }
        public int NByNtb { get; set; }
        public int NByUser { get; set; }
    }
}
