using Elemental.Gameplay.Resource;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Harvest
{
    public interface IResourceFindable
    {
        Collider2D GetNextTarget(Vector2 fairyPosition, ResourceTag resourceTag);
    }
}