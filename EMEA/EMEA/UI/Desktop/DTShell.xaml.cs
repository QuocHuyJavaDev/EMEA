using EMEA.UI.Desktop.Category;

namespace EMEA.UI.Desktop;

public partial class DTShell : Shell
{
	public DTShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DTHomePage), typeof(DTHomePage));
    }
}