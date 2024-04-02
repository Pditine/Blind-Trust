using System.Collections;
using System.Collections.Generic;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class PedestrianController : MonoBehaviour
    {
        [SerializeField]private List<Transform> points = new();
        [SerializeField] private float waitTime;
        [SerializeField] private float speed;
        [SerializeField] private int id;
        private bool _isWaiting;
        private int _pointIndex;
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if(_isWaiting)return;
            Vector3 vector = points[_pointIndex].position - transform.position;
            Vector3 direction = vector.normalized;
            float offset = speed*Time.deltaTime;
            transform.position += direction * offset;
            if (vector.sqrMagnitude < offset * offset)
                NextPoint();
        }

        private void NextPoint()
        {
            BeWaiting(true);
            DelayUtility.Delay(waitTime, () =>
            {
                _pointIndex++;
                if (_pointIndex >= points.Count) _pointIndex = 0;
                BeWaiting(false);
            },false);
        }

        private void BeWaiting(bool isWait)
        {
            _isWaiting = isWait;
        }
        
    }
}