using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.UI.Mobile.Category;
using EMEA.ViewModels;
using System.Collections.ObjectModel;
using UraniumUI.Pages;

namespace EMEA.UI.Mobile.Note;

public partial class MBNotebook : ContentPage
{
    private readonly INotebook _ntbService = new VMNotebook();
    public static Notebook ntb2;
    ObservableCollection<Notebook> updateList { get; set; } = new ObservableCollection<Notebook>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MBNotebook()
	{
		InitializeComponent();
        this.BindingContext = new VMNotebook();
    }

    private async void Edit_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (Notebook)btn.BindingContext;

        ntb2 = ntb;
        await Navigation.PushAsync(new MBEditNotebook());
    }

    private async void Del_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (Notebook)btn.BindingContext;

        ntb2 = ntb;
        var result = await DisplayAlert("Delete", $"Do you want delete notebook " + ntb2.NotebookName + "?", "Yes", "No");
        if (result)
        {
            bool check = await _ntbService.DeleteNtb(ntb2.NotebookId);
            if (check == true)
            {
                updateList.Clear();
                int userid = App.userInfor.UserId;
                List<Notebook> list = await _ntbService.GetByUsId(userid);
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    updateList.Add(list[i]);
                }
                View1.ItemsSource = updateList;
                string text = "Delete Successfully!";
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

    private async void Tap_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (Notebook)btn.BindingContext;

        ntb2 = ntb;
        await Navigation.PushAsync(new MBNoteList());
    }

    private async void Search_TC(object sender, TextChangedEventArgs e)
    {
        List<Notebook> listntb = new List<Notebook>();
        listntb.Clear();
        listntb = await _ntbService.GetByUsId(App.userInfor.UserId);
        var result = listntb.Where(a => a.NotebookName.StartsWith(e.NewTextValue));
        View1.ItemsSource = result;
    }

    private async void AddNtb_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBAddNotebook());
    }

    private async void gotoHome_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBNoteHome());
    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBNoteHome());
    }
}