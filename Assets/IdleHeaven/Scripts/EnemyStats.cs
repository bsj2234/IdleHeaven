using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdleHeaven;

public class EnemyStats : CharacterStats
{
    protected override void CalcResultStats()
    {
        ResultStats.Clear();
        ResultStats.AddStats(base._baseStats);

        float levMult = (LevelSystem.Level - 1f) * 1f + 1f;

        ResultStats[StatType.Hp] = ResultStats[StatType.Hp] * levMult;
        ResultStats[StatType.Attack] = ResultStats[StatType.Attack] * levMult;
        ResultStats[StatType.Defense] = ResultStats[StatType.Defense] * levMult;

    }
}
