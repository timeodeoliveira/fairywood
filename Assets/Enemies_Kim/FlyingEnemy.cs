using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    public float hoverHeight = 3f;

    public override void Move()
    {
        if (player != null)
        {
            Vector2 targetPos = new Vector2(player.position.x, player.position.y + hoverHeight);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    void Update()
    {
        Move();
    }
}
