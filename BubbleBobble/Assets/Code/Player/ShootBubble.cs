using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
	public class ShootBubble : MonoBehaviour
	{
		[SerializeField] private GameObject _projectilePrefab;
		[SerializeField] private ProjectileBubble _projectileBubble;
		[SerializeField] private float _spawnOffset = 0.9f;
		[SerializeField] private GameManager _gameManager;

		public void Shoot(bool shoot, bool lookingRight)
		{
			if (shoot)
			{
				GameObject projectile;
				if (lookingRight)
				{
					projectile = Instantiate(_projectilePrefab, new Vector3(transform.position.x + _spawnOffset, transform.position.y, 0), Quaternion.identity);
				}
				else
				{
					projectile = Instantiate(_projectilePrefab, new Vector3(transform.position.x - _spawnOffset, transform.position.y, 0), Quaternion.identity);
				}

				if (projectile.GetComponent<ProjectileBubble>() != null)
				{
					projectile.GetComponent<ProjectileBubble>().Launch(lookingRight);
					_gameManager.AddProjectileToList(projectile);
				}
			}
		}
	}
}
