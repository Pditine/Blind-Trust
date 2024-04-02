using System;
using LJH.Scripts;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerDog : Player
    {
        [SerializeField] private float speed;
        [SerializeField] public Animator TheAnimator;
        private void OnEnable()
        {
            EventSystem.AddEventListener("GameStart",ReSet);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameStart",ReSet);
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
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*movement.x > 0 ? -1 : 1, transform.localScale.y, 1);
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
            Debug.Log("Dog Acting");
        }
    }
}