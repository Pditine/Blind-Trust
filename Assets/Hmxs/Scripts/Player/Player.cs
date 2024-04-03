using Hmxs.Scripts.Input;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering;

namespace Hmxs.Scripts.Player
{
    public abstract class Player : NetworkBehaviour
    {
        public bool CanMove = true;
        [SerializeField] private VolumeProfile volumeProfile;

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (isLocalPlayer) FindObjectOfType<Volume>().profile = volumeProfile;
        }

        protected virtual void Update()
        {
            if (!isLocalPlayer) return;
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