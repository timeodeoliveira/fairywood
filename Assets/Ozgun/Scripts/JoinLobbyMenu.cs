using Xenolevrai;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Xenolevrai
{
    public class JoinLobbyMenu : MonoBehaviour
    {
        [Header("Network Manager")]
        [SerializeField] private NetworkManagerLobby networkManager = null;

        [Header("UI")]
        [SerializeField] private GameObject landingPagePanel = null;
        [SerializeField] private TMP_InputField ipAddressInputField = null;
        [SerializeField] private Button joinButton = null;

        private void OnEnable()
        {
            if (NetworkManagerLobby.Instance != null)
            {
                NetworkManagerLobby.Instance.OnClientConnected += HandleClientConnected;
                NetworkManagerLobby.Instance.OnClientDisconnected += HandleClientDisconnected;
            }
        }

        private void OnDisable()
        {
            if (NetworkManagerLobby.Instance != null)
            {
                NetworkManagerLobby.Instance.OnClientConnected -= HandleClientConnected;
                NetworkManagerLobby.Instance.OnClientDisconnected -= HandleClientDisconnected;
            }
        }

        public void JoinLobby()
        {
            if (ipAddressInputField == null || string.IsNullOrWhiteSpace(ipAddressInputField.text))
            {
                Debug.LogError("IP address field is empty or invalid.");
                return;
            }

            string ipAddress = ipAddressInputField.text.Trim();
            networkManager.StartClient(ipAddress);

            joinButton.interactable = false;
        }

        private void HandleClientConnected()
        {
            Debug.Log("Client successfully connected.");
            joinButton.interactable = true;
            gameObject.SetActive(false);
            landingPagePanel?.SetActive(false);
        }

        private void HandleClientDisconnected()
        {
            Debug.LogWarning("Client disconnected or failed to connect.");
            joinButton.interactable = true;
        }
    }
}
