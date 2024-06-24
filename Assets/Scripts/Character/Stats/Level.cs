using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int Level;
    public int MaxLevel;
    public float Exp;
    public float MaxExp = 100f;

    public Action OnLevelUp {get; set;}


    public void AddExp(int exp)
    {
        this.Exp += exp;
        if (this.Exp >= MaxExp)
        {
            LevelUp();
        }
    }

    public void RemoveExp(int exp) 
    {
        this.Exp -= exp;
        if (this.Exp < 0) {
            this.Exp = 0;
        }
    }

    public void LevelUp()
    {
        Level++;
        Exp = 0;
        MaxExp = MaxExp * 2;
        OnLevelUp?.Invoke();
    }
}
