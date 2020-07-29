using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public float interval;
    public GameObject spawnObject;
    public float timer;
    public bool stopSpawner = false;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.aspect * halfHeight;


            Vector3 size = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
            Vector3 position = new Vector3(Random.Range(-halfWidth,halfWidth), halfHeight, 0);
            Quaternion rotation = new Quaternion(Random.Range(0, 180),0,0,0);
            float velocity = Random.Range(1f, 3f);
            //spawn here
            Pooling.Instance.Pull(spawnObject.name, size,position,rotation,velocity);
            timer = interval;
        }
    }
}
