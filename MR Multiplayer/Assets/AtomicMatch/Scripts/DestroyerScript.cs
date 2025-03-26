using AtomicMatch.Scripts;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ElementSpawner.CurrentMatchingElement)
        {
            Destroy(other.gameObject); // Remove correct object
            RoundManagerV3.Instance.StartNextRound(); // Start next round
        }
    }
}
