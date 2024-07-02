
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystemView : MonoBehaviour
{
    [SerializeField] LevelSystemViewModel LevelSystemViewModel;


    [SerializeField] TMP_Text Text_Level;
    [SerializeField] TMP_Text Text_Exp;
    [SerializeField] TMP_Text Text_ExpPersentage;
    [SerializeField] Slider Progress_Exp;

    private void Awake()
    {
        LevelSystemViewModel.PropertyChanged += ItemViewModel_PropertyChanged;
    }
    private void Start()
    {
        UpdateLevelSystemView(LevelSystemViewModel.LevelSystem);
    }

    public virtual void ItemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(LevelSystemViewModel.LevelSystem):
                UpdateLevelSystemView(sender as LevelSystem);
                break;
            default:
                break;
        }
    }

    private void UpdateLevelSystemView(LevelSystem levelSystem)
    {
        if (levelSystem == null)
        {
            return;
        }
        if(Text_Level != null)
        {
            Text_Level.text = $"Lv.{levelSystem.Level.ToString()}";
        }
        if(Text_Exp != null)
        {
            Text_Exp.text = levelSystem.Exp.ToString();
        }
        if(Text_ExpPersentage != null)
        {
            Text_ExpPersentage.text = (levelSystem.Exp / levelSystem.MaxExp).ToString("F2") + "%";
        }
        if(Progress_Exp != null)
        {
            Progress_Exp.value = levelSystem.Exp / levelSystem.MaxExp;
        }

    }
}
