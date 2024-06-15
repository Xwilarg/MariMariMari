using System;
using TouhouPride;
using UnityEngine;
using UnityEngine.Events;

public class PartnerPickup : MonoBehaviour
{
    // TODO; probably want to pick graphic / animation based on this value here.
    public Partners partnerValue;

    public UnityEvent pickupCollect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collision!");
        if (other.name == "Player")
        {
            pickupCollect.Invoke();
            LoveMeter.Instance.AddPoint(partnerValue);
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other);
    }
}
