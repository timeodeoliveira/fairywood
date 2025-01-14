
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed =3f;   //stock la valeur de déplacement
    Rigidbody2D rb;
    Vector2 dir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal"); //gauche/droite, recupère l'axe horizontal
        dir.y = Input.GetAxisRaw("Vertical"); //haut/bas, recupère l'axe vertical
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime); //on recup la position de base et on ajt la direction*vitesse*calibrer le mvt pr vitesse cste
    }
}
