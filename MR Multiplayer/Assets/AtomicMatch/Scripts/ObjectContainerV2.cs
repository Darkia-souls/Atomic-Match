using UnityEngine;

public class ObjectContainerV2 : MonoBehaviour
{
    public string containerSide; // "Blue" or "Red"
    public AudioClip destroySound; // Assign this in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

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

        // Play the destroy sound if assigned
        if (destroySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destroySound);
        }

        Destroy(other.gameObject);
    }
}
