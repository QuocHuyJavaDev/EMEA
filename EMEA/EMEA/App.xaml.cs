using EMEA.Models;
using EMEA.UI.Mobile;
using EMEA.UI.Desktop;
namespace EMEA;

public partial class App : Application
{
    public static User userInfor;
    public App()
    {
        InitializeComponent();

#if ANDROID || IOS
        MainPage = new MbShell();
#else
        MainPage = new DTShell();
#endif
    }
}