using UnityEngine;
[System.Serializable]
public class Currency
{
    [SerializeField]
    private int _amount;
    [SerializeField]
    private string _name;
    public int Amount { get => _amount; set => _amount = value; }
    public string Name { get => _name; set => _name = value; }
}

[System.Serializable]
public class Gold : Currency
{
    public Gold(int amount)
    {
        Name = "Gold";
        Amount = amount;
    }
}
