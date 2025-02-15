using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public GameObject arrowPrefab;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    public override void Move()
    {
        if (Vector2.Distance(transform.position, player.position) > 5f)
        {
            base.Move();
        }
    }

    public void Attack()
    {
        if (player != null && Time.time > lastAttackTime + attackCooldown)
        {
            if (CanSeePlayer()) // Only shoot if player is visible
            {
                lastAttackTime = Time.time;
                ShootArrow();
            }
        }
    }

    // Visibility Check Function
    private bool CanSeePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player")) // Enemy has clear sight
            {
                return true;
            }
        }
        return false; // Something is blocking sight
    }


    void ShootArrow()
    {
        if (arrowPrefab != null && player != null)
        {
            Vector2 shootDirection = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * 5f;
            }
        }
    }



    void Update()
    {
        Move();
        Attack();
    }
}
