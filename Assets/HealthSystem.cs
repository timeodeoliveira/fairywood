using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public float healthAmount = 100f;
    public Image healthBar;

    void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponent<Image>();
            if (healthBar == null)
            {
                Debug.LogError("HealthBar not assigned in Inspector!");
            }
        }
    }

    void Update()
    {
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage called! Damage: " + damage); 

        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();

        if (healthAmount <= 0)
        {
            Debug.Log("Player should die now!"); 
            Die();
        }
    }


    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
            Debug.Log("Health Bar Updated: " + healthBar.fillAmount);
        }
        else
        {
            Debug.LogWarning("Health Bar UI is NOT assigned!");
        }
    }


    void Die()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("MenuMort");
    }
}
