using System;
using Hmxs.Scripts.Player;
using Mirror;
using PurpleFlowerCore;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class PianoLake : TriggerBase
    {
        protected override void HumanEnter(Collider2D thePlayer)
        {
            GameManager.Instance.CmdGameOver("盲人落入湖中");
            thePlayer.GetComponent<PlayerHuman>().CanMove = false;
            thePlayer.GetComponent<PlayerHuman>().TheAnimator.SetTrigger("FallWater");
        }

        protected override void HumanExit(Collider2D thePlayer)
        {
            
        }

        protected override void DogEnter(Collider2D thePlayer)
        {
            GameManager.Instance.CmdGameOver("导盲犬落入湖中");
            thePlayer.GetComponent<PlayerDog>().CanMove = false;
            thePlayer.GetComponent<PlayerDog>().TheAnimator.SetTrigger("FallWater");
        }

        protected override void DogExit(Collider2D thePlayer)
        {
            
        }
    }
}