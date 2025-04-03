using UnityEngine;

public class SphereModda : MonoBehaviour
{
    [SerializeField] private float minScale = 0.9f;  // Minimum scale (slightly smaller)
    [SerializeField] private float maxScale = 1.0f;  // Original scale
    [SerializeField] private float speed = 2.0f;     // Speed of the scaling

    private Vector3 initialScale;
    private float scaleFactor;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the smooth scaling factor using Sin wave
        scaleFactor = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * speed) + 1) / 2);

        // Apply the calculated scale smoothly
        transform.localScale = initialScale * scaleFactor;
    }
}
