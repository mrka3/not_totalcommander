using System.Windows;
using NotTotalCommanderLib.Infrastructure.Exceptions;
using NotTotalCommanderLib.Infrastructure.Routing;

namespace NotTotalCommaderDesktop
{
    public partial class NewDirectoryWindow
    {
        private readonly RouteService routeService;

        public NewDirectoryWindow(RouteService routeService)
        {
            this.routeService = routeService;
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                routeService.CreateDirectory(NameBox.Text);
                DialogResult = true;
            }
            catch (CreateDirectoryValidationFailedException exception)
            {
                ErrorValidation.Text = exception.Message;
            }
        }
    }
}