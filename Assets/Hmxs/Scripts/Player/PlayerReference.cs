using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerReference : MonoBehaviour
    {
        public GameObject humanPrefab;
        public GameObject dogPrefab;

        public static PlayerReference Instance { get; private set; }

        private void Awake() => Instance = this;
    }
}