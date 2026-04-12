using Elemental.Framework.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Resource
{
    public class ResourceDrop : MonoBehaviour
    {
        [SerializeField] DropTable dropTable;
        List<(GameObject, int)> dropValues;

        public void Initialize()
        {
            dropValues = new List<(GameObject, int)>(dropTable.datas.Count);
        }

        public void Drop()
        {
            dropValues = dropTable.DropCalculate();

            foreach (var dropValue in dropValues)
            {
                GameObject prefab = dropValue.Item1;
                int count = dropValue.Item2;

                for (int i = 1; i <= count; i++)
                {
                    PoolManager.Instance.PooledSpawnSetPos(prefab, GetDropPosition());
                }
            }
        }

        Vector2 GetDropPosition()
        {
            float x = Random.Range(transform.position.x - 1, transform.position.x + 1);
            float y = Random.Range(transform.position.y - 1, transform.position.y + 1);
            return new Vector2(x, y);
        }
    }
}