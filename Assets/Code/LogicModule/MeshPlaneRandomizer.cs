using UnityEngine;

namespace Code.LogicModule
{
    public static class MeshPlaneRandomizer
    {
        public static Vector3 GetSpawnPositionEnemy(MeshCollider planeMesh)
        {
            float x = Random.Range(planeMesh.transform.position.x - Random.Range(0, planeMesh.bounds.extents.x),
                planeMesh.transform.position.x + Random.Range(0, planeMesh.bounds.extents.x));
            float z = Random.Range(planeMesh.transform.position.z - Random.Range(0, planeMesh.bounds.extents.z),
                planeMesh.transform.position.z + Random.Range(0, planeMesh.bounds.extents.z));
            
            return new Vector3(x, 0, z);
        }
    }
}