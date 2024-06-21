using IdleHeaven;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CharacterStats characterStats = (CharacterStats)target;

        if (characterStats.Stats == null)
        {
            characterStats.Stats = new Stats();
        }


        if (GUILayout.Button("Add Stat"))
        {
            characterStats.Stats.stats.Add(new Stat (StatType.Hp, 0f));
        }

        for (int i = 0; i < characterStats.Stats.stats.Count; i++)
        {
            Stat stat = characterStats.Stats.stats[i];
            EditorGUI.BeginChangeCheck();

            stat.StatType = (StatType)EditorGUILayout.EnumFlagsField("Stat Type", stat.StatType);
            stat.Value = EditorGUILayout.FloatField("Value", stat.Value);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(characterStats);
            }

            if (GUILayout.Button("Remove Stat"))
            {
                characterStats.Stats.stats.RemoveAt(i);
            }

            EditorGUILayout.Space();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(characterStats);
        }
    }
}