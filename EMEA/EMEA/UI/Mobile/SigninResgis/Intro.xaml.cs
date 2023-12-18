namespace EMEA.UI.Mobile.SigninResgis;

public partial class Intro : ContentPage
{
	public Intro()
	{
		InitializeComponent();
	}

    private async void register_click(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.8;
        await Navigation.PushAsync(new Register());
        (sender as Button).Opacity = 1;
    }

    private async void signin_click(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.8;
        await Navigation.PushAsync(new Signin());
        (sender as Button).Opacity = 1;
    }


}