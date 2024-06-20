using System;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchConfiningBoxTrigger : MonoBehaviour
{
    public BoxCollider2D shapeToSwitchTo;

    private CinemachineConfiner confiner;

    private CinemachineCamera cam;
    
    private void Awake()
    {
        cam = GameObject.FindAnyObjectByType<CinemachineCamera>();

        confiner = cam.GetComponent<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("collision entered; " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            print("collideded with player");
            // switch camera over. 
            confiner.m_BoundingShape2D = shapeToSwitchTo.GetComponent<CompositeCollider2D>();
            // need to refresh the cache.
            print("[CAMERA] Refreshing Bounding Shape Cache");
            confiner.InvalidateCache();
        }
    }
}
