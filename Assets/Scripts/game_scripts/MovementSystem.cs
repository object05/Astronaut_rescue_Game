using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float movementSpeed;
    private CharacterController controller;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            float halfHeight = GameManager.instance.halfHeight;
            float halfWidth = GameManager.instance.halfWidth;

            halfHeight = Camera.main.orthographicSize;
            halfWidth = Camera.main.aspect * halfHeight;

            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            gameObject.transform.Translate(move * Time.deltaTime * movementSpeed);


            if (gameObject.transform.position.x < -halfWidth + (GetComponent<SpriteRenderer>().bounds.size.x) / 2)//left wall
            {
                gameObject.transform.position = new Vector3(-halfWidth + (GetComponent<SpriteRenderer>().bounds.size.x) / 2, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.x > halfWidth - (GetComponent<SpriteRenderer>().bounds.size.x) / 2)//right wall
            {
                gameObject.transform.position = new Vector3(halfWidth - (GetComponent<SpriteRenderer>().bounds.size.x) / 2, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
}
