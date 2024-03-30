using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerDog : Player
    {
        [SerializeField] private float speed;

        protected override void OnMove(Vector2 movement)
        {
            transform.position += (Vector3)movement * (speed * Time.deltaTime);
        }

        protected override void OnAct()
        {
            Debug.Log("Dog Acting");
        }
    }
}