using System;
using System.Collections.Generic;
using PurpleFlowerCore;
using UnityEngine;

namespace LJH.Scripts.Map
{
    enum TrafficLightColor
    {
        Green,Yellow,Red
    }
    public class TrafficLight : MonoBehaviour
    {
        //private TrafficLightColor _state;
        [SerializeField] private SpriteRenderer theBulb;

        [SerializeField] private List<GameObject> airWalls = new();

        private void Start()
        {
            var theProcess = ProcessSystem.CreateProcess("Light")
        }

        public void BeGreen()
        {
            //_state = TrafficLightColor.Green;
            theBulb.color = Color.green;
            foreach (var airWall in airWalls)
            {
                airWall.SetActive(false);
            }
        }
        public void BeYellow()
        {
            //_state = TrafficLightColor.Yellow;
            theBulb.color = Color.yellow;
        }
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