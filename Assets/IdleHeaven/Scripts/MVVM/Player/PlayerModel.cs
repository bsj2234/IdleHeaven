using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : BaseModel
{
    private string _name;
    private int _health;
    private int _score;

    public string Name
    {
        get => _name; 
        set 
        { 
            if(_name != value)
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }
    public int Health
    {
        get => _health; 
        set 
        { 
            if(_health != value)
            {
                _health = value;
                NotifyPropertyChanged(nameof(Health));
            }
        }
    }
    public int Score
    {
        get => _score; 
        set 
        { 
            if(_score != value)
            {
                _score = value;
                NotifyPropertyChanged(nameof(Score));
            }
        }
    }
}
