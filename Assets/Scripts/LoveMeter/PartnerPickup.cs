using System;
using TouhouPride;
using UnityEngine;

public class PartnerPickup : MonoBehaviour
{
    // TODO; probably want to pick graphic / animation based on this value here.
    public Partners partnerValue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collision!");
        if (other.name == "Player")
        {
            LoveMeter.Instance.AddPoint(partnerValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other);
    }
}
