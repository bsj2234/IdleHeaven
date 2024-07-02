using Newtonsoft.Json;
using UnityEngine;

namespace IdleHeaven
{
    //SpecificType	AttackSpeed	MinDamage	MaxDamage	DefenseValue
    [System.Serializable]
    public class ItemData : IRandomItemInstance
    {
        [SerializeField] string _itemName;
        [SerializeField] ItemType _itemType;
        [SerializeField] string _description;
        [JsonIgnore][SerializeField] Sprite _icon;
        [SerializeField] Rarity _rarity;


        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public ItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Sprite Icon
        {
            get
            {
                return _icon;
            }
        }
        public string IconPath
        {
            set
            {
                _icon = Resources.Load<Sprite>($"Sprites/{value}");
            }
        }

        public virtual Item GetRandomItemInstance(string name, GenerateInfo generateInfo)
        {
            return new Item(name, this);
        }
    }
}