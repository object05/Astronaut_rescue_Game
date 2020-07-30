using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
    public static DebugMode instance;
    public bool debug = false;

    private GameObject[] activeMeteors;
    private GameObject[] activeAstronautsWhite;
    //private GameObject[] activeAstronautsOrange;
    public GameObject rocket;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        activeMeteors = GameObject.FindGameObjectsWithTag("Meteor");
        activeAstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
        //activeAstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
    }
    void Update()
    {
        if (debug)
        {
            activeMeteors = GameObject.FindGameObjectsWithTag("Meteor");
            activeAstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
            //activeAstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
            foreach(GameObject m in activeMeteors)
            {
                setLine(m);
            }

            foreach(GameObject aW in activeAstronautsWhite)
            {
                setLine(aW);
            }

            setLine(rocket);

            //foreach(GameObject aO in activeAstronautsOrange)
            //{
            //    setLine(aO);
            //}
        }
    }

    public void toggleDebugging()
    {
        if (debug)
        {
            activeMeteors = GameObject.FindGameObjectsWithTag("Meteor");
            activeAstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
            //activeAstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
            foreach (GameObject m in activeMeteors)
            {
                removeLine(m);
            }

            foreach (GameObject aW in activeAstronautsWhite)
            {
                removeLine(aW);
            }
            removeLine(rocket);

        }
        debug = !debug;
    }


    void setLine(GameObject obj)
    {
        var line = obj.GetComponent<LineRenderer>();

        line.sortingLayerName = "OnTop";
        line.sortingOrder = 5;
        line.positionCount = 5;

        line.SetPosition(0, new Vector3(obj.GetComponent<BoxCollider2D>().bounds.min.x, obj.GetComponent<BoxCollider2D>().bounds.min.y));
        line.SetPosition(1, new Vector3(obj.GetComponent<BoxCollider2D>().bounds.min.x, obj.GetComponent<BoxCollider2D>().bounds.max.y));
        line.SetPosition(2, new Vector3(obj.GetComponent<BoxCollider2D>().bounds.max.x, obj.GetComponent<BoxCollider2D>().bounds.max.y));
        line.SetPosition(3, new Vector3(obj.GetComponent<BoxCollider2D>().bounds.max.x, obj.GetComponent<BoxCollider2D>().bounds.min.y));
        line.SetPosition(4, new Vector3(obj.GetComponent<BoxCollider2D>().bounds.min.x, obj.GetComponent<BoxCollider2D>().bounds.min.y));
        //line.SetWidth(0.05f, 0.05f);
        //line.useWorldSpace = true;
        line.startWidth = 0.06f;
        line.endWidth = 0.06f;
        if (line.enabled == false)
        {
            line.enabled = true;
        }
    }

    public void removeLine(GameObject obj)
    {
        var line = obj.GetComponent<LineRenderer>().enabled = false;
    }
}
