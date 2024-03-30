using Hmxs.Scripts.Player;
using Mirror;
using UnityEngine;

namespace Hmxs.Scripts.Network
{
    public class BlindTrustNetworkManager : NetworkManager
    {
        [SerializeField] [ReadOnly] private int playerCount = 0;

        public new static BlindTrustNetworkManager singleton => (BlindTrustNetworkManager)NetworkManager.singleton;

        private PlayerReference _playerReference;

        private struct CreatePlayerMessage : NetworkMessage
        {
            public int PlayerCount;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            _playerReference = PlayerReference.Instance;
            if (_playerReference == null)
            {
                Debug.Log("PlayerReference not found");
                return;
            }

            NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
        }

        private void OnCreatePlayer(NetworkConnectionToClient conn, CreatePlayerMessage message)
        {
            var startPos = GetStartPosition();

            if (message.PlayerCount > 1)
            {
                Debug.Log("Player count is already 2");
                return;
            }

            var playerPrefab = message.PlayerCount == 0 ? _playerReference.humanPrefab : _playerReference.dogPrefab;
            var playerObj = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);

            NetworkServer.AddPlayerForConnection(conn, playerObj);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            NetworkClient.Send(new CreatePlayerMessage { PlayerCount = playerCount });

            playerCount++;
        }
    }
}