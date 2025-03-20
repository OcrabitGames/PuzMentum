using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player; 
    private NavMeshAgent agent;
    private Animator animator;
    
    public float detectionRadius = 5f;
    public float distanceLimit = 8f;
    
    public bool startFollowing = false;

    public float hoverSpeed = 2f;
    public float hoverHeight = 0.5f;
    public float lookSpeed = 5f;
    public Quaternion offsetRotation = Quaternion.Euler(0, -75, 0);
    
    private float _startY;
    private Vector3 _startPos;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        
        _startPos = transform.position;
        _startY = _startPos.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceFromHome = Vector3.Distance(transform.position, _startPos);   
        
        if (distanceToPlayer < detectionRadius && distanceFromHome < distanceLimit)
        {
            FollowPlayer(distanceToPlayer);
        }
        else
        {
            ReturnHome();
        }
        
        // Hover the thing
        HoverBounce();
    }
    
    void FollowPlayer(float distance)
    {
        agent.SetDestination(player.position);
        FaceVector(player.position);
        animator.SetBool("AlertMode", true);
    }

    void ReturnHome()
    {
        agent.SetDestination(_startPos);
        FaceVector(_startPos);
        animator.SetBool("AlertMode", false);
    }
    
    void HoverBounce()
    {
        float hoverAmount = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.localPosition += new Vector3(0, hoverAmount, 0);
    }

    void FaceVector(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        if (direction.magnitude < 0.1f) return;
        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * offsetRotation, Time.deltaTime * lookSpeed);
    }
    
}
