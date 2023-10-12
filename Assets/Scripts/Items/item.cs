using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu (fileName = "item", menuName = "data/items/item", order = 0)]
    public class Item : ScriptableObject
    {
        [FormerlySerializedAs("item_name")] public string itemName;
        public int id;
        public string description;
        public Sprite image;

        [Header("flags")] 
        public bool usable;
        public bool stackable;
        public bool equippable;
        public bool discardable;
        public bool combineable;
    }
}
