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

    void Awake()
    {
        MakeSingleton();
        reset();
        txtHealth.text = "HIGH:" + health;
        txtScore.text = "SCORE:" + score;
    }

    private void MakeSingleton()
    {
        if(instance = null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    }
    public void Score()
    {
        score += 1;
    }
}
