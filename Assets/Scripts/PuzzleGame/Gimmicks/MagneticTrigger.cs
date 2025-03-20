using UnityEngine;

public class MagneticTrigger : MonoBehaviour
{
    private MagneticMover _magneticMover;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _magneticMover = GetComponentInParent<MagneticMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _magneticMover.ActivateMovement(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _magneticMover.ReleasePlayer();
        }
    }
    
}
