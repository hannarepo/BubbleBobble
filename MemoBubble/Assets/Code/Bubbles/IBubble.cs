
namespace MemoBubble
{
	public interface IBubble
	{
		// Pop the bubble and call the GameManager to handle the bubble type (if needed)
		void PopBubble();

		void CanPop(bool canPop);
	}
}
