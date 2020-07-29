using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


[System.Serializable]
public class EntityMovementComponent : MonoBehaviour
{
    public float velocity;
    void Update()
    {
        Vector3 d = new Vector3(0, 1, 0);
        gameObject.transform.Translate(Vector3.down*Time.deltaTime*-velocity);
    }
}
