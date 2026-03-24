using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Resource.Drop
{
    [CreateAssetMenu(fileName = "DropTable", menuName = "Scriptable Objects/Resource/Drop/DropTable", order = 1)]
    public class DropTable : ScriptableObject
    {
        public List<DropItemData> datas;

        List<(GameObject, int)> dropValues = new List<(GameObject, int)>();

        public List<(GameObject, int)> DropCalculate()
        {
            dropValues.Clear();

            foreach (var data in datas)
            {
                GameObject prefab = data.prefab;
                int count = Random.Range(data.minDropCount, data.maxDropCount);

                dropValues.Add((prefab, count));
            }

            return dropValues;
        }

    }
}
