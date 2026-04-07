using System;

namespace Elemental.Gameplay.Resource.Cut
{
    public interface ICuttable
    {
        bool IsReturned { get; }
        void Cut();
    }
}