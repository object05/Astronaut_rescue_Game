using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit detected between " + gameObject.name + " and " + other.name);
        other.GetComponent<RemoveComponent>().forceRemove();//pushes to pool


        if(other.tag == "Meteor")
        {
            GameManager.instance.Damage();

        }
        else
        {
            GameManager.instance.Score();
        }

        //todo sound play
        //todo particle effect
        //todo points
    }
}
