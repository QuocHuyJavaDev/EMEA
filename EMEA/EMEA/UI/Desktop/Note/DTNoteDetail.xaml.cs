using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Desktop.Note;

public partial class DTNoteDetail : ContentPage
{
    private INote _noteSer = new VMNote();
    private readonly INote _noteService = new VMNote();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTNoteDetail()
	{
		InitializeComponent();
        entryTit.Text = DTNoteList.noteData.NoteName;
        TextEditor.Text = DTNoteList.noteData.NoteDetail;
        int isFavor = DTNoteList.noteData.IsFavor;
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
    private void sortNtbList()
    {
        for (int i = 0; i < DTNoteList.noteByNtb.Count - 1; i++)
        {
            for (int j = i; j < DTNoteList.noteByNtb.Count; j++)
            {
                if (DTNoteList.noteByNtb[i].NoteId < DTNoteList.noteByNtb[j].NoteId)
                {
                    Notes temp = DTNoteList.noteByNtb[i];
                    DTNoteList.noteByNtb[i] = DTNoteList.noteByNtb[j];
                    DTNoteList.noteByNtb[j] = temp;
                }
            }
        }
    }
    private async void Save_Click(object sender, EventArgs e)
    {
        string nName = entryTit.Text;
        string nDetail = TextEditor.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = DTNotebook.ntb2.NotebookId;
        int noteid = DTNoteList.noteData.NoteId;
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

    private async void favor_Click(object sender, EventArgs e)
    {
        int noteid = DTNoteList.noteData.NoteId;
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
            string text = "Unfavorited!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);

        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void unfavor_Click(object sender, EventArgs e)
    {
        int noteid = DTNoteList.noteData.NoteId;
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
            string text = "Favorited!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            btnFavor.IsEnabled = true;
            btnFavor.IsVisible = true;
            btnUnfavor.IsEnabled = false;
            btnUnfavor.IsVisible = false;
        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Delete_Click(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Delete", $"Do you want delete note " + DTNoteList.noteData.NoteName + "?", "Yes", "No");
        if (result)
        {
            bool check = await _noteSer.DeleteNote(DTNoteList.noteData.NoteId);
            if (check == true)
            {

                //DTNoteList.noteByNtb.Clear();
                //int ntbid = DTNotebook.ntb2.NotebookId;
                //List<Notes> list = await _noteSer.GetByNtbId(ntbid);
                //for (int i = list.Count - 1; i >= 0; i--)
                //{
                //    DTNoteList.noteByNtb.Add(list[i]);
                //}
                //sortNtbList();
                await Navigation.PushAsync(new DTNoteList());
                //string text = "Delete Succesfully!";
                //var toast = Toast.Make(text, duration, fontSize);
                //await toast.Show(cancellationTokenSource.Token);

            }
            else
            {
                string text = "Delete Failed!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
            }
        }


    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTNoteList());

    }
}