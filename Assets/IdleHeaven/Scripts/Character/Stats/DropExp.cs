using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropExp : MonoBehaviour
{
    [SerializeField] int expAmount = 10;

    public void Drop(Attack attacker, Health health)
    {
        attacker.GetComponent<CharacterStats>().LevelSystem.AddExp(expAmount);
    }
}
