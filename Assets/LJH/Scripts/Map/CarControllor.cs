using UnityEngine;

namespace LJH.Scripts.Map
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        private float _speed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float slowDownScope;
        private void Update()
        {
            Move();
            ChangeSpeed();
        }

        private void Move()
        {
            Vector3 direction = endPoint.position-transform.position;
            transform.position += direction * (_speed*Time.deltaTime);
        }
        
        private void ChangeSpeed()
        {
            float distance = (transform.position - startPoint.position).magnitude;
            float proportion = distance / (endPoint.position-startPoint.position).magnitude;
            if (proportion < slowDownScope)
            {
                _speed = maxSpeed * proportion / slowDownScope;
            }
            else if (proportion < 1-slowDownScope)
            {
                _speed = maxSpeed;
            }
            else if(proportion<1)
            {
                _speed = maxSpeed * (1-proportion) / slowDownScope;
            }
        }
    }
}