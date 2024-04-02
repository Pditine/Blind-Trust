using Mirror;
using PurpleFlowerCore;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class Vase : TriggerBase
    {
        [SerializeField] private Sprite brokenSprite;
        private Sprite _oriSprite;
        private bool _hasBroken;
        private SpriteRenderer TheSpriteRenderer;

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameReStart",ReSet);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameReStart",ReSet);
        }

        private void Start()
        {
            TheSpriteRenderer = GetComponent<SpriteRenderer>();
            _oriSprite = TheSpriteRenderer.sprite;
        }

        private void ReSet()
        {
            TheSpriteRenderer.sprite = _oriSprite;
            _hasBroken = false;
        }
        
        [Command(requiresAuthority = false)]
        private void CmdBroke()
        {
            RpcBroke();
        }

        [ClientRpc]
        private void RpcBroke()
        {
            if (_hasBroken) return;
            TheSpriteRenderer.sprite = brokenSprite;
        }
        protected override void HumanEnter(Collider2D thePlayer)
        {
            CmdBroke();
        }

        protected override void HumanExit(Collider2D thePlayer)
        {
            
        }

        protected override void DogEnter(Collider2D thePlayer)
        {
            CmdBroke();
        }

        protected override void DogExit(Collider2D thePlayer)
        {
            
        }
    }
}