using Xenolevrai;
using UnityEngine;

namespace Xenolevrai
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Network Manager")]
        [SerializeField] private NetworkManagerLobby networkManager = null;

        [Header("UI")]
        [SerializeField] private GameObject landingPagePanel = null;

        public void HostLobby()
        {
            if (networkManager == null)
            {
                Debug.LogError("NetworkManagerLobby is not assigned.");
                return;
            }

            networkManager.StartHost();
            landingPagePanel?.SetActive(false);

            Debug.Log("Lobby host started.");
        }
    }

}