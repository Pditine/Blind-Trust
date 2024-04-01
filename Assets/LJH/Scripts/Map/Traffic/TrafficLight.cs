using System;
using System.Collections.Generic;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace LJH.Scripts.Map
{
    // enum TrafficLightColor
    // {
    //     Green,Yellow,Red
    // }
    public class TrafficLight : MonoBehaviour
    {
        //private TrafficLightColor _state;
        [SerializeField] private SpriteRenderer theBulb;

        [SerializeField] private List<GameObject> airWalls = new();
        [SerializeField] private float delayTime;
        [SerializeField] private float deltaTime;

        private void Start()
        {
            var theProcess = gameObject.AddComponent<Process>();
            theProcess.Init(true, new ActionNode(BeGreen), new WaitNode(deltaTime),
                new ActionNode(BeRed), new WaitNode(deltaTime));
            DelayUtility.Delay(delayTime, () =>
            {
                theProcess.Start_();
            });
        }

        public void BeGreen()
        {
            theBulb.color = Color.green;
            foreach (var airWall in airWalls)
            {
                airWall.SetActive(false);
            }
        }
        // public void BeYellow()
        // {
        //     //_state = TrafficLightColor.Yellow;
        //     theBulb.color = Color.yellow;
        //     foreach (var airWall in airWalls)
        //     {
        //         airWall.SetActive(true);
        //     }
        // }
        public void BeRed()
        {
            //_state = TrafficLightColor.Red;
            theBulb.color = Color.red;
            foreach (var airWall in airWalls)
            {
                airWall.SetActive(true);
            }
        }
        
        // private void Update()
        // {
        //     switch (_state)
        //     {
        //         case TrafficLightColor.Green:
        //             GreenUpdate();
        //             break;
        //         case TrafficLightColor.Yellow:
        //             YellowUpdate();
        //             break;
        //         case TrafficLightColor.Red:
        //             RedUpdate();
        //             break;
        //     }
        // }
        //
        // private void GreenUpdate()
        // {
        //     
        // }
        // private void YellowUpdate()
        // {
        //     
        // }
        // private void RedUpdate()
        // {
        //     
        // }

    }
}