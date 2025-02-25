
using UnityEngine;


public class MovePlayer : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal"); // Gauche/Droite
        dir.y = Input.GetAxisRaw("Vertical"); // Haut/Bas

        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 900);
        }
    }
       

  
}




