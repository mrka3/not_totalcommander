using System.Windows;

namespace NotTotalCommaderDesktop
{
    public partial class DeleteDirectoryWindow
    {
        public DeleteDirectoryWindow()
        {
            InitializeComponent();
        }
        
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}