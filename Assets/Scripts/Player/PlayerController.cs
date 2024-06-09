using System.Collections;
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

        private bool _canShoot = true;

        private void Awake()
        {
            Instance = this;
            Init();

            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * Info.Speed;
        }

        public IEnumerator ReloadCoroutine()
        {
            _canShoot = false;
            yield return new WaitForSeconds(Info.ReloadTime);
            _canShoot = true;
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
            if (value.performed && _canShoot)
            {
                Shoot(_lastDir, true);
                StartCoroutine(ReloadCoroutine());
            }
        }
    }
}
