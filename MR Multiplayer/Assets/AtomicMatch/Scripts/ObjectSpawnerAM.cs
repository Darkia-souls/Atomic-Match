using UnityEngine;

namespace AtomicMatch.Scripts
{
    public class ObjectSpawnerAM : MonoBehaviour
    {
        //This is to attach to an empty GameObject to determine the position of the object
        private GameObject spawnedObject;

        //This is to avoid keeping the objects spawning and keeping the data
        public void SpawnSpecificObject(GameObject prefab)
        {
            if (spawnedObject != null) Destroy(spawnedObject);
            spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
