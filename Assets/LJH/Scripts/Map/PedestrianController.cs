using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class PedestrianController : NetworkBehaviour
    {
        [SerializeField]private List<Transform> points = new();
        [SerializeField] private float waitTime;
        [SerializeField] private float speed;
        [SerializeField] private int id;
        private Animator TheAnimator;
        private bool _isWaiting;
        private int _pointIndex;
        private Vector3 _oriPosition;

        private void Start()
        {
            TheAnimator = GetComponent<Animator>();
            _oriPosition = transform.position;
        }

        private void OnEnable()
        {
            if (!isServer) return;
            EventSystem.AddEventListener("GameReStart",ReSet);
            EventSystem.AddEventListener("GameStart",ReSet);
        }

        private void OnDisable()
        {
            if (!isServer) return;
            EventSystem.RemoveEventListener("GameReStart",ReSet);
            EventSystem.RemoveEventListener("GameStart",ReSet);
        }
        
        private void Update()
        {
            if (!isServer) return;
            CmdMove();
        }
        
        [Command(requiresAuthority = false)]
        private void CmdMove()
        {
            if(_isWaiting)return;
            Vector3 vector = points[_pointIndex].position - transform.position;
            Vector3 direction = vector.normalized;
            float offset = speed*Time.deltaTime; 
            Vector3 position = transform.position + direction * offset;
            
            if (vector.sqrMagnitude < offset * offset)
                NextPoint();
            
            RpcMove(position,direction.x>0);
        }

        private void ReSet()
        {
            RpcReSet();
        }
        
        [ClientRpc]
        private void RpcReSet()
        {
            transform.position = _oriPosition;
        }
        [ClientRpc]
        private void RpcMove(Vector3 position,bool flip)
        {
            transform.position = position;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (flip ? -1 : 1), transform.localScale.y, 1);
        }
        private void NextPoint()
        {
            RpcBeWaiting(true);
            DelayUtility.Delay(waitTime, () =>
            {
                _pointIndex++;
                if (_pointIndex >= points.Count) _pointIndex = 0;
                RpcBeWaiting(false);
            },false);
        }
        [ClientRpc]
        private void RpcBeWaiting(bool isWait)
        {
            _isWaiting = isWait;
            TheAnimator.SetBool("Walking", !isWait);
        }
        
    }
}