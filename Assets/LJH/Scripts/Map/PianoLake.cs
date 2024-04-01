using System;
using Mirror;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class PianoLake : TriggerBase
    {
        protected override void PlayerEnter(Collider2D thePlayer)
        {
            GameManager.Instance.CmdGameOver("死");
        }

        protected override void PlayerExit(Collider2D thePlayer)
        {
            
        }
    }
}