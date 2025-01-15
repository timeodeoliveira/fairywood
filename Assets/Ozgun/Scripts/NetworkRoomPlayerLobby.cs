using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Xenolevrai
{
    public class NetworkRoomPlayerLobby : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject lobbyUI = null;
        [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
        [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
        [SerializeField] private Button startGameButton = null;

        public string DisplayName { get; private set; } = "Loading...";
        public bool IsReady { get; private set; } = false;

        private bool isLeader;
        public bool IsLeader
        {
            set
            {
                isLeader = value;
                startGameButton.gameObject.SetActive(value);
            }
        }

        private List<NetworkRoomPlayerLobby> roomPlayers = new List<NetworkRoomPlayerLobby>();
        private NetworkManagerLobby networkManager;

        private void Awake()
        {
            networkManager = FindObjectOfType<NetworkManagerLobby>();
            if (networkManager == null)
            {
                Debug.LogError("NetworkManagerLobby not found in the scene.");
            }
        }

        public void OnStartAuthority()
        {
            DisplayName = PlayerNameInput.DisplayName;
            SendDisplayNameToServer(DisplayName);
            lobbyUI?.SetActive(true);
        }

        public void OnStartClient()
        {
            roomPlayers.Add(this);
            UpdateDisplay();
        }

        public void OnNetworkDestroy()
        {
            roomPlayers.Remove(this);
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (!networkManager || Event.current != null && Event.current.type != EventType.Repaint) return;

            for (int i = 0; i < playerNameTexts.Length; i++)
            {
                playerNameTexts[i].text = "Waiting For Player...";
                playerReadyTexts[i].text = string.Empty;
            }

            for (int i = 0; i < roomPlayers.Count; i++)
            {
                playerNameTexts[i].text = roomPlayers[i].DisplayName;
                playerReadyTexts[i].text = roomPlayers[i].IsReady ? "<color=green>Ready</color>" : "<color=red>Not Ready</color>";
            }
        }

        public void NotifyStartGame()
        {
            Debug.Log("Game is starting...");
            SceneManager.LoadScene("RoomPlayer");
        }

        public void HandleReadyToStart(bool readyToStart)
        {
            if (!isLeader) return;
            startGameButton.interactable = readyToStart;
        }

        public void ToggleReadyStatus()
        {
            IsReady = !IsReady;
            SendReadyStatusToServer(IsReady);
        }

        public void StartGame()
        {
            if (!isLeader) return;
            networkManager.NotifyStartGame();
        }

        private void SendDisplayNameToServer(string displayName)
        {
            networkManager.SendToServer($"SET_DISPLAY_NAME|{displayName}");
        }

        private void SendReadyStatusToServer(bool isReady)
        {
            networkManager.SendToServer($"SET_READY_STATUS|{isReady}");
        }
    }
}
