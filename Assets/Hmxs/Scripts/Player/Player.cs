using Hmxs.Scripts.Input;
using Mirror;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public abstract class Player : NetworkBehaviour
    {
        private void Update()
        {
            if (!isLocalPlayer) return;

            if (InputManager.Instance.MoveValue.magnitude > 0)
            {
                Debug.Log("Moving");
                OnMove(InputManager.Instance.MoveValue);
            }

            if (InputManager.Instance.ActValue)
                OnAct();
        }

        protected abstract void OnMove(Vector2 movement);
        protected abstract void OnAct();
    }
}