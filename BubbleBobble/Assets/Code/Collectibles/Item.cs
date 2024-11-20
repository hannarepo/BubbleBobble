using System;
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
		[SerializeField] private AudioClip _collectSFX;
		private Renderer _renderer = null;
		private Collider2D _collider = null;
		private Audiomanager _audioManager = null;

		public ItemData ItemData => _itemData;

		public static event Action OnItemCollected;
		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			_collider = GetComponent<Collider2D>();
		}

		private void Start()
		{
			_audioManager = FindObjectOfType<Audiomanager>();
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

			if (_audioManager != null)
			{
				_audioManager.PlaySFX(_collectSFX);
			}

			Destroy(gameObject, delay);
			OnItemCollected?.Invoke();
		}
	}
}
