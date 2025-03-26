using UnityEngine;

public class PeriodicTableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject periodicTableObject; // Assign in Inspector
    [SerializeField] private float destroyTime = 30f; // Adjustable in Inspector

    void Start()
    {
        if (periodicTableObject == null)
        {
            Debug.LogError("Periodic Table Object is NOT assigned in the Inspector!");
            return;
        }

        // Schedule the periodic table to be destroyed after 'destroyTime' seconds
        Destroy(periodicTableObject, destroyTime);
    }
}
