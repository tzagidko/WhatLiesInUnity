using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform player;                        // Reference to the player
    public float attackRange = 2f;                  // Range within which the enemy can attack
    public int damageAmount = 50;                   // Damage dealt on a regular attack
    public float attackInterval = 2f;               // Time between each attack attempt

    [Header("Jump Scare Settings")]
    public GameObject jumpScareEffect;              // Jump scare effect (e.g., an image or animation)
    public float jumpScareDuration = 1f;            // Duration of the jump scare effect

    private Health playerHealth;                    // Reference to the player's Health script
    private float nextAttackTime = 0f;              // Time until the enemy can attack again

    void Start()
    {
        playerHealth = player.GetComponent<Health>();  // Get reference to the player's Health script
    }

    void Update()
    {
        // Check if player is within attack range and it's time to attack
        if (IsPlayerInRange() && Time.time >= nextAttackTime)
        {
            AttemptAttack();
            nextAttackTime = Time.time + attackInterval;  // Set the next attack time
        }
    }

    // Check if the player is within the attack range
    bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    // Attempt to attack the player with a 50% chance to deal damage or kill
    void AttemptAttack()
    {
        int attackChoice = Random.Range(0, 2);  // Generates 0 or 1

        if (attackChoice == 0)
        {
            // 50% chance to deal regular damage
            DealDamage();
        }
        else
        {
            // 50% chance to trigger a jump scare kill
            TriggerJumpScareKill();
        }
    }

    // Deal damage to the player
    void DealDamage()
    {
        Debug.Log("Enemy attacks and deals " + damageAmount + " damage to the player.");
        playerHealth.TakeDamage(damageAmount);
    }

    // Trigger jump scare and kill the player
    void TriggerJumpScareKill()
    {
        Debug.Log("Enemy triggers a jump scare kill!");
        StartCoroutine(JumpScareAndKill());
    }

    // Coroutine to handle jump scare and player death
    System.Collections.IEnumerator JumpScareAndKill()
    {
        if (jumpScareEffect != null)
        {
            jumpScareEffect.SetActive(true);  // Activate the jump scare effect
        }

        yield return new WaitForSeconds(jumpScareDuration);  // Wait for the jump scare duration

        if (jumpScareEffect != null)
        {
            jumpScareEffect.SetActive(false);  // Deactivate the jump scare effect
        }

        playerHealth.TakeDamage(playerHealth.maxHealth);  // Instantly reduce player's health to 0
    }
}