using UnityEngine;

public class ObjectContainerV2 : MonoBehaviour
{
    public string containerSide; // "Blue" or "Red"
    public AudioSource correctSound; // Assign this in the Inspector
    public AudioSource incorrectSound; // Assign this in the Inspector

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

        // Play the correct or incorrect sound based on the object's tag
        if (other.CompareTag("correct") && correctSound != null)
        {
            correctSound.PlayOneShot(correctSound.clip);
        }
        else if (other.CompareTag("Incorrect") && incorrectSound != null)
        {
            incorrectSound.PlayOneShot(incorrectSound.clip);
        }

        Destroy(other.gameObject);
    }
}
