using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 10f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); 
                Debug.Log("Player hit! Health reduced by: " + damage);
            }
            Destroy(gameObject); 
        }
        else if (collision.CompareTag("Wall")) 
        {
            Destroy(gameObject);
        }
    }
}
