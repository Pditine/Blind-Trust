using Hmxs.Scripts.Player;
using LJH.Scripts;
using Mirror;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Hmxs.Scripts.Network
{
    public class BlindTrustNetworkManager : NetworkManager
    {
        [SerializeField] [ReadOnly] private int playerCount = 0;

        public new static BlindTrustNetworkManager singleton => (BlindTrustNetworkManager)NetworkManager.singleton;

        private PlayerReference _playerReference;

        public override void OnStartServer()
        {
            base.OnStartServer();

            _playerReference = PlayerReference.Instance;
            if (_playerReference == null)
                Debug.Log("PlayerReference not found");
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            var startPos = GetStartPosition();

            if (playerCount > 1)
            {
                Debug.Log("Player count is already 2");
                return;
            }

            var player = playerCount == 0 ? _playerReference.humanPrefab : _playerReference.dogPrefab;
            var playerObj = startPos != null
                ? Instantiate(player, startPos.position, startPos.rotation)
                : Instantiate(player);

            NetworkServer.AddPlayerForConnection(conn, playerObj);

            playerCount++;

            GameManager.Instance.SetPlayer(playerObj);
            if(playerCount>=2)
            {
                DelayUtility.Delay(2,GameManager.Instance.RpcGameStart);
            }
        }
    }
}