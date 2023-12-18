using UraniumUI.Pages;

namespace EMEA.UI.Mobile.Note;

public partial class MBNoteHome : UraniumContentPage
{
	public MBNoteHome()
	{
		InitializeComponent();
	}

    private void gotoNote_Click(object sender, EventArgs e)
    {

    }

    private async void gotoNTB_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBNotebook());
    }
}