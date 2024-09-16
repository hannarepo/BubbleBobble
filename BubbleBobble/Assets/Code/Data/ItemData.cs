
using UnityEngine;

namespace BubbleBobble
{
    // Create a new Asset to menu to build new prefab assets
    [CreateAssetMenu(fileName = "New Collectible Item", menuName = "Inventory/Collectible Item")]

    public class ItemData : ScriptableObject
    {
        [SerializeField] private ItemType _type = ItemType.None;
        [SerializeField] private string _name = "";
        [SerializeField] private Sprite _sprite = null;

        public ItemType ItemType => _type;
        public string Name => _name;
        public Sprite Sprite => _sprite;
    }
}
