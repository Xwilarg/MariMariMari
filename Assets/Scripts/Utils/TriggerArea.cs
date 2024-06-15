using UnityEngine;
using UnityEngine.Events;

namespace TouhouPride.Utils
{
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Collider2D> _onEnter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onEnter.Invoke(collision);
        }
    }
}