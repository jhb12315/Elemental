using UnityEngine;

namespace Elemental.Gameplay.Resource
{
    [CreateAssetMenu(fileName = "ResourceSpawnData", menuName = "Scriptable Objects/ResourceSpawnData")]
    public class ResourceSpawnData : ScriptableObject
    {
        public GameObject prefab;
        public float intervalTime;
        public int maxCount;
    }
}