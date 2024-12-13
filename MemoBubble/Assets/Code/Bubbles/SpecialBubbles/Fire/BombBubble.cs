using UnityEngine;

namespace MemoBubble
{
/// <summary>
/// Defines the features and functions of the bomb bubbles.
/// </summary>
///
/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
	public class BombBubble : Bubble
	{
		[SerializeField] private GameObject _explosionPrefab;
		protected override BubbleType Type
		{
			get { return BubbleType.Bomb; }
		}
		[SerializeField] private bool _moveLeft = false;
		public bool MoveLeft { set { _moveLeft = value; } }

		protected override void Awake()
		{
			CanPop(true);
		}

		protected override void Start()
		{
			base.Start();
			if (_moveLeft)
			{
				ChangeXDirection();
			}
		}

		private void FixedUpdate()
		{
			if (_canMoveBubble)
			{
				BubbleMovement();
			}
		}

		public override void PopBubble()
		{
			GameObject explosion = Instantiate(_explosionPrefab, Vector3.zero, Quaternion.identity);
			Destroy(explosion, 1);
			base.PopBubble();
		}
	}
}
