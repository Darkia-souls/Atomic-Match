using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 5f;  // Speed of movement
    public float stopY = 0.4638726f; // Target Y position to stop at

    void Update()
    {
        // Move downward
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Stop when reaching the target position
        if (transform.position.y <= stopY)
        {
            enabled = false; // Disable the script to stop movement
        }
    }
}
