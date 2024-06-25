using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropExp : MonoBehaviour
{
    [SerializeField] int expAmount = 10;

    public void Drop(Attack attacker, )
    {
        // Drop exp
        Debug.Log("Dropped " + expAmount + " exp");
    }
}
