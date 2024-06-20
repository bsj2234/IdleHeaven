using UnityEngine;

namespace IdleHeaven
{
    public interface ICharacterEffector
    {
        void ApplyEffect(GameObject character);
    }

    [System.Serializable]
    public class HpEffect : ScriptableObject, ICharacterEffector
    {
        [SerializeField] private int _hpChange;

        public void ApplyEffect(GameObject character)
        {
            CharacterStats stats = character.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.ChangeHp(_hpChange);
            }
        }
    }

    [System.Serializable]
    public class StatEffect : ScriptableObject, ICharacterEffector
    {
        [SerializeField] private string _statName;
        [SerializeField] private int _statChange;

        public void ApplyEffect(GameObject character)
        {
            CharacterStats stats = character.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.ChangeStat(_statName, _statChange);
            }
        }
    }
}
