using Mirror;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : NetworkBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // R�f�rence � la CinemachineVirtualCamera

    void Start()
    {
        // Seul le client local configure sa propre cam�ra
        if (isLocalPlayer)
        {
            // Trouve la cam�ra virtuelle dans les enfants du joueur (si elle n'est pas assign�e manuellement)
            if (virtualCamera == null)
            {
                virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
                if (virtualCamera == null)
                {
                    Debug.LogError("CinemachineVirtualCamera not found in player's children!");
                }
                else
                {
                    Debug.Log("CinemachineVirtualCamera found and assigned!");
                }
            }

            // Active la cam�ra virtuelle
            if (virtualCamera != null)
            {
                virtualCamera.gameObject.SetActive(true);

                // Fait suivre le joueur par la cam�ra
                virtualCamera.Follow = transform; // Suit le transform du joueur
                virtualCamera.LookAt = transform; // Regarde le transform du joueur (optionnel)
            }
            else
            {
                Debug.LogError("CinemachineVirtualCamera is not assigned!");
            }
        }
        else
        {
            // D�sactive la cam�ra pour les autres joueurs
            if (virtualCamera != null)
            {
                virtualCamera.gameObject.SetActive(false);
            }
        }
    }
}