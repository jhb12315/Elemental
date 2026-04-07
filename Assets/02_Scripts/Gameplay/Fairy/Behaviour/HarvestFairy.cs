using Elemental.Gameplay.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Behaviour
{
    public class HarvestFairy : MonoBehaviour
    {
        Vector2 alterPosition;
        List<Collider2D> resourceTargets = new List<Collider2D>(30);

        Rigidbody2D rigid;
        ResourceType resourceType;

        bool isHarvest;

        float moveSpeed;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Alter alter, List<Collider2D> resources, HarvestFairyData data)
        {
            isHarvest = false;
            alterPosition = alter.transform.position;
            resourceType = data.resourceType;
            moveSpeed = data.speed;
            SetTargetList(resources);
        }

        void FixedUpdate()
        {
            if (resourceTargets.Count == 0 || isHarvest) return;
            rigid.linearVelocity = (GetNextTargetPosition() - (Vector2)transform.position).normalized * moveSpeed;
        }

        Vector2 GetNextTargetPosition()
        {
            resourceTargets.Sort((a, b) => ((Vector2) a.transform.position - alterPosition).sqrMagnitude.CompareTo(((Vector2) b.transform.position - alterPosition).sqrMagnitude));
            return resourceTargets[0].gameObject.transform.position;
        }

        void SetTargetList(List<Collider2D> resources)
        {
            foreach (var resource in resources)
            {
                targetFiltering(resource);
            }
        }

        // 필드에 자원이 추가 됐을 때
        public void targetFiltering(Collider2D resource)
        {
            if (Enum.TryParse<ResourceType>(resource.gameObject.tag, out var type) && ((type & resourceType) != 0))
            {
                resourceTargets.Add(resource);
            }
        }
    }
}