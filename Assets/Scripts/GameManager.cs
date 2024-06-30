using UnityEngine;
namespace IdleHeaven
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] Player _player;


        private void Start()
        {
            _player = FindObjectOfType<Player>();

            
        }

        public Player GetPlayer()
        {
            return _player;
        }

        //플레이어가 죽으면 전 스테이지를 반복모드로 돌음

        //
    }
}