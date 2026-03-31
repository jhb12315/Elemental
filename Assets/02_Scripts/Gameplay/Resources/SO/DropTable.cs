using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Elemental.Gameplay.Resource.Drop
{
    [Serializable]
    public class DropItemData
    {
        public GameObject prefab;
        public int minDropCount;
        public int maxDropCount;

        [Range(0f, 1f)]
        public float dropChance;
    }

    [CreateAssetMenu(fileName = "DropTable", menuName = "Scriptable Objects/Resource/Drop/DropTable", order = 1)]
    public class DropTable : ScriptableObject
    {
        public List<DropItemData> datas;

        public List<(GameObject, int)> DropCalculate()
        {
            List<(GameObject, int)> dropValues = new List<(GameObject, int)>(datas.Count);

            foreach (var data in datas)
            {
                if (data.dropChance >= Random.Range(0f, 1f))
                {
                    dropValues.Add((data.prefab, Random.Range(data.minDropCount, data.maxDropCount + 1)));
                }
            }

            return dropValues;
        }

    }
}
