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
    public Text infoText;

    private Camera cam;
    public float halfHeight;
    public float halfWidth;

    public bool isPause = false;
    public bool isGameOver = false;



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
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (!isGameOver)
            {

                if (isPause)
                {
                    infoText.text = "";
                }
                else
                {
                    infoText.text = "GAME PAUSED";
                }
                isPause = !isPause;
            }
            else
            {
                reset();
                infoText.color = Color.yellow;
                infoText.text = "";
                GameObject rocket = GameObject.FindGameObjectWithTag("Rocket");
                rocket.transform.position = new Vector3(0, -halfHeight+rocket.GetComponent<SpriteRenderer>().size.y/2,0);
                Pooling.Instance.ResetPools();

                isPause = false;
            
            }
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
        if(health <= 0)
        {
            isGameOver = true;
            isPause = true;
            infoText.text = "GAME OVER";
            infoText.color = Color.red;
        }
    }
    public void Score()
    {
        score += 1;
        txtScore.text = "SCORE:" + score;
    }



}
