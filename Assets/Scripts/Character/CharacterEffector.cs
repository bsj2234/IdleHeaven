using UnityEngine;

namespace IdleHeaven
{
    public interface ICharacterEffector
    {
        void ApplyEffect();
    }

    [System.Serializable]
    public class StatChangeEffect : ScriptableObject, ICharacterEffector
    {
        [SerializeField] private StatType _statType;
        [SerializeField] private float _value;

        private Stats _target;

        public StatChangeEffect SetTarget(Stats target)
        {
            _target = target;
            return this;
        }

        public void ApplyEffect()
        {
            _target[_statType] += _value;
        }
    }

}
