using Hmxs.Scripts.Input;
using Mirror;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public abstract class Player : NetworkBehaviour
    {
        public bool CanMove = true;
        [SerializeField] private GameObject globalVolume;
        protected virtual void Update()
        {
            if (!isLocalPlayer)
            {
                globalVolume.SetActive(false);
                return;
            }
            if (!CanMove) return;
            if (InputManager.Instance.MoveValue.magnitude > 0)
            {
                //Debug.Log("Moving: " + InputManager.Instance.MoveValue);
                OnMove(InputManager.Instance.MoveValue);
            }else
                OnIdle();

            if (InputManager.Instance.ActValue)
                OnAct();
        }

        protected abstract void OnMove(Vector2 movement);
        protected abstract void OnIdle();
        protected abstract void OnAct();
    }
}