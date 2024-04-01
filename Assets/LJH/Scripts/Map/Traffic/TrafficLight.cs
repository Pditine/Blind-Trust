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
        [SerializeField] private List<Sprite> sprites = new();

        private void Start()
        {
            var theProcess = gameObject.AddComponent<Process>();
            theProcess.Init(true, new ActionNode(BeGreen), new WaitNode(deltaTime/2),
                new ActionNode(BeYellow), new WaitNode(deltaTime/2),
                new ActionNode(BeRed), new WaitNode(deltaTime));
            DelayUtility.Delay(delayTime, () =>
            {
                theProcess.Start_();
            });
        }

        public void BeGreen()
        {
            theBulb.sprite = sprites[0];
            foreach (var airWall in airWalls)
            {
                airWall.SetActive(false);
            }
        }
        public void BeYellow()
        {
            //_state = TrafficLightColor.Yellow;
            theBulb.sprite = sprites[1];
            foreach (var airWall in airWalls)
            {
                airWall.SetActive(true);
            }
        }
        public void BeRed()
        {
            //_state = TrafficLightColor.Red;
            theBulb.sprite = sprites[2];
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