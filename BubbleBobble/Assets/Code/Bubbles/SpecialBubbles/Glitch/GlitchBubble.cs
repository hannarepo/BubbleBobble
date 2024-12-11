using UnityEngine;

namespace BubbleBobble
{
/// <summary>
/// Defines the features and functions of the water bubbles.
/// </summary>
///
/// <remarks>
/// author: Jose Mäntylä
/// </remarks>
	public class GlitchBubble : Bubble
	{
		protected override BubbleType Type
		{
			get { return BubbleType.Glitch; }
		}

		[SerializeField] private GameObject _glitchPrefab;
		[SerializeField] private bool _moveLeft = false;
		public bool MoveLeft 
		{ 
			set { _moveLeft = value; }
		}

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
			Instantiate(_glitchPrefab, transform.position, Quaternion.identity, transform.parent);
		}
	}
}
