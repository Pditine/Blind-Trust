using Mirror;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class Vase : TriggerBase
    {
        [SerializeField] private Sprite brokenSprite;
        

        [Command]
        private void CmdBroke()
        {
            GetComponent<SpriteRenderer>().sprite = brokenSprite;
        }
        protected override void PlayerEnter(Collider2D thePlayer)
        {
            CmdBroke();
        }

        protected override void PlayerExit(Collider2D thePlayer)
        {
            
        }
    }
}