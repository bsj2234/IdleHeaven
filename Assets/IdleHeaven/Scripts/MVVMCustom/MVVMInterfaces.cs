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
}