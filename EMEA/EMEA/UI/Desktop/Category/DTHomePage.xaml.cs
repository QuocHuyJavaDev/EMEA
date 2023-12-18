
using EMEA.UI.Desktop.Calendars;
using EMEA.UI.Desktop.Note;
using EMEA.UI.Desktop.TodoList;

namespace EMEA.UI.Desktop.Category;

public partial class DTHomePage : ContentPage
{
	public DTHomePage()
	{
		InitializeComponent();
	}

    private async void GotoNtb_click(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new DTNoteHome());
    }

    private async void GotoTodo_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTTodo());
    }

    private async void GotoCalendar_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTSchedule());
    }
}