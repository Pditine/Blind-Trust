using System;
using Mirror;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public abstract class  TriggerBase : NetworkBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Human")||other.CompareTag("Dog"))
            {
                PlayerEnter(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Human")||other.CompareTag("Dog"))
            {
                PlayerExit(other);
            }
        }

        protected abstract void PlayerEnter(Collider2D thePlayer);

        protected abstract void PlayerExit(Collider2D thePlayer);
    }
}