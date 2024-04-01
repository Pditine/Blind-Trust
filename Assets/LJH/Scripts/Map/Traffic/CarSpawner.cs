using System;
using System.Collections;
using Mirror;
using PurpleFlowerCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LJH.Scripts.Map
{
    public class CarSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject car;
        //[SerializeField] private Transform createPoint;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameStart",CmdCreateCar);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameStart",CmdCreateCar);
        }

        // private void Start()
        // {
        //     
        //     CmdCreateCar();
        // }
        
        
        [Command(requiresAuthority = false)]
        private void CmdCreateCar()
        {
            StartCoroutine(DoCreateCar());
        }
        
        private IEnumerator DoCreateCar()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));
                var theCar = Instantiate(car, transform.position, Quaternion.identity, transform);
                //var theCar = PoolSystem.GetGameObject(car);
                NetworkServer.Spawn(theCar);
                theCar.transform.parent = transform;
                theCar.transform.position = transform.position;
            }
        }
        
    }
}