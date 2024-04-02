using System.Collections.Generic;
using Hmxs.Scripts.Player;
using Mirror;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class CarController : TriggerBase
    {
        private float _speed;
        private float _targetSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float checkScope;
        [SerializeField] private Vector3 direction;
        [SerializeField] private Transform rayPoint;
        [SerializeField] private List<Sprite> sprites = new();

        [ClientRpc]
        private void Init(int randomNum)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[randomNum];
        }
        private void Start()
        {
            if (!isServer) return;
            // DelayUtility.Delay(60, () =>
            // {
            //     //PoolSystem.PushGameObject(gameObject);
            //     //Destroy(gameObject);
            //     NetworkServer.Destroy(gameObject);
            // });
            Init(Random.Range(0,sprites.Count));
        }

        private void Update()
        {
            if (!isServer) return;
            Move();
            CheckFront();
        }

        private void FixedUpdate()
        {
            if (!isServer) return;
            ChangeSpeed();
        }
        
        private void Move()
        {
            transform.position += direction * (_speed*Time.deltaTime);
        }
        private void ChangeSpeed()
        {
            _speed = Mathf.Lerp(_speed, _targetSpeed, 0.03f);
        }
        
        private void CheckFront()
        {
            RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, direction*checkScope,
                checkScope,LayerMask.GetMask("Traffic"));
            
            Debug.DrawRay(rayPoint.position, direction*checkScope,Color.red);
            if (hit)
                _targetSpeed = 0;
            else
                _targetSpeed = maxSpeed;
            
            RaycastHit2D hit2 = Physics2D.Raycast(rayPoint.position, direction*checkScope,
                checkScope,LayerMask.GetMask("CarDead"));
            if(hit2)
                NetworkServer.Destroy(gameObject);
        }

        protected override void HumanEnter(Collider2D thePlayer)
        {
            GameManager.Instance.CmdGameOver("盲人撞到车辆");
            thePlayer.GetComponent<PlayerHuman>().CanMove = false;
            thePlayer.GetComponent<PlayerHuman>().TheAnimator.SetTrigger("BeHit");
        }

        protected override void HumanExit(Collider2D thePlayer)
        {
            
        }

        protected override void DogEnter(Collider2D thePlayer)
        {
            GameManager.Instance.CmdGameOver("导盲犬撞到车辆");
            thePlayer.GetComponent<PlayerDog>().CanMove = false;
            thePlayer.GetComponent<PlayerDog>().TheAnimator.SetTrigger("BeHit");
        }

        protected override void DogExit(Collider2D thePlayer)
        {
            
        }
    }
}