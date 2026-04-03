namespace Elemental.Gameplay.item
{
    public class ItemSlot
    {
        ItemData itemData;
        public int MaxOverlapCount => itemData.maxOverlapCount;
        public int CurrentCount { get; private set; }

        public ItemSlot(ItemData data)
        {
            itemData = data;

            CurrentCount = 1;
        }

        public void AddItem()
        {
            CurrentCount++;
        }
    }
}