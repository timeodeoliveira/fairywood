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
    public Image healthBar;  // Assure-toi d'assigner �a dans l'Inspector

    void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponent<Image>();
            if (healthBar == null)
            {
                Debug.LogError("HealthBar non assign�e ! V�rifie l�Inspector ou le type d�objet.");
            }
        }
    }


    void Update()
    {
        // Emp�che la sant� de d�passer 100 ou d��tre n�gative
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        // V�rifie les touches pour test
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(20);
        }

        // Met � jour la barre de vie
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
        if (healthBar != null) // �vite l'erreur si healthBar n'est pas assign�
        {
            healthBar.fillAmount = healthAmount / 100f;
        }
        else
        {
            Debug.LogWarning("healthBar n'est pas assign� dans l'Inspector !");
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");
        // Ajoute ici un �cran de game over ou d�sactive le joueur
    }
}
