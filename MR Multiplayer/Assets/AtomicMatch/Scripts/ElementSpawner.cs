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

        [SerializeField] private List<ElementPair> elementPairs; // All possible elements
        private ElementPair chosenPair;
    
        public GameObject GetMatchingElement() => chosenPair.matchingPrefab;
    
        public void SpawnRandomElement()
        {
            if (elementPairs.Count == 0) return;
        
            if (CurrentMatchingElement != null)
            {
                Debug.LogWarning("⚠️ ElementSpawner is trying to assign another matching element, but one already exists!");
                return; // Prevent multiple assignments
            }

            // Pick a random element-matching pair
            chosenPair = elementPairs[Random.Range(0, elementPairs.Count)];

            // Spawn the element
            Instantiate(chosenPair.elementPrefab, transform.position, Quaternion.identity);

            // Ensure only ONE instance sets the CurrentMatchingElement
            if (CurrentMatchingElement == null)
            {
                CurrentMatchingElement = chosenPair.matchingPrefab; // ✅ Assign only ONCE
            }
        }
    }
}
