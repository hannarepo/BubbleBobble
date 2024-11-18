using UnityEngine;

namespace BubbleBobble
{
/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
/// 
/// <summary>
/// Defines the features and functions of the water bubbles.
/// </summary>
	public class WaterBubble : Bubble
	{
		protected override BubbleType Type
		{
			get { return BubbleType.Water; }
		}

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

		private void FixedUpdate()
		{
			if (_canMoveBubble)
			{
				BubbleMovement();
			}
		}

		#endregion Unity Functions

		public override void PopBubble()
		{
			base.PopBubble();
			// Instantiate something to drag the player and enemies with it
		}
	}
}
