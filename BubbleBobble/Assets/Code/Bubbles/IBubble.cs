
namespace BubbleBobble
{
    public interface IBubble
    {
        // Pop the bubble and call the GameManager to handle the bubble type (if needed)
        void PopBubble();

        bool CanPop();
    }
}
