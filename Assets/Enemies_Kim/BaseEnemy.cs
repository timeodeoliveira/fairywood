using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 100;
    protected Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Move()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

    void Update()
    {
        Move();
    }
}
