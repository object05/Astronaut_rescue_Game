using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public float movementSpeed = 1f;
    private CharacterController controller;
    private Vector3 move;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        move = new Vector3(0,0,0);
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (Input.GetKeyDown("left"))
        {
            Debug.Log("left");
        }
        else if (Input.GetKeyDown("right"))
        {
            Debug.Log("right");
        }
        controller.Move(move * Time.deltaTime * movementSpeed);
    }
}
