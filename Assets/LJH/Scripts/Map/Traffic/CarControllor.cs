using System.Collections.Generic;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class CarController : MonoBehaviour
    {
        // [SerializeField] private Transform startPoint;
        // [SerializeField] private Transform endPoint;
        private float _speed;
        private float _targetSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float checkScope;
        [SerializeField] private Vector3 direction;
        [SerializeField] private Transform rayPoint;
        [SerializeField] private List<Sprite> sprites = new();

        private void Start()
        {
            DelayUtility.Delay(60, () =>
            {
                PoolSystem.PushGameObject(gameObject);
                //Destroy(gameObject);
            });
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        }

        private void Update()
        {
            Move();
            CheckFront();
        }

        private void FixedUpdate()
        {
            ChangeSpeed();
        }

        private void Move()
        {
            // Vector3 direction = (endPoint.position - transform.position).normalized;
            // transform.position += direction * (_speed*Time.deltaTime);
            transform.position += direction * (_speed*Time.deltaTime);
        }
        
        private void ChangeSpeed()
        {
            // float distance = (transform.position - startPoint.position).magnitude;
            // float proportion = distance / (endPoint.position-startPoint.position).magnitude;
            // if (proportion < slowDownScope)
            // {
            //     _speed = maxSpeed * proportion / slowDownScope;
            // }
            // else if (proportion < 1-slowDownScope)
            // {
            //     _speed = maxSpeed;
            // }
            // else if(proportion<1)
            // {
            //     _speed = maxSpeed * (1-proportion) / slowDownScope;
            // }

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
        }
    }
}