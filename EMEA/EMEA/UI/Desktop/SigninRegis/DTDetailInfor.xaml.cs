using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace EMEA.UI.Desktop.SigninRegis;

public partial class DTDetailInfor : ContentPage
{
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTDetailInfor()
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
        string mail = txtEmail.Text;
        string dob = txtDoB.Date.ToString();
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

        DTRegister.dtusdata.UserFName = fname;
        DTRegister.dtusdata.UserLName = lname;
        DTRegister.dtusdata.UserlMail = mail;
        DTRegister.dtusdata.UserDOB = dob;
        DTRegister.dtusdata.UserSex = sex;

        await Navigation.PushAsync(new DTPinCode());
    }
}