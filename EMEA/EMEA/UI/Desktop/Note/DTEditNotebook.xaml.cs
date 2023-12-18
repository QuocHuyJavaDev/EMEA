using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Desktop.Note;

public partial class DTEditNotebook : ContentPage
{
    private readonly INotebook _ntbService = new VMNotebook();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTEditNotebook()
	{
		InitializeComponent();
        txtNtbName.Text = DTNotebook.ntb2.NotebookName;
    }
    private async void Edit_Click(object sender, EventArgs e)
    {
        string ntbName = txtNtbName.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = DTNotebook.ntb2.NotebookId;
        Notebook notebook = new Notebook
        {
            NotebookName = ntbName,
            DateCreate = getDate,
            NtbByUser = userid
        };
        bool check = await _ntbService.UpdNtb(ntbId, notebook);
        if (check == true)
        {
            await Navigation.PushAsync(new DTNotebook());
            string text = "Change Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Change Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Back_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTNotebook());
    }
}