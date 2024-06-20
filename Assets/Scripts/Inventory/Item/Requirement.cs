using UnityEngine;

namespace IdleHeaven
{
    public interface IRequirement
    {
        bool IsSatisfied(GameObject character);
    }

    [System.Serializable]
    public class LevelRequirement : ScriptableObject, IRequirement
    {
        [SerializeField] private int _requiredLevel;

        public bool IsSatisfied(GameObject character)
        {
            CharacterStats stats = character.GetComponent<CharacterStats>();
            return stats != null && stats.Level >= _requiredLevel;
        }
    }

    [System.Serializable]
    public class StatRequirement : ScriptableObject, IRequirement
    {
        [SerializeField] private string _statName;
        [SerializeField] private int _requiredValue;

        public bool IsSatisfied(GameObject character)
        {
            CharacterStats stats = character.GetComponent<CharacterStats>();
            return stats != null && stats.GetStatValue(_statName) >= _requiredValue;
        }
    }
}
