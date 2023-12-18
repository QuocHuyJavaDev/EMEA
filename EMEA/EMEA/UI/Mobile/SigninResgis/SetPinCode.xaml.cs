using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Models;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.SigninResgis;

public partial class SetPinCode : ContentPage
{
    private readonly IUser _userSevice = new VMUser();
    private readonly IPinCode _pcSevice = new VMPinCode();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public SetPinCode()
	{
		InitializeComponent();
        txtPIN.HorizontalTextAlignment = TextAlignment.Center;

    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void finish_click(object sender, EventArgs e)
    {
        var strPC = txtPIN.Text;
        if (strPC == "")
        {
            string text = "Please input data";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        bool regis_proc = await _userSevice.Register(Register.usdata);
        if (regis_proc == true)
        {
            User user = await _userSevice.Login(Register.usdata.UserName, Register.usdata.UserPass);
            if (user != null)
            {
                PinCode pc = new PinCode
                {
                    PinCodee = strPC,
                    PCByUser = user.UserId
                };
                bool add_pc_proc = await _pcSevice.InserPC(pc);
                if (add_pc_proc == true)
                {
                    await Navigation.PushAsync(new Intro());
                    string text = "Register Successfully!";
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show(cancellationTokenSource.Token);
                } else
                {
                    string text = "Something went wrong!";
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show(cancellationTokenSource.Token);
                }
            } else
            {
                string text = "Something went wrong!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
            }
        } else
        {
            string text = "Something went wrong!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}