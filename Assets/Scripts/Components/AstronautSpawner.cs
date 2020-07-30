using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautSpawner : MonoBehaviour
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
                    Vector3 size = new Vector3(1, 1);
                    Vector3 position = new Vector3(Random.Range(-GameManager.instance.halfWidth, GameManager.instance.halfWidth), GameManager.instance.halfHeight, 0);
                    Quaternion rotation = new Quaternion(Random.Range(0, 180), 0, 0, 0);
                    float velocity = Random.Range(1f, 3f);
                    //spawn here
                    Pooling.Instance.Pull(spawnObject.name, size, position, rotation, velocity);
                    timer = interval;
                }
            }
        }
    }
}
