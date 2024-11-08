using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform player;                        
    public float attackRange = 2f;                  
    public int damageAmount = 50;                   
    public float attackInterval = 2f;               
    public float attackCooldown = 5f;               

    [Header("Jump Scare Settings")]
    public GameObject jumpScareEffect;              
    public float jumpScareDuration = 1f;            

    private Health playerHealth;                   
    private float nextAttackTime = 0f;              
    private bool isOnCooldown = false;              
    void Start()
    {
        playerHealth = player.GetComponent<Health>();  
    }

    void Update()
    {
        
        if (IsPlayerInRange() && Time.time >= nextAttackTime && !isOnCooldown)
        {
            AttemptAttack();
            StartCooldown();
        }
    }

    
    bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    
    void AttemptAttack()
    {
        int attackChoice = Random.Range(0, 2); 

        if (attackChoice == 0)
        {
           
            DealDamage();
        }
        else
        {
            
            TriggerJumpScareKill();
        }
    }

   
    void DealDamage()
    {
        Debug.Log("Enemy attacks and deals " + damageAmount + " damage to the player.");
        playerHealth.TakeDamage(damageAmount);
    }

    
    void TriggerJumpScareKill()
    {
        Debug.Log("Enemy triggers a jump scare kill!");
        StartCoroutine(JumpScareAndKill());
        Time.timeScale = 0;
        Cursor.visible=true;
         Cursor.lockState = CursorLockMode.None;
        //LoadLevel();
    }

    
    System.Collections.IEnumerator JumpScareAndKill()
    {
        if (jumpScareEffect != null)
        {
            jumpScareEffect.SetActive(true);  
        }

        yield return new WaitForSeconds(jumpScareDuration);  
        if (jumpScareEffect != null)
        {
            jumpScareEffect.SetActive(false);  
        }

        playerHealth.TakeDamage(playerHealth.maxHealth);  
    }

   
    void StartCooldown()
{
    isOnCooldown = true;
    nextAttackTime = Time.time + attackCooldown;  // Set next possible attack time
    StartCoroutine(CooldownCoroutine());          // Start the cooldown coroutine
}

// Coroutine for cooldown
System.Collections.IEnumerator CooldownCoroutine()
{
    yield return new WaitForSeconds(attackCooldown);
    isOnCooldown = false;
}
    public void LoadLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }

}