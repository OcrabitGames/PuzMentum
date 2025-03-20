using UnityEngine;

public class AnimationBallController : MonoBehaviour
{
    Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 normal = contact.normal;
            float impactForce = collision.impulse.magnitude;
            float forceThreshold = 2.0f;

            // Check if collision is not coming from the top and has significant force
            if (!(Vector3.Dot(normal, Vector3.up) > 0.5f) && impactForce > forceThreshold)
            {
                animator.SetTrigger("IsBouncing");
            }
        }
    }
}
