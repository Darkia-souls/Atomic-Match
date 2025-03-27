using UnityEngine;

public class ObjectContainerV2 : MonoBehaviour
{
    public string containerSide; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("correct"))
        {
           
            RoundManagerV2 roundManager = FindFirstObjectByType<RoundManagerV2>();
            if (roundManager != null)
            {
                roundManager.RegisterPoint(containerSide);
            }
        }

        Destroy(other.gameObject);
    }
}
