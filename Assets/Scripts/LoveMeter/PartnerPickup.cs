using TouhouPride.SO;
using UnityEngine;

namespace TouhouPride.Love
{
    public class PartnerPickup : MonoBehaviour
    {
        // TODO; probably want to pick graphic / animation based on this value here.
        [SerializeField]
        private PlayerInfo _partnerTarget;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LoveMeter.Instance.AddPoint(_partnerTarget.Name);

                Destroy(gameObject);
            }
        }
    }
}
