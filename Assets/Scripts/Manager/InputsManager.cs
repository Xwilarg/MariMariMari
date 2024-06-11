using System.Collections.Generic;
using TouhouPride.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Manager
{
    public class InputsManager : MonoBehaviour
    {
        public static InputsManager Instance { private set; get; }

        private List<PlayerController> _controllers = new();

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
            foreach (var c  in _controllers) if (c.enabled) c.OnMove(value);
        }

        public void OnSwitchCharacter(InputAction.CallbackContext value)
        {
            foreach (var c in _controllers) if (c.enabled) c.OnSwitchCharacter(value);
        }

        public void OnStrafe(InputAction.CallbackContext value)
        {
            foreach (var c in _controllers) if (c.enabled) c.OnStrafe(value);
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            foreach (var c in _controllers) if (c.enabled) c.OnShoot(value);
        }
    }
}