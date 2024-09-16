/// <remarks>
/// author: Hanna Repo
/// </remarks>
using UnityEngine;

namespace BubbleBobble
{
    /// <summary>
    /// Class for the collectible items in the game.
    /// Plays collect sfx and particle effect when collected.
    /// </summary>
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData = null;
        private Renderer _renderer = null;
        private Collider2D _collider = null;

        public ItemData ItemData => _itemData;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _collider = GetComponent<Collider2D>();
        }

        public void Collect()
        {
            _renderer.enabled = false;
            _collider.enabled = false;

            float delay = 1;
            Destroy(gameObject, delay);
        }
    }
}
