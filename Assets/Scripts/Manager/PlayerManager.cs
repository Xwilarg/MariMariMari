using System.Collections.Generic;
using System.Linq;
using TouhouPride.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { private set; get; }

        private List<PlayerController> _controllers = new();
        private PlayerController[] Controllers => _controllers.Where(x => x.enabled).ToArray();

        public PlayerController GetPriorityTarget(Vector2 _)
            => Controllers.First(x => x.enabled);
            //=> Controllers.OrderBy(x => Vector2.Distance(x.transform.position, pos)).First();

        public FollowerController GetFollower()
            => Controllers.Select(x => x.GetComponent<FollowerController>()).First(x => x != null);

        private void Awake()
        {
            Instance = this;
        }

        public void Register(PlayerController controller)
        {
            _controllers.Add(controller);
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
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnShoot(value);
        }

        public void OnBomb(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnBomb(value);
        }
    }
}