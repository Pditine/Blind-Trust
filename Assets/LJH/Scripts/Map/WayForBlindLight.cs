using System;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class WayForBlindLight : MonoBehaviour
    {
        private SpriteRenderer TheSpriteRenderer;
        private float _target;
        private void Start()
        {
            TheSpriteRenderer = GetComponent<SpriteRenderer>();
            TheSpriteRenderer.color = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            Shine();   
        }

        private void Shine()
        {
            TheSpriteRenderer.color = Color.Lerp(TheSpriteRenderer.color,new Color(1,1,1,_target),0.01f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("TheBlind")) return;
            _target = 1;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("TheBlind")) return;
            _target = 0;
        }
    }
}
