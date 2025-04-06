using UnityEngine;
using DG.Tweening; // Ensure you have DOTween imported

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private float duration = 15f;
    [SerializeField] private float finalScalePercentage = 0.01f;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private bool initialized = false;

    void OnEnable()
    {
        if (!initialized)
        {
            initialScale = transform.localScale;
            initialPosition = transform.position;
            initialized = true;
        }

        StartLoading();
    }

    void StartLoading()
    {
        // Reset to initial values before starting the animation
        transform.localScale = initialScale;
        transform.position = initialPosition;

        // Animate again
        transform.DOScaleX(initialScale.x * finalScalePercentage, duration).SetEase(Ease.Linear);
        transform.DOMoveX(initialPosition.x - (initialScale.x * (1 - finalScalePercentage) * 0.5f), duration).SetEase(Ease.Linear);
    }
}