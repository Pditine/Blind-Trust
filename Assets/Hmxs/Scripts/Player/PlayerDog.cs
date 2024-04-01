using Hmxs.Scripts.Network;
using LJH.Scripts;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerDog : Player
    {
        [SerializeField] private float speed;
        //[SerializeField] private GameObject bark;

        protected override void OnMove(Vector2 movement)
        {
            transform.position += (Vector3)movement * (speed * Time.deltaTime);
        }

        protected override void OnAct()
        {
            // var theBark = Instantiate(bark, transform.position,Quaternion.identity);
            // NetworkServer.Spawn(theBark);
            // PFCLog.Info(BlindTrustNetworkManager.singleton);
            // PFCLog.Info(BlindTrustNetworkManager.singleton.Human);
            GameManager.Instance.Human.CmdChangeTarget(transform.position);
            Debug.Log("Dog Acting");
        }
    }
}