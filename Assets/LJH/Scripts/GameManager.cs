using Cinemachine;
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
        }
        
        [ClientRpc]
        public void SetPlayer(GameObject thePlayer)
        {
            if (_thePlayer is not null) return;
            _thePlayer = thePlayer;
        }
        
        [ClientRpc]
        public void RpcGameStart()
        {
            PFCLog.Info("游戏开始");
            gameOverPanel.SetActive(false);
            FadeUtility.FadeInAndStay(blackPanel,80, () =>
            {
                DelayUtility.Delay(2, () =>
                {
                    InitGame();
                    FadeUtility.FadeOut(blackPanel, 80);
                });
            });
        }
        [ClientRpc]
        public void RpcGameOver(string theMessage)
        {
            if(isServer)Camera.main.cullingMask = LayerMask.GetMask("Default","Traffic","HumanAndDog");
            message.text = theMessage;
            gameOverPanel.SetActive(true);
            globalLight.intensity = 1;
        }
        
    }
}