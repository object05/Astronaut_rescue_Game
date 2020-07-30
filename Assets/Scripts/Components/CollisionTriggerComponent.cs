using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggerComponent : MonoBehaviour
{
    public AudioClip Hit;
    public AudioClip Pick;
    private AudioSource source;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit detected between " + gameObject.name + " and " + other.name);
        other.GetComponent<RemoveComponent>().forceRemove();//pushes to pool
        

        if(other.tag == "Meteor")
        {
            GameManager.instance.Damage();
            source.PlayOneShot(Hit);
        }
        else
        {
            GameManager.instance.Score();
            source.PlayOneShot(Pick);
        }

        //todo sound play
        //todo particle effect
    }
}
