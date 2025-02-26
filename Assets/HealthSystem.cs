using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) 
        {
            TakeDamage(20);
        }
    }

    public float healthAmount = 100f;
    public Image healthBar; 

    void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponent<Image>();
            if (healthBar == null)
            {
                Debug.LogError("HealthBar non assignée ! Vérifie l’Inspector ou le type d’objet.");
            }
        }
    }


    void Update()
    {
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(20);
        }


   UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();

        if (healthAmount <= 0)
        {
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
        }
        else
        {
            Debug.LogWarning("healthBar n'est pas assigné dans l'Inspector !");
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");

        SceneManager.LoadScene("MenuMort");

    }
}
