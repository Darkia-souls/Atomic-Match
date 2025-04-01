using UnityEngine;

public class ObjectContainerV2 : MonoBehaviour
{
    public string containerSide; // "Blue" or "Red"
    public AudioSource audioSource; // Assign this in the Inspector

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
        }

        // Play the destroy sound if assigned and if the tag is "correct" or "incorrect"
        if ((other.CompareTag("correct") || other.CompareTag("Incorrect")) && audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

            Destroy(other.gameObject);
        
    }
}
