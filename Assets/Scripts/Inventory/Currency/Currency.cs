using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Currency
{
    public int Amount { get; set; }
    public string Name { get; set; }
}

public class Gold : Currency
{
    public Gold(int amount)
    {
        Name = "Gold";
        Amount = amount;
    }
}
