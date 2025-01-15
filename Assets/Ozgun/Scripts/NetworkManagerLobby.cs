using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Xenolevrai
{
    public class NetworkManagerLobby : MonoBehaviour
    {
        public static NetworkManagerLobby Instance;

        [Header("Network Settings")]
        [SerializeField] private int port = 7777;

        private TcpListener server;
        private Thread serverThread;

        private TcpClient client;
        private NetworkStream clientStream;

        public event Action OnClientConnected;
        public event Action OnClientDisconnected;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Server Logic
        public void StartHost()
        {
            if (server != null)
            {
                Debug.LogWarning("Server already running.");
                return;
            }

            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            serverThread = new Thread(() =>
            {
                Debug.Log("Server started...");
                while (true)
                {
                    TcpClient newClient = server.AcceptTcpClient();
                    Debug.Log("Client connected!");
                    HandleClient(newClient);
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }


        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            while (client.Connected)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Debug.Log($"Received: {message}");
                    }
                }
                catch
                {
                    Debug.LogError("Client disconnected.");
                    break;
                }
            }
        }

        public void StopHost()
        {
            server?.Stop();
            serverThread?.Abort();
            Debug.Log("Server stopped.");
        }
        #endregion

        #region Client Logic
        public void StartClient(string ipAddress)
        {
            try
            {
                client = new TcpClient(ipAddress, port);
                clientStream = client.GetStream();

                Debug.Log("Connected to server.");
                OnClientConnected?.Invoke();

                Thread clientThread = new Thread(() =>
                {
                    byte[] buffer = new byte[1024];
                    while (client.Connected)
                    {
                        try
                        {
                            int bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                            if (bytesRead > 0)
                            {
                                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                Debug.Log($"Server: {message}");
                            }
                        }
                        catch
                        {
                            Debug.LogError("Disconnected from server.");
                            OnClientDisconnected?.Invoke();
                            break;
                        }
                    }
                });

                clientThread.IsBackground = true;
                clientThread.Start();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error connecting to server: {e.Message}");
            }
        }

        public void Disconnect()
        {
            client?.Close();
            Debug.Log("Disconnected from server.");
        }

        public void SendToServer(string message)
        {
            if (client == null || clientStream == null || !client.Connected)
            {
                Debug.LogError("Cannot send message: Not connected to the server.");
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(message);
            clientStream.Write(data, 0, data.Length);
            Debug.Log($"Message sent to server: {message}");
        }
        #endregion
        public void NotifyStartGame()
        {
            Debug.Log("NotifyStartGame called: Starting the game...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("RoomPlayer");
        }

    }
}
