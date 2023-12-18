using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.Note;

public partial class MBEditNotebook : ContentPage
{
    private readonly INotebook _ntbService = new VMNotebook();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MBEditNotebook()
	{
		InitializeComponent();
        txtNtbName.Text = MBNotebook.ntb2.NotebookName;
	}

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBNotebook());
    }

    private async void edit_Click(object sender, EventArgs e)
    {
        string ntbName = txtNtbName.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = MBNotebook.ntb2.NotebookId;
        Notebook notebook = new Notebook
        {
            NotebookName = ntbName,
            DateCreate = getDate,
            NtbByUser = userid
        };
        bool check = await _ntbService.UpdNtb(ntbId, notebook);
        if (check == true)
        {
            await Navigation.PushAsync(new MBNotebook());
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
}