using Mirror;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerHuman : Player
    {
        [SerializeField] private float speed;
        [SerializeField] public Animator TheAnimator;
        
        [SyncVar] private float _arrowAlpha;
        [SyncVar] private Vector3 _targetPosition;
        [SerializeField] private GameObject arrow;
        [SerializeField] private SpriteRenderer arrowSpriteRenderer;
        [SerializeField] private float arrowFadeOutSpeed;
        
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
            TheAnimator.SetBool("Walking",true);
            transform.position += (Vector3)movement * (speed * Time.deltaTime);
            if(movement.x!=0)
            {
                int num = movement.x > 0 ? -1 : 1;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*num, transform.localScale.y, 1);
                RpcFlipArrow(num);
            }
        }

        [ClientRpc]
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
            Debug.Log("Human Acting");
        }
    }
}