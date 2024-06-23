using TMPro;

namespace IdleHeaven
{
    public class PlayerView : BaseView<PlayerViewModel>
    {
        public TextMeshProUGUI playerNameText;
        public TextMeshProUGUI playerHealthText;
        public TextMeshProUGUI playerScoreText;

        protected override void OnViewModelPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(PlayerViewModel.Name):
                    playerNameText.text = ViewModel.Name;
                    break;
                case nameof(PlayerViewModel.Health):
                    playerHealthText.text = ViewModel.Health.ToString();
                    break;
                case nameof(PlayerViewModel.Score):
                    playerScoreText.text = ViewModel.Score.ToString();
                    break;
            }
        }
        public void OnHealthDecreased()
        {
            if (ViewModel.DecreaseHealthCommand.CanExecute())
            {
                ViewModel.DecreaseHealthCommand.Execute();
            }
        }
    }
}