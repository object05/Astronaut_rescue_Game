using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pooling : MonoBehaviour
{

    public static Pooling Instance;//singleton
    private void Awake()
    {
        Instance = this;
    }


    public Dictionary<string, Queue<GameObject>> dictPool;//pool dictionary, so we can reference with tags
    public List<Pool> pools; //list of pools

    void Start()
    {
        dictPool = new Dictionary<string, Queue<GameObject>>();//create dictionary
        foreach (Pool pool in pools)//for every type of pool...
        {
            Queue<GameObject> objPool = new Queue<GameObject>();//create a queue for type of pool
            for(int i = 0; i < pool.poolSize; i++)//for every item in pool...
            {
                GameObject obj = Instantiate(pool.objectToPool);//create new instance for that item
                obj.SetActive(false);//and disable
                objPool.Enqueue(obj);
            }

            dictPool.Add(pool.name, objPool);
        }
    }

    public GameObject Pull(string name, Vector3 size, Vector3 position, Quaternion rotation)
    {
        if (dictPool.ContainsKey(name))
        {
            GameObject obj = dictPool[name].Dequeue();
            obj.SetActive(true);
            obj.transform.rotation = rotation;
            obj.transform.position = position;
            obj.transform.localScale = size;

            //TODO SPEED

            dictPool[name].Enqueue(obj);
            return obj;
        }
        else
        {
            Debug.LogWarning("No such name in pool dictionary: " + name);
            return null;
        }
    }

}
