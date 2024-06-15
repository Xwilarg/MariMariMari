using System.Collections;
using TouhouPride.Manager;
using TouhouPride.Utils;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Player
{
    public class PlayerController : ACharacter
    {
        public static PlayerController Instance { private set; get; }

        [SerializeField] CinemachineCamera _cam;
        private Camera _mainCam;

        private Rigidbody2D _rb;
        private Vector2 _mov;
        private Follower _follower;

        private Vector2 _lastDir = Vector2.up;

        private bool _canShoot = true;

        private bool _isDashing;
        private bool _canDash = true;

        private bool _isCurrentlyFiring;

        protected override void Awake()
        {
            Instance = this;
            base.Awake();

            _rb = GetComponent<Rigidbody2D>();
            _follower = GetComponent<Follower>();

            _mainCam = Camera.main;
        }

        protected override void Start()
        {
            base.Start();
            // TODO: fuckery with child class
            PlayerManager.Instance.Register(this);
            GetComponent<Follower>().SetInfo(false);
        }

        private void FixedUpdate()
        {
            if (_isDashing)
            {
                _rb.velocity = _lastDir * 3f * Info.Speed;
            }
            else
            {
                _rb.velocity = _mov * Info.Speed;
            }
        }

        private void OnDisable()
        {
            _rb.velocity = Vector2.zero;
            _mov = Vector2.zero;
            _isCurrentlyFiring = false;
            _isDashing = false;
        }

        public void TargetMe()
        {
            _cam.Target.TrackingTarget = transform;
        }

        public IEnumerator RapidFireCoroutine()
        {
            while (_isDashing) yield return new WaitForEndOfFrame(); // Can't shoot while dashing
            if (_isCurrentlyFiring && _canShoot)
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
        }

        private IEnumerator DashExecute()
        {
            _isDashing = true;
            _canDash = false;

            yield return new WaitForSeconds(.5f);

            _isDashing = false;

            yield return new WaitForSeconds(1f);

            _canDash = true;
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
            if (_mov.magnitude != 0f && !_isDashing)
            {
                _lastDir = _mov;
            }
        }

        public void OnSwitchCharacter(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                _follower.Switch();
            }
        }

        public void OnDash(InputAction.CallbackContext value)
        {
            if (value.started && _canDash)
            {
                StartCoroutine(DashExecute());
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

        public void OnBomb(InputAction.CallbackContext value)
        {
            // pass in partner once we keep track of that.
            if (value.started && LoveMeter.Instance.CanBomb(PlayerManager.Instance.Follower.Info.Name))
            {
                Debug.Log("bombing");
                LoveMeter.Instance.UsePower(PlayerManager.Instance.Follower.Info.Name);
                
                var bounds = _mainCam.CalculateBounds();

                for (int i = EnemyManager.Instance.Enemies.Count - 1; i >= 0; i--)
                {
                    var e = EnemyManager.Instance.Enemies[i];
                    if (bounds.Contains((Vector2)e.transform.position))
                    {
                        e.TakeDamage(9999);
                    }
                }
            }
        }
    }
}
