using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;
using System.Collections.ObjectModel;

namespace EMEA.UI.Desktop.Note;

public partial class DTAllNote : ContentPage
{
    public ObservableCollection<Notes> noteByUser { get; set; }
    private readonly INote _noteService = new VMNote();
    public static Notes noteData;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTAllNote()
	{
		InitializeComponent();
        lbNtbName.Text = "ALL NOTE";
        noteByUser = new ObservableCollection<Notes>();
        NoteByUser();

        BindingContext = this;
    }
    private async void NoteByUser()
    {
        noteByUser.Clear();
        int userid = App.userInfor.UserId;
        List<Notes> list = await _noteService.GetByUserId(userid);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            noteByUser.Add(list[i]);
        }
        noteView.ItemsSource = noteByUser;

    }
    private async void Add_Click(object sender, EventArgs e)
    {
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int ntbid = 0;
        int userid = App.userInfor.UserId;
        Notes note = new Notes
        {
            NoteName = "Title",
            NoteDetail = "Write more...",
            NoteDate = getDate,
            IsFavor = 0,
            NByNtb = ntbid,
            NByUser = userid
        };
        bool check = await _noteService.AddNote(note);
        if (check == true)
        {
            NoteByUser();
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

    private async void Detai_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (Notes)btn.BindingContext;

        noteData = ntb;
        await Navigation.PushAsync(new DTAllDetail());
    }

    private async void Search_TC(object sender, TextChangedEventArgs e)
    {
        int userid = App.userInfor.UserId;
        List<Notes> listntb = new List<Notes>();
        listntb.Clear();
        listntb = await _noteService.GetByUserId(userid);
        var result = listntb.Where(a => a.NoteName.StartsWith(e.NewTextValue));
        noteView.ItemsSource = result;
    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTNoteHome());
    }
}