using UnityEngine;

namespace LJH.Scripts.Map
{
    public class TerminalPoint : TriggerBase
    {
        protected override void HumanEnter(Collider2D thePlayer)
        {
            GameManager.Instance.CmdGameOver("成功!");
        }

        protected override void HumanExit(Collider2D thePlayer)
        {
            
        }

        protected override void DogEnter(Collider2D thePlayer)
        {
            
        }

        protected override void DogExit(Collider2D thePlayer)
        {
           
        }
    }
}