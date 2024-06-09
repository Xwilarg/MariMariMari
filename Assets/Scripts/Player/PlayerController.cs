using TouhouPride.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouhouPride.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;
        private Vector2 _mov;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * _info.Speed;
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
        }
    }
}
