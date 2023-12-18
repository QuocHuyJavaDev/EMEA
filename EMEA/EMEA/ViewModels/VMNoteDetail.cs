using EMEA.Models;
using EMEA.UI.Mobile.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA.ViewModels
{
    public class VMNoteDetail
    {
        public Notes notedata;
        public Notes noteview
        {
            get => notedata;
        }

        public VMNoteDetail()
        {
            notedata = MBNoteDetail.createModel();
        }
    }
}
