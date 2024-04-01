using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;

namespace NotTotalCommaderDesktop.ViewModels
{
    public class DirectoriesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DirectoryContentModel> Content { get; } =
            new ObservableCollection<DirectoryContentModel>();

        private BitmapSource imageSource;

        public BitmapSource ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        private string textSource;

        public string TextSource
        {
            get => textSource;
            set
            {
                textSource = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}