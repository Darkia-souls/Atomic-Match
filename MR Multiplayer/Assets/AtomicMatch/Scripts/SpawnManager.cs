using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using AtomicMatch.Scripts;
using Chess.Game;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace AtomicMatch.Scripts

{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToSpawn; // All possible objects
        private List<GameObject> availableObjects; // Tracks available objects
        private List<ObjectSpawner> spawners = new List<ObjectSpawner>(); // List of spawners

        [SerializeField] private ElementSpawner elementSpawner; // Reference to ElementSpawner
        private GameObject currentMatchingObject; // Tracks the spawned correct object

        private void Awake()
        {
            spawners.AddRange(FindObjectsByType<ObjectSpawner>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        }

        void Start()
        {
            RoundManagerV3.Instance.OnGameRestart += ResetAvailableObjects; // Subscribe to game restart
            StartNextRound(); // Start the first round
        }

        public void StartNextRound()
        {
            if (!elementSpawner.HasElementsLeft()) // If no elements are left, end game
            {
                RoundManagerV3.Instance.EndGame();
                return;
            }

            elementSpawner.SpawnRandomElement(); // Spawns the element + sets matching element

            AssignObjects(); // Assign random objects + matching object
        }

        private void ResetAvailableObjects()
        {
            availableObjects = new List<GameObject>(objectsToSpawn);
        }

        private void AssignObjects()
        {
            if (currentMatchingObject != null)
            {
                Destroy(currentMatchingObject); // Remove the old correct object
                currentMatchingObject = null; // Reset reference
            }

            if (availableObjects.Count < 4)
            {
                Debug.LogError("❌ Not enough unique objects to spawn!");
                return;
            }

            ShuffleList(availableObjects); // Shuffle objects
            ShuffleList(spawners); // Shuffle spawners

            List<ObjectSpawner> usedSpawners = new List<ObjectSpawner>();

            for (int i = 0; i < 4; i++) // Spawn 4 random objects
            {
                GameObject objToSpawn = availableObjects[i];
                ObjectSpawner spawner = spawners[i];
                Instantiate(objToSpawn, spawner.transform.position, Quaternion.identity);
                usedSpawners.Add(spawner);
            }

            GameObject matchingObject = ElementSpawner.CurrentMatchingElement;

            if (matchingObject == null)
            {
                Debug.LogError("❌ Matching element prefab is NULL!");
                return;
            }

            List<ObjectSpawner> availableSpawners = spawners.Except(usedSpawners).ToList();

            if (availableSpawners.Count == 0)
            {
                Debug.LogError("❌ No spawners left for the matching object!");
                return;
            }

            ObjectSpawner chosenSpawner = availableSpawners[Random.Range(0, availableSpawners.Count)];

            // Spawn the correct matching object
            currentMatchingObject = Instantiate(matchingObject, chosenSpawner.transform.position, Quaternion.identity);

            Debug.Log("✅ Spawned 4 random objects + 1 correct object.");
        }

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