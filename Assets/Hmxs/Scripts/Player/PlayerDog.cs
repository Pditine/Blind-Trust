using System;
using System.Collections;
using System.Threading;
using LJH.Scripts;
using Mirror;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.Player
{
    public class PlayerDog : Player
    {
        [SerializeField] private float speed;
        [SerializeField] public Animator TheAnimator;
        
        [SerializeField] private GameObject arrow;
        [SerializeField] private AudioSource bark;
        private bool _canBark;
        private float _cdTimer;
        [SerializeField] private float CdTime;
        [SerializeField] private Image cd;

        public override void OnStartClient()
        {
            base.OnStartClient();
            cd = GameManager.Instance.dogCd;
            if (!isLocalPlayer)
            {
                cd.gameObject.SetActive(false);
            }
        }
        
        protected override void Update()
        {
            base.Update();
            arrow.transform.right = GameManager.Instance.TerminalPoint.position-transform.position;
            if (!_canBark)
            {
                _cdTimer += Time.deltaTime;
                cd.fillAmount = _cdTimer / CdTime;
                if (_cdTimer >= CdTime)
                {
                    _cdTimer = 0;
                    _canBark = true;
                    cd.fillAmount = 0;
                }
            }
        }

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameReStart",ReSet);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameReStart",ReSet);
        }

        private void ReSet()
        {
            TheAnimator.SetTrigger("GameStart");
            CanMove = true;
        }

        protected override void OnMove(Vector2 movement)
        {
            
            TheAnimator.SetBool("Walking",true);
            transform.position += (Vector3)movement * (speed * Time.deltaTime);
            if(movement.x!=0)
            {
                int num = movement.x > 0 ? -1 : 1;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * num,
                    transform.localScale.y, 1);
                RpcFlipArrow(num);
            }
            
        }
        
        private void RpcFlipArrow(int num)
        {
            arrow.transform.localScale = new Vector3(Mathf.Abs(arrow.transform.localScale.x)*num,arrow.transform.localScale.y, 1);
        }
        
        protected override void OnIdle()
        {
            TheAnimator.SetBool("Walking",false);
        }

        protected override void OnAct()
        {
            // var theBark = Instantiate(bark, transform.position,Quaternion.identity);
            // NetworkServer.Spawn(theBark);
            // PFCLog.Info(BlindTrustNetworkManager.singleton);
            // PFCLog.Info(BlindTrustNetworkManager.singleton.Human);
            if(!_canBark) return;
            _canBark = false;
            _cdTimer = 0;
            GameManager.Instance.Human.CmdChangeTarget(transform.position);
            CmdBark();
            Debug.Log("Dog Acting");
        }
        
        [Command(requiresAuthority = false)]
        private void CmdBark()
        {
            RpcBark();
        }

        [ClientRpc]
        private void RpcBark()
        {
            bark.Play();
        }
    }
}