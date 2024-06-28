using System;
using UnityEngine.Events;

[Serializable]
public class LevelSystem
{
    public int Level = 1;
    public int MaxLevel = 50;
    public float Exp;
    public float MaxExp = 100f;

    public UnityEvent OnLevelUp;
    public Action OnLevelSystemChanged { get; internal set; }

    public void AddExp(int exp)
    {
        this.Exp += exp;
        if (this.Exp >= MaxExp)
        {
            LevelUp();
        }
        OnLevelSystemChanged?.Invoke();
    }

    public void RemoveExp(int exp) 
    {
        this.Exp -= exp;
        if (this.Exp < 0) {
            this.Exp = 0;
        }
        OnLevelSystemChanged?.Invoke();
    }

    public void LevelUp()
    {
        Level++;
        Exp = 0;
        MaxExp = MaxExp * 1.2f;
        OnLevelUp?.Invoke();
    }
}
