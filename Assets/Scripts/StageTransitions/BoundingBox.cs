using System;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    private void Awake()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        collider2D.usedByComposite = true;
    }
}