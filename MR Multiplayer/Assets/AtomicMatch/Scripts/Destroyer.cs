using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound; // Assign sound in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // This will be triggered when another collider enters the trigger area of this object
    private void OnTriggerEnter(Collider other)
    {
        PlayDestroySound();
        Destroy(other.gameObject); // Destroy the object that collided with the Destroyer
    }

    // Plays the destroy sound when triggered
    private void PlayDestroySound()
    {
        if (destroySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destroySound);
        }
    }
}
