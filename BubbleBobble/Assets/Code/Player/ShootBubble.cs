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
		private bool _forceBoostIsActive = false;
		private bool _sizeBoostIsActive = false;

		public bool ForceBoostIsActive
		{
			get { return _forceBoostIsActive; }
			set { _forceBoostIsActive = value; }
		}

		public bool SizeBoostIsActive
		{
			get { return _sizeBoostIsActive; }
			set { _sizeBoostIsActive = value; }
		}

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
					projectile.GetComponent<ProjectileBubble>().Launch(lookingRight, _forceBoostIsActive, _sizeBoostIsActive);
					_gameManager.AddProjectileToList(projectile);
				}
			}
		}
	}
}
