using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

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
    public Image healthBar;  // Assure-toi d'assigner ça dans l'Inspector

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
        // Empêche la santé de dépasser 100 ou d’être négative
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        // Vérifie les touches pour test
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(20);
        }

        // Met à jour la barre de vie
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
        if (healthBar != null) // Évite l'erreur si healthBar n'est pas assigné
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
        // Ajoute ici un écran de game over ou désactive le joueur
    }
}
