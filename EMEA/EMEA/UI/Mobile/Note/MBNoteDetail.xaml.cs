using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.Note;

public partial class MBNoteDetail : ContentPage
{
    private INote _noteSer = new VMNote();
    private readonly INote _noteService = new VMNote();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MBNoteDetail()
	{
		InitializeComponent();
        this.BindingContext = new VMNoteDetail();

        int isFavor = MBNoteList.noteData.IsFavor;
        if (isFavor == 0)
        {
            btnFavor.IsEnabled = false;
            btnFavor.IsVisible = false;
            btnUnfavor.IsEnabled = true;
            btnUnfavor.IsVisible = true;
        }
        else if (isFavor == 1)
        {
            btnFavor.IsEnabled = true;
            btnFavor.IsVisible = true;
            btnUnfavor.IsEnabled = false;
            btnUnfavor.IsVisible = false;
        }
    }
    public static Notes createModel()
    {
        Notes note = new Notes
        {
            NoteName = MBNoteList.noteData.NoteName,
            NoteDetail = MBNoteList.noteData.NoteDetail,
            NoteDate = MBNoteList.noteData.NoteDate,
            IsFavor = MBNoteList.noteData.IsFavor,
            NByNtb = MBNoteList.noteData.NByNtb,
            NByUser = MBNoteList.noteData.NByUser
        };
        return note;
    }
    private void sortNtbList()
    {
        for (int i = 0; i < MBNoteList.noteByNtb.Count - 1; i++)
        {
            for (int j = i; j < MBNoteList.noteByNtb.Count; j++)
            {
                if (MBNoteList.noteByNtb[i].NoteId < MBNoteList.noteByNtb[j].NoteId)
                {
                    Notes temp = MBNoteList.noteByNtb[i];
                    MBNoteList.noteByNtb[i] = MBNoteList.noteByNtb[j];
                    MBNoteList.noteByNtb[j] = temp;
                }
            }
        }
    }

    private async void back_click(object sender, EventArgs e)
    {
        MBNoteList.noteByNtb.Clear();
        int ntbid = MBNotebook.ntb2.NotebookId;
        List<Notes> list = await _noteService.GetByNtbId(ntbid);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            MBNoteList.noteByNtb.Add(list[i]);
        }
        sortNtbList();
        await Navigation.PushAsync(new MBNoteList());
    }

    private async void Save_Click(object sender, EventArgs e)
    {
        string nName = entryTit.Text;
        string nDetail = TextEditor.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = MBNotebook.ntb2.NotebookId;
        int noteid = MBNoteList.noteData.NoteId;
        Notes note = new Notes
        {
            NoteName = nName,
            NoteDetail = nDetail,
            NoteDate = getDate,
            IsFavor = 0,
            NByNtb = ntbId,
            NByUser = userid
        };
        bool check = await _noteSer.UpdNote(noteid, note);
        if (check == true)
        {

            string text = "Saved";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Failed";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Unfavo_Click(object sender, EventArgs e)
    {
        int noteid = MBNoteList.noteData.NoteId;
        Notes note = new Notes
        {
            NoteName = "",
            NoteDetail = "",
            NoteDate = "",
            IsFavor = 1,
            NByNtb = 0,
            NByUser = 0
        };
        bool check = await _noteSer.FavorChange(noteid, note);
        if (check == true)
        {
            btnFavor.IsEnabled = true;
            btnFavor.IsVisible = true;
            btnUnfavor.IsEnabled = false;
            btnUnfavor.IsVisible = false;
            //MbHome.favorNote.Clear();
            //int userid = App.userInfor.UserId;
            //List<Note> list = await _noteService.GetFavpr(1, userid);
            //for (int i = list.Count - 1; i >= 0; i--)
            //{
            //    MbHome.favorNote.Add(list[i]);
            //}
        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Favo_Click(object sender, EventArgs e)
    {
        int noteid = MBNoteList.noteData.NoteId;
        Notes note = new Notes
        {
            NoteName = "",
            NoteDetail = "",
            NoteDate = "",
            IsFavor = 0,
            NByNtb = 0,
            NByUser = 0
        };
        bool check = await _noteSer.FavorChange(noteid, note);
        if (check == true)
        {
            btnFavor.IsEnabled = false;
            btnFavor.IsVisible = false;
            btnUnfavor.IsEnabled = true;
            btnUnfavor.IsVisible = true;
            
            //MbHome.favorNote.Clear();
            //int userid = App.userInfor.UserId;
            //List<Note> list = await _noteService.GetFavpr(1, userid);
            //for (int i = list.Count - 1; i >= 0; i--)
            //{
            //    MbHome.favorNote.Add(list[i]);
            //}
        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void Export_Click(object sender, EventArgs e)
    {

    }

    private async void Delete_Click(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Delete", $"Do you want delete note " + MBNoteList.noteData.NoteName + "?", "Yes", "No");
        if (result)
        {
            bool check = await _noteSer.DeleteNote(MBNoteList.noteData.NoteId);
            if (check == true)
            {
                await Navigation.PushAsync(new MBNoteList());
                //MBNoteList.noteByNtb.Clear();
                //int ntbid = MBNotebook.ntb2.NotebookId;
                //List<Notes> list = await _noteService.GetByNtbId(ntbid);
                //for (int i = list.Count - 1; i >= 0; i--)
                //{
                //    MBNoteList.noteByNtb.Add(list[i]);
                //}
                //sortNtbList();
                string text = "Delete Succesfully!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);

            }
            else
            {
                string text = "Delete Failed!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }
}