using UnityEngine;

namespace BubbleBobble
{
	/// <summary>
	/// Class for the collectible items in the game.
	/// Plays collect sfx and particle effect when collected.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

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

		/// <summary>
		/// Collect the item.
		/// Hide the item by disabling renderer and collider so that it can't be collected again.
		/// Destroy the item after a delay. Delay is defined by collect effect duration.
		/// </summary>
		public void Collect()
		{
			_renderer.enabled = false;
			_collider.enabled = false;

			float delay = 1;
			Destroy(gameObject, delay);
		}
	}
}
