using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;                 
    private int currentHealth; 
    public GameObject panel;                 

    [Header("UI Settings")]
    public Scrollbar healthBarScrollbar;        
    void Start()
    {
       
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

   
    private void UpdateHealthBar()
    {
        if (healthBarScrollbar != null)
        {
            healthBarScrollbar.size = (float)currentHealth / maxHealth;
        }
    }

   
    private void Die()
    {
     Time.timeScale = 0;
     panel.SetActive(true);
      Cursor.visible=true;
         Cursor.lockState = CursorLockMode.None;
        //LoadLevel();    
       
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}