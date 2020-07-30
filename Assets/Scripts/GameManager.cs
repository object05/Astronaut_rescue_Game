using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int health;
    private int score;
    private int max_score;

    public Text txtHealth;
    public Text txtScore;
    public Text txtmax_score;

    private Camera cam;
    public float halfHeight;
    public float halfWidth;



    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        cam = Camera.main;
        reset();
        txtHealth.text = "LIFE:" + health;
        txtScore.text = "SCORE:" + score;
        halfHeight = cam.orthographicSize;
        halfWidth = cam.aspect * halfHeight;
    }

    void Update()
    {
        halfHeight = cam.orthographicSize;
        halfWidth = cam.aspect * halfHeight;


        if (Input.GetKeyDown(KeyCode.F1))
        {
            DebugMode.instance.toggleDebugging();
        }
    }



    private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void reset()
    {
        health = 100;
        score = 0;
    }

    public void Damage()
    {
        health -= 10;
        txtHealth.text = "LIFE:" + health;
    }
    public void Score()
    {
        score += 1;
        txtScore.text = "SCORE:" + score;
    }


}
