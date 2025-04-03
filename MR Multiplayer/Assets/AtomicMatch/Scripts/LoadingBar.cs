using UnityEngine;
using DG.Tweening; // Ensure you have DOTween imported

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private float duration = 15f; // Duration of the loading effect
    [SerializeField] private float finalScalePercentage = 0.01f; // Final scale percentage (default to 1%)
    private Vector3 initialScale;
    private Vector3 initialPosition;

    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;

        StartLoading();
    }

    void StartLoading()
    {
        // Animate the X scale from full size to the final percentage
        transform.DOScaleX(initialScale.x * finalScalePercentage, duration).SetEase(Ease.Linear)
            .OnKill(DestroyObject); // When animation completes, destroy the object

        // Optionally, adjust position to make sure the bar shrinks to the right
        transform.DOMoveX(initialPosition.x - (initialScale.x * (1 - finalScalePercentage) * 0.5f), duration).SetEase(Ease.Linear);
    }

    // Method to destroy the object
    void DestroyObject()
    {
        Destroy(gameObject); // Destroys the game object
    }
}
