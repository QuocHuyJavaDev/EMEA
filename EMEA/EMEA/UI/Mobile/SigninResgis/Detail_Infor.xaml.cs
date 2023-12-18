using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EMEA.Service;
using EMEA.ViewModels;

namespace EMEA.UI.Mobile.SigninResgis;

public partial class Detail_Infor : ContentPage
{
 
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public Detail_Infor()
	{
		InitializeComponent();
	}

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void next_click(object sender, EventArgs e)
    {
        string fname = txtFName.Text;
        string lname = txtLName.Text;
        string mail  = txtEmail.Text;
        string dob   = txtDoB.Date.ToString();
        int sex = -1;
        if (fname == "" || lname == "" || mail == "" || dob == "")
        {
            string text = "Please input data";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        if (radMan.IsChecked)
        {
            sex = 0;
        }
        if (radWomen.IsChecked)
        {
            sex = 1;
        }
        
        Register.usdata.UserFName = fname;
        Register.usdata.UserLName = lname;
        Register.usdata.UserlMail = mail;
        Register.usdata.UserDOB = dob;
        Register.usdata.UserSex = sex; 

        await Navigation.PushAsync(new SetPinCode());
    }
}