using UnityEngine;
using DG.Tweening;

public class FloatingBubble : MonoBehaviour
{
    [Header("Scale (Breathing) Settings")]
    public Vector3 scaleVariance = new Vector3(1.2f, 1.2f, 1.2f);
    public float scaleDurationMin = 1f;
    public float scaleDurationMax = 2f;

    [Header("Position (Floating) Settings")]
    public Vector3 floatRange = new Vector3(0.1f, 0.1f, 0.1f);
    public float floatDurationMin = 1.5f;
    public float floatDurationMax = 3f;

    [Header("Rotation (Spinning) Settings")]
    public Vector3 rotationSpeedMin = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 rotationSpeedMax = new Vector3(2f, 2f, 2f);

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;

        AnimateScale();
        AnimatePosition();
        AnimateRotation();
    }

    void AnimateScale()
    {
        Vector3 randomScale = originalScale + new Vector3(
            Random.Range(-scaleVariance.x, scaleVariance.x),
            Random.Range(-scaleVariance.y, scaleVariance.y),
            Random.Range(-scaleVariance.z, scaleVariance.z)
        );

        float duration = Random.Range(scaleDurationMin, scaleDurationMax);

        transform.DOScale(randomScale, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => AnimateScale());
    }

    void AnimatePosition()
    {
        Vector3 randomPosition = originalPosition + new Vector3(
            Random.Range(-floatRange.x, floatRange.x),
            Random.Range(-floatRange.y, floatRange.y),
            Random.Range(-floatRange.z, floatRange.z)
        );

        float duration = Random.Range(floatDurationMin, floatDurationMax);

        transform.DOMove(randomPosition, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => AnimatePosition());
    }

    void AnimateRotation()
    {
        Vector3 rotationSpeed = new Vector3(
            Random.Range(rotationSpeedMin.x, rotationSpeedMax.x) * (Random.value > 0.5f ? 1 : -1),
            Random.Range(rotationSpeedMin.y, rotationSpeedMax.y) * (Random.value > 0.5f ? 1 : -1),
            Random.Range(rotationSpeedMin.z, rotationSpeedMax.z) * (Random.value > 0.5f ? 1 : -1)
        );

        transform.DORotate(rotationSpeed * 360f, 20f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
