namespace Elemental.Gameplay.Resource
{
    public interface ICuttable
    {
        bool IsReturned { get; }
        void Cut();
    }
}