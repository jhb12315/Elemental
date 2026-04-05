using Elemental.Gameplay.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Behaviour
{
    public class HarvestFairy : MonoBehaviour
    {
        HarvestFairyData harvestFairyData;
        List<Collider2D> resourceTargets;

        float speed;

        public void Initialize(List<Collider2D> resources, HarvestFairyData data)
        {
            harvestFairyData = data;
            SetResourceTargets(resources);
        }

        void SetResourceTargets(List<Collider2D> resources)
        {
            foreach (var resource in resources)
            {
                targetFiltering(resource);
            }
        }

        // 필드 자원 추가 됐을 때
        public void targetFiltering(Collider2D resource)
        {
            if (Enum.TryParse<ResourceType>(resource.gameObject.tag, out var type) && ((type & harvestFairyData.resourceType) != 0))
            {
                resourceTargets.Add(resource);
            }
        }
    }
}