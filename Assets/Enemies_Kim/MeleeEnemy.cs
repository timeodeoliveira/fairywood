using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackRange = 1f;

    public override void Move()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            base.Move(); // Move normally
        }
    }

    public void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            Debug.Log("Melee Enemy Attacks!");
            // Implement player damage
        }
    }

    void Update()
    {
        Move();
        Attack();
    }
}
