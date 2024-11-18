using UnityEngine;

namespace BubbleBobble
{
	public class BombBubble : Bubble
	{
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

		private void Update()
		{
			if (_canMoveBubble)
			{
				BubbleMovement();
			}
		}

		public override void PopBubble()
		{
			// Explosion animation/sprite flash
			_gameManager.BubblePopped(Type);
			Destroy(gameObject);
		}
	}
}
