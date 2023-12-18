using EMEA.UI.Mobile.Note;
using EMEA.UI.Mobile.Schedules;
using EMEA.UI.Mobile.TodoList;

namespace EMEA.UI.Mobile.Category;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void GotoNtb_click(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new MBNoteHome());
    }

    private async void gotoTodo_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBTodo());
    }

    private async void GotoSche_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MBSchedule());
    }
}