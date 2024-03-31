using System.Collections;
using PurpleFlowerCore;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject car;
        //[SerializeField] private Transform createPoint;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;
        private void Start()
        {
            StartCoroutine(DoCreateCar());
        }

        private IEnumerator DoCreateCar()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));
                //Instantiate(car, transform.position, Quaternion.identity, transform);
                var theCar = PoolSystem.GetGameObject(car);
                theCar.transform.parent = transform;
                theCar.transform.position = transform.position;
            }
        }
        
    }
}