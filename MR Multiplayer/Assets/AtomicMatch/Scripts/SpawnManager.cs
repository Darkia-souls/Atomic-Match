using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AtomicMatch.Scripts
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToSpawn; // All possible objects
        private List<GameObject> availableObjects; // Track available objects
        private List<ObjectSpawnerAM> spawners = new List<ObjectSpawnerAM>(); //List of existing spawners
    
        [SerializeField] private ElementSpawner elementSpawner; // Reference to ElementSpawner Script
        private GameObject currentMatchingObject; // Tracks the spawned correct object
        private bool hasAssignedObjects = false; // Prevent multiple assignments

    
        //This finds all ObjectSpawners in the Scene and does not sort the results, meaning the order is random
        private void Awake()
        {
            spawners.AddRange(FindObjectsByType<ObjectSpawnerAM>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        }
    
        /*This invokes all the elements needed to spawn the objects and the element, OBS! the order of these are
     important, do not change the order*/
        void Start()
        {
            RoundManagerV3.Instance.OnGameRestart += ResetAvailableObjects; // Subscribe to game restart
            StartNextRound(); // Start the first round
        }

        public void StartNextRound()
        {
            // First, ensure ElementSpawner has run to assign the matching element.
            ElementSpawner spawner = Object.FindFirstObjectByType<ElementSpawner>();
            
            if (spawner == null)
            {
                Debug.LogError("❌ ElementSpawner not found in the scene! Make sure ElementSpawner is in the scene.");
                return;
            }
        
            spawner.SpawnRandomElement();  // Ensure the element is spawned and matching element is assigned

            FindAllSpawners();  // Find all spawners first
            
            if (!elementSpawner.HasElementsLeft()) // If no elements are left, end game
            {
                RoundManagerV3.Instance.EndGame();
                return;
            }

            elementSpawner.SpawnRandomElement(); // Spawns the element + sets matching element

            
            // Only call AssignObjects once per round.
            if (!hasAssignedObjects)
            {
                AssignObjects();  // Now we are sure availableObjects is ready
                hasAssignedObjects = true;  // Mark that we have already assigned objects for this round
            }
        }
        private void ResetAvailableObjects()
        {
            if (objectsToSpawn == null || objectsToSpawn.Length == 0)
            {
                Debug.LogError("❌ objectsToSpawn is NULL or EMPTY!");
                return;
            }

            availableObjects = new List<GameObject>(objectsToSpawn);
            Debug.Log("✅ availableObjects initialized with " + availableObjects.Count + " items.");
        }

        private void AssignObjects()
        {
            if (currentMatchingObject != null)
            {
                Destroy(currentMatchingObject); // Remove the old one before spawning a new one
                currentMatchingObject = null; // Reset reference
            }
        
            if (availableObjects.Count < 4)
            {
                Debug.LogError("❌ Not enough unique objects to spawn! Ensure objectsToSpawn has at least 4 unique prefabs.");
                return;
            }
        
            // Get the correct matching object from ElementSpawner
            GameObject matchingObject = ElementSpawner.CurrentMatchingElement;
            if (matchingObject == null)
            {
                Debug.LogError("❌ Matching element prefab is NULL! Make sure ElementSpawner runs first.");
                return;
            }
            // We don't need to remove the matching object from the available pool, it will be used.
            // Ensure that it won't be picked more than once.
        
            // Shuffle the list of available objects, but also shuffle spawners for randomness
            List<GameObject> randomObjects = new List<GameObject>(availableObjects);
            if (randomObjects.Contains(matchingObject))
            {
                randomObjects.Remove(matchingObject);  // Ensure the matching object is NOT included
            }
        
            ShuffleList(randomObjects);  // Shuffle the remaining random objects
            ShuffleList(spawners);        // Shuffle spawners to avoid duplicates
        
            List<ObjectSpawnerAM> usedSpawners = new List<ObjectSpawnerAM>(); // Keep track of used spawners

            for (int i = 0; i < 4; i++) // Spawn exactly 4 random objects
            {
                ObjectSpawnerAM spawner = spawners[i]; //Select a random spawner
                GameObject objToSpawn = randomObjects[i]; //Select a random object
                Instantiate(objToSpawn, spawner.transform.position, Quaternion.identity);
                usedSpawners.Add(spawner); // Mark this spawner as used
            }

            // Choose a random spawner from the remaining spawners (not used)
            List<ObjectSpawnerAM> availableSpawners = spawners.Except(usedSpawners).ToList();
        
            if (availableSpawners.Count == 0)
            {
                Debug.LogError("❌ No available spawners left for the matching object!");
                return;
            }
        
            // Choose a random available spawner
            ObjectSpawnerAM chosenSpawner = availableSpawners[Random.Range(0, availableSpawners.Count)];


            // Spawn the matching element at the chosen spawner’s position
            currentMatchingObject = Instantiate(matchingObject, chosenSpawner.transform.position, Quaternion.identity);

            Debug.Log("✅ Spawned 4 unique objects + Matching Element in unique positions.");
        
        }
    
        private void FindAllSpawners()
        {
            spawners = new List<ObjectSpawnerAM>(FindObjectsByType<ObjectSpawnerAM>(FindObjectsInactive.Include, FindObjectsSortMode.None));

            if (spawners.Count < 4)
            {
                Debug.LogError("❌ Not enough ObjectSpawners in the scene! Ensure there are at least 4 spawners.");
            }
        }
    
        //Shuffles the positions of objects on every "Start"
        private void ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]); // Swap positions
            }
        }
    
    }
}
