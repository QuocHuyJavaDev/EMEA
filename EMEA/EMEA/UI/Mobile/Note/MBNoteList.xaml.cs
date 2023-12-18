using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.Note;

public partial class MBNoteList : ContentPage
{
    public static ObservableCollection<Notes> noteByNtb { get; set; }
    private readonly INote _noteService = new VMNote();
    public static Notes noteData;
    //
    ObservableCollection<Notes> updateList { get; set; } = new ObservableCollection<Notes>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MBNoteList()
	{
		InitializeComponent();
        lbNtbName.Text = MBNotebook.ntb2.NotebookName;
        noteByNtb = new ObservableCollection<Notes>();
        NoteByNtb();
        sortNtbList();
        noteView.ItemsSource = noteByNtb;
        BindingContext = this;
    }
    private static void sortNtbList()
    {
        for (int i = 0; i < noteByNtb.Count - 1; i++)
        {
            for (int j = i; j < noteByNtb.Count; j++)
            {
                if (noteByNtb[i].NoteId < noteByNtb[j].NoteId)
                {
                    Notes temp = noteByNtb[i];
                    noteByNtb[i] = noteByNtb[j];
                    noteByNtb[j] = temp;
                }
            }
        }
    }
    private async void NoteByNtb()
    {
        noteByNtb.Clear();
        int ntbid = MBNotebook.ntb2.NotebookId;
        List<Notes> list = await _noteService.GetByNtbId(ntbid);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            noteByNtb.Add(list[i]);
        }

    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBNotebook());
    }

    private async void Add_Click(object sender, EventArgs e)
    {
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int ntbid = MBNotebook.ntb2.NotebookId;
        int userid = App.userInfor.UserId;
        Notes note = new Notes
        {
            NoteName = "Title",
            NoteDetail = "Writer more ...",
            NoteDate = getDate,
            IsFavor = 0,
            NByNtb = ntbid,
            NByUser = userid
        };
        bool check = await _noteService.AddNote(note);
        if (check == true)
        {
            NoteByNtb();
            sortNtbList();
            noteView.ItemsSource = noteByNtb;
            string text = "Add Note Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Add Note Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Search_TC(object sender, TextChangedEventArgs e)
    {
        int ntbid = MBNotebook.ntb2.NotebookId;
        List<Notes> listntb = new List<Notes>();
        listntb.Clear();
        listntb = await _noteService.GetByNtbId(ntbid);
        var result = listntb.Where(a => a.NoteName.StartsWith(e.NewTextValue));
        noteView.ItemsSource = result;
    }

    private async void Detai_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (Notes)btn.BindingContext;

        noteData = ntb;
        await Navigation.PushAsync(new MBNoteDetail());
    }
}