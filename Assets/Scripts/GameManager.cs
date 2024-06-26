using UnityEngine;
namespace IdleHeaven
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] Player _player;

        public Player GetPlayer()
        {
            return _player;
        }
    }
}