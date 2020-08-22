using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponent : MonoBehaviour
{
    public void forceRemove()
    {
        GameManager.instance.GetComponent<DebugMode>().removeLine(gameObject);
        Pooling.Instance.Push(gameObject.tag, gameObject);
    }
}
