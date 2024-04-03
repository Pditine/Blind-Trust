using System;
using LJH.Scripts;
using Mirror;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerDog : Player
    {
        [SerializeField] private float speed;
        [SerializeField] public Animator TheAnimator;
        
        [SerializeField] private GameObject arrow;
        [SerializeField] private AudioSource bark;
        protected override void Update()
        {
            base.Update();
            arrow.transform.right = GameManager.Instance.TerminalPoint.position-transform.position;
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