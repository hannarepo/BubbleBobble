
using UnityEngine;

namespace MemoBubble
{
    // Create a new Asset to menu to build new prefab assets
    [CreateAssetMenu(fileName = "New Collectible Item", menuName = "Inventory/Collectible Item")]

    public class ItemData : ScriptableObject
    {
        [SerializeField] private ItemType _type = ItemType.None;
        [SerializeField] private string _name = "";
        [SerializeField] private Sprite _sprite = null;
        [SerializeField] private int _points = 0;

        public ItemType ItemType => _type;
        public string Name => _name;
        public Sprite Sprite => _sprite;
		public int Points => _points;
    }
}
