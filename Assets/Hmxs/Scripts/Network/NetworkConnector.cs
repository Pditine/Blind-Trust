using System.Net;
using System.Net.Sockets;
using Mirror;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.Network
{
    public class NetworkConnector : MonoBehaviour
    {
        [Title("Start")]
        [SerializeField] private TMP_InputField ipInputField;
        [SerializeField] private Button hostButton;
        [SerializeField] private Button clientButton;

        [Title("Connected")]
        [SerializeField] private TMP_Text infoText;

        [Title("RootReference")]
        [SerializeField] private GameObject startRoot;
        [SerializeField] private GameObject connectedRoot;

        private string _ip;
        private NetworkManager _manager;

        private void Start()
        {
            ipInputField.onValueChanged.AddListener(s => _ip = s);
            hostButton.onClick.AddListener(OnHost);
            clientButton.onClick.AddListener(OnClient);

            _manager = NetworkManager.singleton;
            ipInputField.text = _manager.networkAddress;
        }

        private void OnHost()
        {
            if (NetworkServer.active)
            {
                Debug.Log("Already connected");
                return;
            }
            _manager.StartHost();

            if (NetworkClient.active && NetworkServer.active)
            {
                infoText.text = "Host Start Successfully.\nIP: " + GetLocalIP();
                startRoot.SetActive(false);
                connectedRoot.SetActive(true);
                return;
            }
            Debug.Log("Host Start Failed");
        }

        private void OnClient()
        {
            if (NetworkClient.isConnected)
            {
                Debug.Log("Already connected");
                return;
            }
            _manager.networkAddress = _ip;
            _manager.StartClient();

            if (NetworkClient.active)
            {
                infoText.text = "Client Connected Successfully.";
                startRoot.SetActive(false);
                connectedRoot.SetActive(true);
                return;
            }
            Debug.Log("Client Connected Failed");
        }

        private static string GetLocalIP()
        {

            var ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in ipEntry.AddressList)
                if (item.AddressFamily == AddressFamily.InterNetwork)
                    return item.ToString();
            return "Get Failed";
        }
    }
}