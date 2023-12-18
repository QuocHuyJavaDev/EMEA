using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.UI.Desktop.Category;
using EMEA.ViewModels;
using Newtonsoft.Json;

namespace EMEA.UI.Desktop.SigninRegis;

public partial class DTLogin : ContentPage
{
    private readonly IUser _userService = new VMUser();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTLogin()
	{
		InitializeComponent();
	}

    private async void Login_Click(object sender, EventArgs e)
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
            await Shell.Current.GoToAsync($"//{nameof(DTHomePage)}");
        }
        else
        {
            string text = "Username or Password incorrect!";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void Forgot_Click(object sender, EventArgs e)
    {

    }

    private async void Register_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTRegister());
    }
}