using Mirror;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public abstract class  TriggerBase : NetworkBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Human"))
            {
                HumanEnter(other);
            }

            if (other.CompareTag("Dog"))
            {
                DogEnter(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Human"))
            {
                HumanExit(other);
            }

            if (other.CompareTag("Dog"))
            {
                DogExit(other);
            }
        }

        protected abstract void HumanEnter(Collider2D thePlayer);
        protected abstract void HumanExit(Collider2D thePlayer);

        protected abstract void DogEnter(Collider2D thePlayer);
        protected abstract void DogExit(Collider2D thePlayer);
    }
}