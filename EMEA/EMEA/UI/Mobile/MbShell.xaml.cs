using EMEA.UI.Mobile.Category;

namespace EMEA.UI.Mobile;

public partial class MbShell : Shell
{
    public MbShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }
}