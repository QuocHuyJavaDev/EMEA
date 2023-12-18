using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.SigninResgis;

public partial class Forgot : ContentPage
{
    private readonly IUser _userSevice = new VMUser();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    private static User user;
    public Forgot()
	{
		InitializeComponent();
	}

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void check_click(object sender, EventArgs e)
    {
        var username = txtUsername.Text;
        var email    = txtEmail.Text;

        if (username == "" || email == "")
        {
            string text = "Please input data";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }

        user = await _userSevice.CheckAcc(username, email);
        if (user == null)
        {
            change_pass_area.IsVisible = false;
            btnSubmit.IsVisible = false;
            btnSubmit.IsEnabled = false;
            string text = "You account is not exist!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        change_pass_area.IsVisible = true;
        btnSubmit.IsVisible = true;
        btnSubmit.IsEnabled = true;
    }

    private async void submit_click(object sender, EventArgs e)
    {
        var password = txtPassword.Text;
        var repassword = txtRePassword.Text;

        if (password == "" || repassword == "")
        {
            string text = "Please input data";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }

        if (password != repassword)
        {
            string text = "Password does not match!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }

        user.UserPass = password;
        bool upd_proc = await _userSevice.UpdatePassword(user.UserId, user);
        if (upd_proc == true)
        {
            await Navigation.PushAsync(new Signin());
            string text = "Updating successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            
        } 
        else
        {
            string text = "Something went wrong!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }

    }
}