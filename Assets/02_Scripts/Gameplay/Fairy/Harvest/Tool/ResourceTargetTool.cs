using Elemental.Gameplay.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Harvest
{
    public class ResourceTargetTool : IResourceFindable
    {
        List<Collider2D> harvestTargets = new List<Collider2D>(50);
        ContactFilter2D resourceFilter;
        Vector2 altarPosition;
        Vector2 safeZone;

        public ResourceTargetTool(Vector2 altarPosition, Vector2 safeZone)
        {
            resourceFilter = new ContactFilter2D
            {
                layerMask = LayerMask.GetMask("Resource"),
                useLayerMask = true
            };

            this.altarPosition = altarPosition;
            this.safeZone = safeZone;
        }

        public Collider2D GetNextTarget(Vector2 fairyPosition, ResourceTag resourceTag)
        {
            if (harvestTargets.Count == 0) return null;

            // List.Remove 할 때 자리 이동이 발생하지 않게 거리가 먼 순서대로 정렬
            harvestTargets.Sort((a, b) => ((Vector2) b.transform.position - fairyPosition).sqrMagnitude.CompareTo(((Vector2) a.transform.position - fairyPosition).sqrMagnitude));

            Collider2D target = null;

            for (int i = harvestTargets.Count - 1; i >= 0; i--)
            {
                Collider2D resource = harvestTargets[i];
                if (Enum.TryParse<ResourceTag>(resource.gameObject.tag, out var type) && ((type & resourceTag) != 0))
                {
                    target = resource;
                    harvestTargets.Remove(target);
                    break;
                }
            }

            return target;
        }

        public void CollectHarvestTargets()
        {
            harvestTargets.Clear();
            Physics2D.OverlapBox(altarPosition, safeZone, 0f, resourceFilter, harvestTargets);
        }
    }
}