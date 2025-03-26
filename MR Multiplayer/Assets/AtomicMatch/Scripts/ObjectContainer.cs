using UnityEngine;

public class ObjectContainer : MonoBehaviour
{
    public string containerSide; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("correct"))
        {
            
            RoundManager.Instance.RegisterPoint(containerSide);
        }

       
    
    }
}

