using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.UI.Mobile.Category;
using EMEA.ViewModels;
using Newtonsoft.Json;

namespace EMEA.UI.Mobile.SigninResgis;

public partial class Signin : ContentPage
{
    private readonly IUser _userService = new VMUser();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public Signin()
	{
		InitializeComponent();
	}

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void forgot_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Forgot());
    }

    private async void signin_click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        if (userName == null || password == null)
        {
            string text = "Please input Username & Password";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        User userModel = await _userService.Login(userName, password);
        if (Preferences.ContainsKey(nameof(App.userInfor)))
        {
            Preferences.Remove(nameof(App.userInfor));
        }
        string userDetail = JsonConvert.SerializeObject(userModel);
        Preferences.Set(nameof(App.userInfor), userDetail);
        App.userInfor = userModel;
        if (userModel != null)
        {

            string text = "Success";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        else
        {
            string text = "Username or Password incorrect!";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void gotoregister_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }
}