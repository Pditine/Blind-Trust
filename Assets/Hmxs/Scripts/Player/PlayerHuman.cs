using System;
using Mirror;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hmxs.Scripts.Player
{
    public class PlayerHuman : Player
    {
        [SerializeField] private float speed;
        [SyncVar] private Vector3 _targetPosition;
        [SerializeField] private Animator theAnimator;
        [SyncVar] private float _arrowAlpha;
        [SerializeField] private GameObject arrow;
        [SerializeField] private SpriteRenderer arrowSpriteRenderer;
        [SerializeField] private float arrowFadeOutSpeed;
        protected override void Update()
        {
            base.Update();
            UpdateArrow();
        }

        private void UpdateArrow()
        {
            if(isServer)
            {
                _arrowAlpha -= arrowFadeOutSpeed * Time.deltaTime;
                if (_arrowAlpha < 0) _arrowAlpha = 0;
            }
            arrow.transform.right = (_targetPosition-transform.position).normalized;
            arrowSpriteRenderer.color = new Color(1, 1, 1, _arrowAlpha);
        }
        
        [Command(requiresAuthority = false)]
        public void CmdChangeTarget(Vector3 targetPosition)
        {
            _arrowAlpha = 1;
            _targetPosition = targetPosition;
        }
        
        // [ClientRpc]
        // private void RpcChangeTarget(Vector3 targetPosition)
        // {
        //     _targetPosition = targetPosition;
        //     
        // }

        protected override void OnMove(Vector2 movement)
        {
            theAnimator.SetBool("Walking",true);
            transform.position += (Vector3)movement * (speed * Time.deltaTime);
        }

        protected override void OnIdle()
        {
            theAnimator.SetBool("Walking",false);
        }

        protected override void OnAct()
        {
            Debug.Log("Human Acting");
        }
    }
}