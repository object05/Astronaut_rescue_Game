using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponent : MonoBehaviour
{
    public void forceRemove()
    {
        Pooling.Instance.Push(gameObject.name, gameObject);
    }
}
