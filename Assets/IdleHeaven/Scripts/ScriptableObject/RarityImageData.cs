using IdleHeaven;
using UnityEngine;

[CreateAssetMenu(fileName = "RarityData", menuName = "IdleHeaven/RarityData", order = 0)]

public class RarityData : ScriptableObject
{
    [field: SerializeField] public Rarity Rarity { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField] public Sprite Sprite_Background { get; private set; }
    [field: SerializeField] public float MinRarityStatMulti { get; private set; }
    [field: SerializeField] public float MaxRarityStatMulti { get; private set; }
}
