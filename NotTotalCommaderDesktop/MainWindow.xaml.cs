using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NotTotalCommaderDesktop.ViewModels;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;
using NotTotalCommanderLib.Infrastructure.FileContent;
using NotTotalCommanderLib.Infrastructure.Routing;

namespace NotTotalCommaderDesktop
{
    public partial class MainWindow
    {
        private readonly DeleteDirectoryWindow deleteDirectoryWindow;
        private readonly NewDirectoryWindow newDirectoryWindow;
        private readonly RouteService routeService;

        private DirectoryContentModel selectedContent;

        public MainWindow(RouteService routeService, NewDirectoryWindow newDirectoryWindow,
            DeleteDirectoryWindow deleteDirectoryWindow)
        {
            this.deleteDirectoryWindow = deleteDirectoryWindow;
            this.newDirectoryWindow = newDirectoryWindow;
            this.routeService = routeService;
            DataContext = new DirectoriesViewModel();
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            FillContent();
            base.OnInitialized(e);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var name = ((DirectoryContentModel)((DataGridRow)e.Source).Item).Name;

            routeService.DirectoryDown(name);
            FillContent();
        }

        private void ButtonNewDirectory_OnClick(object sender, RoutedEventArgs e)
        {
            if (newDirectoryWindow.ShowDialog() != true)
                return;

            FillContent();
        }

        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            routeService.DirectoryUp();
            FillContent();
        }

        private void FillContent()
        {
            var context = (DirectoriesViewModel)DataContext;

            context.Content.Clear();

            var currentContentModel = routeService.GetCurrentDirectoryContent();

            var directoryContent = currentContentModel.DirectoryContent;

            foreach (var file in directoryContent)
            {
                context.Content.Add(file);
            }

            ToggleButtons(!currentContentModel.IsRoot);
        }

        private void ButtonDeleteDirectory_OnClick(object sender, RoutedEventArgs e)
        {
            if (selectedContent == null)
                return;

            if (deleteDirectoryWindow.ShowDialog() != true)
                return;

            routeService.DeleteDirectory(selectedContent.Name);
            FillContent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (DirectoryContentModel)((DataGrid)e.Source).SelectedValue;

            if (selected == null)
                return;

            selectedContent = selected;

            var fileContent = routeService.GetFileContent(selected.Name);

            switch (fileContent.FileContentType)
            {
                case FileContentType.Other:
                    ((DirectoriesViewModel)DataContext).TextSource = "Формат файла не поддерживается для просмотра";
                    ((DirectoriesViewModel)DataContext).ImageSource = null;
                    break;
                case FileContentType.Image:
                    ((DirectoriesViewModel)DataContext).ImageSource = BitmapFromBase64(fileContent.Content);
                    ((DirectoriesViewModel)DataContext).TextSource = null;
                    break;
                case FileContentType.Text:
                    ((DirectoriesViewModel)DataContext).TextSource = fileContent.Content;
                    ((DirectoriesViewModel)DataContext).ImageSource = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ToggleButtons(bool enable)
        {
            BackButton.IsEnabled = enable;
            DeleteButton.IsEnabled = enable;
            CreateButton.IsEnabled = enable;
        }

        private BitmapSource BitmapFromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);

            using (var stream = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}