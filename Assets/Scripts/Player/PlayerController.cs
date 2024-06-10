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

        private bool _isStrafing = false;

        private bool _isCurrentlyFiring = false;

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

        public IEnumerator RapidFireCoroutine()
        {
            Shoot(_lastDir, true);
            _canShoot = false;
            yield return new WaitForSeconds(Info.ReloadTime);
            _canShoot = true;

            if (_isCurrentlyFiring)
            {
                yield return RapidFireCoroutine();
            }
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
            if (_mov.magnitude != 0f)
            {
                if (!_isStrafing)
                {
                    _lastDir = _mov;
                }
            }
        }

        public void OnStrafe(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                print("is strafing");
                _isStrafing = true;
            }

            if (value.canceled)
            {
                print("no longer strafing");
                _isStrafing = false;
            }
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            // if button pressed, start fire
            if (value.started)
            {
                _isCurrentlyFiring = true;
                if (_canShoot)
                {
                    StartCoroutine(RapidFireCoroutine());
                }
            }

            // if button release, cease fire.
            else if (value.canceled)
            {
                _isCurrentlyFiring = false;
            }
        }
    }
}
