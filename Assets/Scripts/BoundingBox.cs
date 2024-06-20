using System;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    private void Awake()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();

        collider2D.usedByComposite = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
