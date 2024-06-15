using System.Collections.Generic;
using System.Linq;
using TouhouPride.Enemy.Impl;
using TouhouPride.Player;
using TouhouPride.VN;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { private set; get; }

        private List<PlayerController> _controllers = new();
        private FollowerController _fc;
        private PlayerController[] Controllers => _controllers.Where(x => x.enabled).ToArray();

        public PlayerController GetPriorityTarget(Vector2 _)
            => Controllers.First(x => x.enabled);
        //=> Controllers.OrderBy(x => Vector2.Distance(x.transform.position, pos)).First();

        public FollowerController Follower
            => _fc;

        public PlayerController Player { set; get; }

        public BossEnemy Boss { set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public void Register(PlayerController controller)
        {
            _controllers.Add(controller);
            if (controller.TryGetComponent<FollowerController>(out var fc))
            {
                _fc = fc;
            }
            else
            {
                Player = controller;
            }
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnMove(value);
        }

        public void OnSwitchCharacter(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnSwitchCharacter(value);
        }

        public void OnDash(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnDash(value);
            VNManager.Instance.OnSkip(value);
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnShoot(value);
            VNManager.Instance.OnNextDialogue(value);
        }

        public void OnBomb(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnBomb(value);
        }
    }
}