using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.SigninResgis;

public partial class Register : ContentPage
{
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public static User usdata;
    public Register()
	{
		InitializeComponent();
    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void register_click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        string rePassword = txtRePassword.Text;
        if (userName == "" || password == "" || rePassword == "")
        {
            string text = "Please input Username & Password";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        if (password != rePassword)
        {
            string text = "Password does not match!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        usdata = new User
        {
            UserName = userName,
            UserPass = password,
            UserFName = "",
            UserLName = "",
            UserlMail = "",
            UserDOB = "",
            UserSex = -1
        };
        await Navigation.PushAsync(new Detail_Infor());
        string text1 = "Next, please add your information!";
        var toast1 = Toast.Make(text1, duration, fontSize);
        await toast1.Show(cancellationTokenSource.Token);
        
    }

    private async void gotosignin_click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Signin());
    }
}