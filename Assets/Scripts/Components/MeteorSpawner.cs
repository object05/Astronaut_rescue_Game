using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    private float interval;
    public GameObject spawnObject;
    public float timer;
    public bool stopSpawner = false;

    void Start()
    {
        interval = timer;
    }
    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (!stopSpawner)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    Vector3 size = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
                    Vector3 position = new Vector3(Random.Range(-GameManager.instance.halfWidth, GameManager.instance.halfWidth), GameManager.instance.halfHeight, 0);
                    float rotation = Random.Range(0, 360);
                    float velocity = Random.Range(1f, 3f);
                    Pooling.Instance.Pull(spawnObject.name, size, position, rotation, velocity);
                    timer = interval;
                }
            }
        }
    }
}
