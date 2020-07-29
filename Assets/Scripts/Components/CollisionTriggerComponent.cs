using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit detected between " + gameObject.name + " and " + other.gameObject.name);
    }
}
