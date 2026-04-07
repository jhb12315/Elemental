using Elemental.Gameplay.Resource;
using Elemental.Gameplay.Resource.Cut;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Behaviour
{
    public class HarvestFairy : MonoBehaviour
    {
        Vector2 alterPosition;
        List<Collider2D> resourceTargets = new List<Collider2D>(30);
        Collider2D target;

        Rigidbody2D rigid;
        ResourceType resourceType;

        public bool isArrived;

        float harvestTimeInterval;
        float moveSpeed;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Alter alter, List<Collider2D> resources, HarvestFairyData data)
        {
            isArrived = false;
            alterPosition = alter.transform.position;
            resourceType = data.resourceType;
            harvestTimeInterval = data.harvestTimeInterval;
            moveSpeed = data.speed;
            SetTargetList(resources);
        }

        void FixedUpdate()
        {
            if (isArrived || target == null) return;
            rigid.linearVelocity = (target.ClosestPoint(target.transform.position) - (Vector2) transform.position).normalized * moveSpeed;
        }

        void GetNextTarget()
        {
            resourceTargets.Sort((a, b) => ((Vector2) a.transform.position - alterPosition).sqrMagnitude.CompareTo(((Vector2) b.transform.position - alterPosition).sqrMagnitude));
            target = resourceTargets[0];
        }

        void SetTargetList(List<Collider2D> resources)
        {
            foreach (var resource in resources)
            {
                AddTarget(resource);
            }

            StartCoroutine(MoveToTarget());
        }

        // 필드에 자원이 추가 됐을 때
        public void AddTarget(Collider2D resource)
        {
            if (Enum.TryParse<ResourceType>(resource.gameObject.tag, out var type) && ((type & resourceType) != 0))
            {
                resourceTargets.Add(resource);
            }
        }

        public void RemoveTarget(Collider2D resource)
        {
            resourceTargets.Remove(resource);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider != target) return;
            isArrived = true;
            rigid.linearVelocity = Vector2.zero;
        }

        IEnumerator MoveToTarget()
        {
            while (true)
            {
                if (resourceTargets.Count == 0) yield break;

                GetNextTarget();
                ICuttable resourceCut = target.GetComponent<ICuttable>();
                isArrived = false;
                yield return new WaitUntil(() => isArrived);
                yield return StartCoroutine(Harvest(resourceCut));
            }
        }

        IEnumerator Harvest(ICuttable resourceCut)
        {
            while (true)
            {
                if (resourceCut.IsReturned)
                {
                    RemoveTarget(target);
                    yield break;
                }
                resourceCut.Cut();
                yield return new WaitForSeconds(harvestTimeInterval);
                
            }
        }
    }
}