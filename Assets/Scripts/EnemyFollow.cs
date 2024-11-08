using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    
    public Transform player;                  
    public float detectionRadius = 10f;      
    public float fieldOfViewAngle = 60f;      
    public float followSpeed = 3f;            
    public float rotationSpeed = 5f;          

    private bool isFollowing = false; 
       public Animator animator;         
     

    void Update()
    {
        
        if (IsPlayerInRange() && IsPlayerInFieldOfView())
        {
            StartFollowing();
        }
        else
        {
            StopFollowing();
        }
    }

    
    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRadius;
    }

    
    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        return angleToPlayer <= fieldOfViewAngle / 2f;
    }

    
    void StartFollowing()
    {
        isFollowing = true;
        RotateTowardsPlayer();
        MoveTowardsPlayer();
        if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
    }

    
    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    
    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    
    void StopFollowing()
    {
        isFollowing = false;
         if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
    }

    
    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRadius);
    }
}