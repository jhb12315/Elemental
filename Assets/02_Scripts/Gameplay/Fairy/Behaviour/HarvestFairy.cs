using Elemental.Gameplay.Resource;
using Elemental.Gameplay.Resource.Cut;
using System.Collections;
using UnityEngine;

namespace Elemental.Gameplay.Fairy.Harvest.Behaviour
{
    public class HarvestFairy : MonoBehaviour
    {
        Rigidbody2D rigid;

        Collider2D target;

        IResourceFindable resourceTool;
        ResourceTag resourceTag;

        public bool isArrived;

        float harvestTimeInterval;
        float moveSpeed;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Initialize(IResourceFindable resourceTool, HarvestFairyData data)
        {
            isArrived = false;
            this.resourceTool = resourceTool;
            resourceTag = data.resourceTag;
            harvestTimeInterval = data.harvestTimeInterval;
            moveSpeed = data.speed;
            StartCoroutine(MoveToTarget());
        }

        void FixedUpdate()
        {
            if (isArrived || target == null) return;
            rigid.linearVelocity = (target.ClosestPoint(target.transform.position) - (Vector2) transform.position).normalized * moveSpeed;
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
                target = resourceTool.GetNextTarget(transform.position, resourceTag);
                if (target == null) yield break;
                ICuttable resourceCut = target.GetComponent<ICuttable>();
                isArrived = false;
                yield return new WaitUntil(() => isArrived || resourceCut.IsReturned);
                if (resourceCut.IsReturned)
                {
                    continue;
                }
                yield return StartCoroutine(Harvest(resourceCut));
            }
        }

        IEnumerator Harvest(ICuttable resourceCut)
        {
            while (true)
            {
                if (resourceCut.IsReturned)
                {
                    yield break;
                }
                resourceCut.Cut();
                yield return new WaitForSeconds(harvestTimeInterval);
            }
        }
    }
}