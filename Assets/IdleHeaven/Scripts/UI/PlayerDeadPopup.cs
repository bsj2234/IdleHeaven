using IdleHeaven;
using TMPro;
using UnityEngine;

public class PlayerDeadPopup : MonoBehaviour
{
    private UiDrawerEffect _drawer;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Health _health;

    private void Awake()
    {
        _drawer = GetComponent<UiDrawerEffect>();
        _health.OnDead.AddListener(OnPlayerDead);
    }
    public void OnPlayerDead(Attack attack, Health health)
    {
        _text.text = "플레이어가 죽었습니다!";
        _drawer.OpenWithDelayedClose(2f);
    }
}
