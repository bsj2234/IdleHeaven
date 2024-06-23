using UnityEngine;

public class GameManager: MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] Player _player;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Assert(false, "NO GameManager In Scene");
            }
            return _instance;
        }
    }

    public Player GetPlayer()
    {
        return _player;
    }
}
