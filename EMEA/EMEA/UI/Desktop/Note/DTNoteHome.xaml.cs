using EMEA.UI.Desktop.Category;

namespace EMEA.UI.Desktop.Note;

public partial class DTNoteHome : ContentPage
{

	public DTNoteHome()
	{
		InitializeComponent();
	}

    private async void gotoNote_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTAllNote());
    }

    private async void gotoNtb_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTNotebook());
    }

    private async void GotoHome_Click(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(DTHomePage)}");
    }
}