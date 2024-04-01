using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using PurpleFlowerCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LJH.Scripts.Map
{
    public class CarSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject car;
        [SerializeField] private Transform createPoint;
        // [SerializeField] private float minTime;
        // [SerializeField] private float maxTime;
        [SerializeField]private List<float> randomNumList = new();
        private int _randomNumIndex = -1;

        public float GetRandomNum()
        {
            _randomNumIndex++;
            if (_randomNumIndex >= randomNumList.Count)
                _randomNumIndex = 0;
            return randomNumList[_randomNumIndex];
        }
        private void OnEnable()
        {
            EventSystem.AddEventListener("GameStart",CmdCreateCar);
        }
        
        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameStart",CmdCreateCar);
        }

        // public override void OnStartClient()
        // {
        //     base.OnStartClient();
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
                yield return new WaitForSeconds(GetRandomNum());
                var theCar = Instantiate(car, createPoint.position, Quaternion.identity);
                //var theCar = PoolSystem.GetGameObject(car);
                NetworkServer.Spawn(theCar);
                // theCar.transform.parent = transform;
                // theCar.transform.position = transform.position;
            }
        }
        
        
    }
}