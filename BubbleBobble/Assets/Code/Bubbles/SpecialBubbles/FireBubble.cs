/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Defines the features and functions of the fire bubbles.
/// </summary>
using UnityEngine;

namespace BubbleBobble
{
	public class FireBubble : Bubble
	{
		protected override BubbleType Type
		{
			get { return BubbleType.Fire; }
		}
		[SerializeField] private GameObject _fireBallPrefab;
		[SerializeField] private bool _moveLeft = false;
		public bool MoveLeft { set { _moveLeft = value; } }

		#region Unity Functions
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

		#endregion Unity Functions

		/// <summary>
		/// Instantiates a fireball when the bubble is popped.
		/// </summary>
		public override void PopBubble()
		{
			base.PopBubble();
			Instantiate(_fireBallPrefab, gameObject.transform.position, Quaternion.identity);
		}
	}
}
