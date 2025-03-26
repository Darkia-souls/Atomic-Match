using System.Collections.Generic;
using UnityEngine;

namespace AtomicMatch.Scripts
{
    public class ElementSpawner : MonoBehaviour
    {
        /*This code is to attach to a Spawner for the Element, so where it will be placed
     is where the element will spawn from*/
        //The list of the elements with their matching objects, add prefabs in Inspector
        [System.Serializable]
        public class ElementPair
        {
            public GameObject elementPrefab;  // The chemical element
            public GameObject matchingPrefab; // The correct matching object
        }
    
        //This is so that we can access the CurrentMatchingElement in SpawnManager
        public static GameObject CurrentMatchingElement { get; private set; }

        [SerializeField] private List<ElementPair> allElementPairs; // All possible elements
        private List<ElementPair> availableElementPairs; // Dynamic list for tracking used elements
        private ElementPair chosenPair;
        
        private void Awake()
        {
            ResetElementPool(); // Initialize available elements
        }
        
        public GameObject GetMatchingElement() => chosenPair.matchingPrefab;
    
        public void SpawnRandomElement()
        {
            if (availableElementPairs.Count == 0)
            {
                Debug.LogWarning("✅ No more elements left! Game should end.");
                RoundManagerV3.Instance.EndGame();
                return;
            }
            
            // Pick a random element-matching pair
            chosenPair = allElementPairs[Random.Range(0, allElementPairs.Count)];

            // Spawn the element
            Instantiate(chosenPair.elementPrefab, transform.position, Quaternion.identity);

            // Ensure only ONE instance sets the CurrentMatchingElement
            if (CurrentMatchingElement == null)
            {
                CurrentMatchingElement = chosenPair.matchingPrefab; // ✅ Assign only ONCE
            }
            
            // Remove the chosen pair so it won't repeat
            availableElementPairs.Remove(chosenPair);
        }
        
        public void ResetElementPool()
            {
                availableElementPairs = new List<ElementPair>(allElementPairs); // Restore all elements
                Debug.Log("🔄 Element pool reset! Available elements: " + availableElementPairs.Count);
            }
        
        public bool HasElementsLeft()
        {
            return availableElementPairs.Count > 0;
        }
        
        public void ResetGame()
        {
            ResetElementPool();
        }
        
    }
}
