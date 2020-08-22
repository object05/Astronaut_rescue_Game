using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


public class EntityMovement : MonoBehaviour
{
    public float velocity;
    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * velocity,Space.World);

            if (gameObject.transform.position.y < -GameManager.instance.halfHeight + gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2)
            {
                gameObject.GetComponent<RemoveComponent>().forceRemove();
            }
        }
    }
}
