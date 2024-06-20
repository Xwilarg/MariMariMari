using System;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchConfiningBoxTrigger : MonoBehaviour
{
    public BoxCollider2D shapeToSwitchTo;

    // I know this is technically obsoleted; but I can't seem to get this working with the new Cinemachine confiner. 
    private CinemachineConfiner confiner;

    private CinemachineCamera cam;
    
    private void Awake()
    {
        cam = GameObject.FindAnyObjectByType<CinemachineCamera>();

        confiner = cam.GetComponent<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //print("collision entered; " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            //print("collideded with player");
            // disable cam confines
            confiner.enabled = false;
            
            // focus cinemachine cam on new bounding shape; wait until thats done. 
            
            // switch camera over. 
            confiner.m_BoundingShape2D = shapeToSwitchTo.GetComponent<CompositeCollider2D>();
            
            // reenable cam confines.
            confiner.enabled = true;
            
            // need to refresh the cache.
            print("[CAMERA] Refreshing Bounding Shape Cache");
            confiner.InvalidateCache();
        }
    }
}
