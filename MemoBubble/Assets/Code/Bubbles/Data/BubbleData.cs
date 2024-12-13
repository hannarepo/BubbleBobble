using UnityEngine;


namespace MemoBubble
{
	// Create a new Asset to menu to build new prefab assets
	[CreateAssetMenu(fileName = "New Bubble", menuName = "Bubble")]
	public class BubbleData : ScriptableObject
	{
		[SerializeField] private BubbleType _type = BubbleType.None;
		[SerializeField] private string _name = "";
		[SerializeField] private Sprite _sprite;
		[SerializeField] private int _points;

		public BubbleType BubbleType => _type;
		public string Name => _name;
		public Sprite Sprite => _sprite;
		public int Points => _points;
	}
}
