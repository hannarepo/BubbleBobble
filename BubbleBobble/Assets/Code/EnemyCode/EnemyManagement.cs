using UnityEngine;

namespace BubbleBobble
{
	public class EnemyManagement : MonoBehaviour
	{
		[SerializeField] private Item[] _itemPrefabs;
		[SerializeField] private float _deathDelay = 4f;
		[SerializeField] private float _launchForce = 5f;
		[SerializeField] private Color _deathColor;
		private GameManager _gameManager;
		private Transform _levelParent;
		private Rigidbody2D _rb;
		private SpriteRenderer _spriteRenderer;

		// Find the Game Manager and add this enemy object to the list of enemies
		void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_gameManager = FindObjectOfType<GameManager>();
			_gameManager.AddEnemyToList(gameObject);
			_levelParent = FindObjectOfType<LevelManager>().transform;
		}

		public void Die()
		{
			int randomItem = Random.Range(0, _itemPrefabs.Length);
			Instantiate(_itemPrefabs[randomItem], transform.position, Quaternion.identity, _levelParent);
			_gameManager.RemoveEnemyFromList(gameObject);
		}

		public void LaunchAtDeath()
		{
			gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
			_spriteRenderer.color = _deathColor;
			GetComponent<EnemyMovement>().enabled = false;
			GetComponent<Animator>().enabled = false;

			int randomInt = Random.Range(0, 2);
			Vector2 launchDirection = new Vector2(0, 0);
			if (randomInt == 0)
			{
				launchDirection = new Vector2(-1, 1);
			}
			else if (randomInt == 1)
			{
				launchDirection = new Vector2(1, 1);
			}
			_rb.AddForce(launchDirection * _launchForce, ForceMode2D.Impulse);

			Invoke("Die", _deathDelay);
		}
	}
}
