using Mirror;
using PurpleFlowerCore;
using UnityEngine;

namespace LJH.Scripts
{
    public class PedestrianCreator : NetworkBehaviour
    {
        [SerializeField] private GameObject thePedestrian;

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameStart",CmdCreatePedestrian);
        }
        
        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameStart",CmdCreatePedestrian);
        }

        // public override void OnStartClient()
        // {
        //     base.OnStartClient();
        //     CmdCreateCar();
        // }

        // [ClientRpc]
        // private void RpcCreatePedestrian()
        // {
        //     
        // }
        
        [Command(requiresAuthority = false)]
        private void CmdCreatePedestrian()
        {
            //RpcCreatePedestrian();
            var pedestrian = Instantiate(thePedestrian, transform.position, Quaternion.identity);
            //var theCar = PoolSystem.GetGameObject(car);
            NetworkServer.Spawn(pedestrian);
        }
        
    }
}