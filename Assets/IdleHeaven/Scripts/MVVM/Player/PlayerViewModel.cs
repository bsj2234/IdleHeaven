namespace IdleHeaven
{
    public class PlayerViewModel : BaseViewModel
    {
        private PlayerModel playerModel;

        public PlayerViewModel()
        {
            playerModel = new PlayerModel();
            playerModel.PropertyChanged += PlayerModel_PropertyChanged;
            DecreaseHealthCommand = new RelayCommand(DecreaseHealth, CanDecreaseHealth);
        }

        private void PlayerModel_PropertyChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }

        public string Name
        {
            get => playerModel.Name;
            set => playerModel.Name = value;
        }

        public int Health
        {
            get => playerModel.Health;
            set => playerModel.Health = value;
        }

        public int Score
        {
            get => playerModel.Score;
            set => playerModel.Score = value;
        }
        public ICommand DecreaseHealthCommand { get; private set; }

        private bool CanDecreaseHealth()
        {
            return Health > 0;
        }

        public void DecreaseHealth()
        {
            Health -= 10;
        }
    }


}
