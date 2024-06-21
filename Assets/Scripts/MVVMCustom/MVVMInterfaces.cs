using System.ComponentModel;

namespace IdleHeaven
{
    public interface IViewModel
    {
        void Refresh();
    }

    public interface IView
    {
        void Initialize(IViewModel viewModel);
        void Refresh();
    }


    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}