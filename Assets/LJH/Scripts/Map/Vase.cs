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
        //[SerializeField]private AudioSource _audioSource;
        [SerializeField] private AudioClip audio;
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
            //_audioSource.GetComponent<AudioSource>();
        }

        private void ReSet()
        {
            TheSpriteRenderer.sprite = _oriSprite;
            _hasBroken = false;
        }
        
        [Command(requiresAuthority = false)]
        private void CmdBroke()
        {
            if (_hasBroken) return;
            _hasBroken = true;
            RpcBroke();
        }

        [ClientRpc]
        private void RpcBroke()
        {
            TheSpriteRenderer.sprite = brokenSprite;
            AudioSource.PlayClipAtPoint(audio,transform.position);
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