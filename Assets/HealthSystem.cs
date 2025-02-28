using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))] // Synchronise la sant� et appelle OnHealthChanged lorsqu'elle change
    public float healthAmount = 100f;

    [Header("Health Bar")]
    [SerializeField] private Image healthBar; // R�f�rence � la barre de vie du joueur

    
    void Start()
    {
        // Seul le client local configure sa propre HealthBar
        if (isLocalPlayer)
        {
            // Trouve la HealthBar dans le prefab du joueur
            if (healthBar == null)
            {
                healthBar = GetComponentInChildren<Image>();
                if (healthBar == null)
                {
                    Debug.LogError("HealthBar not found in player's children!");
                }
                else
                {
                    Debug.Log("HealthBar found and assigned!");
                }
            }


            // Active la HealthBar uniquement pour le joueur local
            healthBar.gameObject.SetActive(true);
            // Met � jour la barre de vie au d�marrage
            UpdateHealthBar();
        }
        else
        {
            // D�sactive la HealthBar pour les autres joueurs
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(false);
            }
        }
    }

    // M�thode appel�e lorsque la sant� change
    private void OnHealthChanged(float oldHealth, float newHealth)
    {
        Debug.Log($"Health changed from {oldHealth} to {newHealth}");
        healthAmount = newHealth;
        UpdateHealthBar();

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    // M�thode pour infliger des dommages (appel�e par le client, ex�cut�e sur le serveur)
    [Command]
    public void CmdTakeDamage(float damage)
    {
        Debug.Log($"Player took {damage} damage!");
        healthAmount = Mathf.Clamp(healthAmount - damage, 0, 100);
        Debug.Log("New health: " + healthAmount);

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    // M�thode publique pour infliger des dommages (appel�e par les bots ou les projectiles)
    public void TakeDamage(float damage)
    {
        if (isServer)
        {
            CmdTakeDamage(damage);
        }
    }

    // M�thode pour soigner (appel�e par le client, ex�cut�e sur le serveur)
    [Command]
    public void CmdHeal(float healAmount)
    {
        healthAmount = Mathf.Clamp(healthAmount + healAmount, 0, 100);
        Debug.Log("Player healed! New health: " + healthAmount);
    }

    // Met � jour la barre de vie
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
        if (isServer)
        {
            RpcDie();
        }
    }

    [ClientRpc]
    void RpcDie()
    {
        Debug.Log("Player died!");
        if (isLocalPlayer)
        {
            
            gameObject.SetActive(false);
            SceneManager.LoadScene("MenuMort");
        }
    }

}