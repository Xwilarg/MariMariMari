using UnityEngine;
using UnityEngine.Events;

namespace TouhouPride
{
    public class Detector : MonoBehaviour
    {
        public UnityEvent<Collider2D> OnEnter { get; } = new();
        public UnityEvent<Collider2D> OnExit { get; } = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnter.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnExit.Invoke(collision);
        }
    }
}