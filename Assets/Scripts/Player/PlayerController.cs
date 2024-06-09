using TouhouPride.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Player
{
    public class PlayerController : ACharacter
    {
        public static PlayerController Instance { private set; get; }

        private Rigidbody2D _rb;
        private Vector2 _mov;

        private Vector2 _lastDir = Vector2.up;

        private void Awake()
        {
            Instance = this;

            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * Info.Speed;
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
            if (_mov.magnitude != 0f)
            {
                _lastDir = _mov;
            }
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                Shoot(_lastDir, true);
            }
        }
    }
}
