using Mirror;
using UnityEngine;

public class MovePlayer : NetworkBehaviour
{
    public float speed = 3f; // Vitesse de déplacement
    private Rigidbody2D rb;
    private Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Seul le client local peut contrôler ce joueur
        if (!isLocalPlayer) return;

        // Récupère les entrées du joueur
        dir.x = Input.GetAxisRaw("Horizontal"); // Gauche/droite
        dir.y = Input.GetAxisRaw("Vertical");   // Haut/bas

        // Envoie les entrées au serveur pour déplacer le joueur
        if (dir != Vector2.zero)
        {
            CmdMove(dir);
        }
        else
        {
            // Si aucune touche n'est enfoncée, réinitialise la vitesse
            CmdStop();
        }
    }

    [Command]
    private void CmdMove(Vector2 direction)
    {
        // Déplace le joueur sur le serveur
        rb.velocity = direction * speed;

        // Synchronise la vitesse avec tous les clients
        RpcUpdateVelocity(rb.velocity);
    }

    [Command]
    private void CmdStop()
    {
        // Arrête le joueur sur le serveur
        rb.velocity = Vector2.zero;

        // Synchronise l'arrêt avec tous les clients
        RpcUpdateVelocity(Vector2.zero);
    }

    [ClientRpc]
    private void RpcUpdateVelocity(Vector2 newVelocity)
    {
        // Met à jour la vitesse du joueur sur tous les clients
        rb.velocity = newVelocity;
    }
}