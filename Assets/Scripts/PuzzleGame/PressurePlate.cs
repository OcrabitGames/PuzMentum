using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject target;
    private bool activated = false;
    
    // Trigger Frames
    private int lastTriggerFrame = 0;
    private int frameThreshold = 100;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && Time.frameCount - lastTriggerFrame > frameThreshold)
        {
            activated = false;
            DeactivateTarget();
        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log(collision.gameObject.tag);
    // }
    
    private void OnTriggerStay(Collider collider)
    {
        Debug.Log(lastTriggerFrame);
        if (!activated && collider.gameObject.tag == "AfterImage")
        {
            activated = true;
            ActivateTarget();
        }
        lastTriggerFrame = Time.frameCount;
    }

    private void ActivateTarget()
    {
        target.SetActive(false);
    }
    
    private void DeactivateTarget()
    {
        target.SetActive(true);
    }
}
