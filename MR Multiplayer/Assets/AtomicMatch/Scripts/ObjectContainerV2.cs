using UnityEngine;

public class ObjectContainerV2 : MonoBehaviour
{
    public string containerSide; // "Blue" or "Red"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("correct"))
        {
            // Notify the RoundManager to register the point and activate the next pack
            RoundManagerV2 roundManager = FindFirstObjectByType<RoundManagerV2>();
            if (roundManager != null)
            {
                roundManager.RegisterPoint(containerSide);
            }

            Destroy(other.gameObject);
        }
    }
}
