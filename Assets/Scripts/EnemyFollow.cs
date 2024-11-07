using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Detection Settings")]
    public Transform player;                  // Reference to the player
    public float detectionRadius = 10f;       // Radius within which the enemy can detect the player
    public float fieldOfViewAngle = 60f;      // Field of view angle (in degrees) for detecting player
    public float followSpeed = 3f;            // Speed at which the enemy follows the player
    public float rotationSpeed = 5f;          // Speed at which the enemy rotates to face the player

    private bool isFollowing = false;         // Tracks whether the enemy is currently following the player

    void Update()
    {
        // Check if the player is within detection radius and field of view
        if (IsPlayerInRange() && IsPlayerInFieldOfView())
        {
            StartFollowing();
        }
        else
        {
            StopFollowing();
        }
    }

    // Check if the player is within the detection radius
    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRadius;
    }

    // Check if the player is within the enemy's field of view
    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        return angleToPlayer <= fieldOfViewAngle / 2f;
    }

    // Start following the player
    void StartFollowing()
    {
        isFollowing = true;
        RotateTowardsPlayer();
        MoveTowardsPlayer();
    }

    // Rotate the enemy to face the player
    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // Move the enemy towards the player
    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    // Stop following the player
    void StopFollowing()
    {
        isFollowing = false;
    }

    // Optional: Visualize detection radius and field of view in the editor
    void OnDrawGizmosSelected()
    {
        // Draw detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw field of view lines
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRadius);
    }
}