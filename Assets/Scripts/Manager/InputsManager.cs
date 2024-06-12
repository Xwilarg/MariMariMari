using System.Collections.Generic;
using System.Linq;
using TouhouPride.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Manager
{
    public class InputsManager : MonoBehaviour
    {
        public static InputsManager Instance { private set; get; }

        private List<PlayerController> _controllers = new();
        private PlayerController[] Controllers => _controllers.Where(x => x.enabled).ToArray();

        public PlayerController GetPriorityTarget(Vector2 _)
            => Controllers.First(x => x.enabled);
            //=> Controllers.OrderBy(x => Vector2.Distance(x.transform.position, pos)).First();

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

        public void OnStrafe(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnStrafe(value);
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            foreach (var c in Controllers) c.OnShoot(value);
        }

        public void OnLoveMeterActionUse(InputAction.CallbackContext value)
        {
            print("doing the thang");

            if (value.started)
            {
                // todo; get the current partner.
                if (LoveMeter.Instance.UsePower(Partners.Alice))
                {
                    print("yes");
                }
                else
                {
                    print("no");
                }
            }
        }
    }
}