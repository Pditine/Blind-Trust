using Cinemachine;
using Hmxs.Scripts.Player;
using Mirror;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace LJH.Scripts
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private Image blackPanel;
        [SerializeField] private GameObject preparationHall;
        [SerializeField] private GameObject gameScene;
        [SerializeField] private Transform humanBirthPoint;
        [SerializeField] private Transform dogBirthPoint;
        [SerializeField] private CinemachineVirtualCamera theCamera;
        private GameObject _thePlayer;
        public GameObject ThePlayer => _thePlayer;
        [SerializeField] private Light2D globalLight; // 弃用
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text message;
        
        [SyncVar] public PlayerDog Dog;
        [SyncVar] public PlayerHuman Human;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            blackPanel.enabled = false;
        }

        private void InitGame()
        {
            theCamera.Follow = ThePlayer.transform;
            preparationHall.SetActive(false);
            gameScene.SetActive(true);
            if (isServer)
            {
                Camera.main.cullingMask = LayerMask.GetMask("Human","HumanAndDog");
                globalLight.intensity = 0;
            }
            else
            {
                Camera.main.cullingMask = LayerMask.GetMask("Default","Traffic","HumanAndDog");
            }

            ThePlayer.transform.position = isServer ? humanBirthPoint.position : dogBirthPoint.position;
            EventSystem.EventTrigger("GameStart");
        }
        
        [ClientRpc]
        public void SetPlayer(GameObject thePlayer)
        {
            if (_thePlayer is not null) return;
            _thePlayer = thePlayer;
        }
        
        [ClientRpc]
        private void RpcGameStart()
        {
            PFCLog.Info("游戏开始");
            PFCLog.Info(NetworkClient.ready);
            gameOverPanel.SetActive(false);
            FadeUtility.FadeInAndStay(blackPanel,80, () =>
            {
                DelayUtility.Delay(2, () =>
                {
                    InitGame();
                    FadeUtility.FadeOut(blackPanel, 80);
                });
            });
            PFCLog.Info("开始游戏逻辑完成");
        }
        [ClientRpc]
        private void RpcGameOver(string theMessage)
        {
            if(isServer)Camera.main.cullingMask = LayerMask.GetMask("Default","Traffic","HumanAndDog");
            message.text = theMessage;
            gameOverPanel.SetActive(true);
            globalLight.intensity = 1;
        }

        // public void GameOver(string theMessage)
        // {
        //     if (isServer)
        //         RpcGameOver(theMessage);
        // }
        
        [Command(requiresAuthority = false)]
        public void CmdGameStart()
        {
            RpcGameStart();
        }

        // public void GameStart()
        // {
        //     if(isServer)
        //         RpcGameStart();
        // }
        
        [Command(requiresAuthority = false)]
        public void CmdGameOver(string theMessage)
        {
            RpcGameOver(theMessage);
        }
    }
}