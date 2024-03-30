using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public PlayerInput PlayerInput => _playerInput;
        public InputAction MoveAction => _moveAction;
        public InputAction ActAction => _actAction;
        public Vector2 MoveValue => _moveAction.ReadValue<Vector2>();
        public bool ActValue => _actAction.triggered;

        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _actAction;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Movement"];
            _actAction = _playerInput.actions["Act"];
        }
    }
}