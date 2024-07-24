using IdleHeaven;
using TMPro;
using UnityEngine;

public class StageClearPopup : MonoBehaviour
{
    private UiDrawerEffect _drawer;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Stage _stage;

    private void Awake()
    {
        _drawer = GetComponent<UiDrawerEffect>();
        _stage.OnWaveChanged.AddListener(OnWaveChange);
    }

    public void OnWaveChange(string stageName, int wave)
    {
        _text.text = $"{stageName} - {wave + 1}";
        _drawer.OpenWithDelayedClose(2f);
    }
}
