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
        private void Update()
        {
            Move();
            ChangeSpeed();
            CheckFront();
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

            _speed = Mathf.Lerp(_speed, _targetSpeed, 0.01f);
        }

        private void CheckFront()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right,
                checkScope,LayerMask.GetMask("Traffic"));
            
            Debug.DrawRay(transform.position, direction*checkScope,Color.red);
            Debug.Log(hit);
            if (hit)
                _targetSpeed = 0;
            else
                _targetSpeed = maxSpeed;
        }
    }
}