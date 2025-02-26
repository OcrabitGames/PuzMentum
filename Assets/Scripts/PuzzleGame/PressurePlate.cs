using System;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject target;
    private bool activated = false;
    Vector3 _activatedPosition;
    Vector3 _originalPosition;
    
    // Trigger Frames
    private int lastTriggerFrame = 0;
    private int frameThreshold = 100;

    private void Start()
    {
        // Set positions
        _originalPosition = target.transform.position;
        _activatedPosition = target.transform.position;
        
        _activatedPosition.y = -0.99f;
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
    
    private void OnTriggerStay(Collider collider)
    {
        if (!activated && collider.gameObject.tag == "AfterImage")
        {
            activated = true;
            ActivateTarget();
        }
        lastTriggerFrame = Time.frameCount;
    }

    private void ActivateTarget()
    {
        target.transform.position = _activatedPosition;
    }
    
    private void DeactivateTarget()
    {
        target.transform.position = _originalPosition;
    }
}
