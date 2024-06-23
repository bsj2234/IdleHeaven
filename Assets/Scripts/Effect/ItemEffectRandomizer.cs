using IdleHeaven;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemEffectRandomizer : MonoSingleton<ItemEffectRandomizer>
{
    // Rarity percentages
    private readonly Dictionary<Rarity, float> rarityChances = new Dictionary<Rarity, float>
    {
        { Rarity.Common, 50f },
        { Rarity.Uncommon, 30f },
        { Rarity.Rare, 15f },
        { Rarity.Epic, 4f },
        { Rarity.Legendary, 1f }
    };

    private List<ItemEffectData> itemEffects;

    void Start()
    {
        itemEffects = CSVParser.Instance.effects;
    }

    public ItemEffect GetRandomEffect()
    {
        float totalChance = 0f;
        foreach (var chance in rarityChances.Values)
        {
            totalChance += chance;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        Rarity selectedRarity = Rarity.Common;
        foreach (var rarity in rarityChances)
        {
            cumulativeChance += rarity.Value;
            if (randomValue < cumulativeChance)
            {
                selectedRarity = rarity.Key;
                break;
            }
        }

        // Filter effects by selected rarity and choose a random one
        var filteredEffects = itemEffects.FindAll(effect => effect.Rarity == selectedRarity);
        if (filteredEffects.Count == 0)
        {
            Debug.LogError($"No effects found for rarity: {selectedRarity}");
            return null;
        }
        ItemEffectData selectdedData = filteredEffects[UnityEngine.Random.Range(0, filteredEffects.Count)];

        // Generate random value within the specified range
        int randomStat = UnityEngine.Random.Range(selectdedData.MinValue, selectdedData.MaxValue + 1);
        ItemEffect randomEffect = new ItemEffect
        {
            Stat = selectdedData.Stat,
            Rarity = selectdedData.Rarity,
            Value = randomStat
        };


        return randomEffect;
    }
}
