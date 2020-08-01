using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
    public static DebugMode instance;
    public bool debug = false;
    public bool grid = false;
    private bool gridOn = false;

    private GameObject[] Meteors;
    private GameObject[] AstronautsWhite;
    private GameObject[] AstronautsOrange;
    public GameObject rocket;

    private Queue<LineDrawer> lines;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        lines = new Queue<LineDrawer>();
        Meteors = GameObject.FindGameObjectsWithTag("Meteor");
        AstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
        AstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
    }
    void Update()
    {
        if (debug)
        {
            Meteors = GameObject.FindGameObjectsWithTag("Meteor");
            AstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
            AstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
            foreach(GameObject m in Meteors)
            {
                setLine(m);
            }

            foreach(GameObject aW in AstronautsWhite)
            {
                setLine(aW);
            }

            foreach (GameObject aO in AstronautsOrange)
            {
                setLine(aO);
            }

            setLine(rocket);
            if (grid && !gridOn)
            {
                Debug.Log("Grid on");
                overlayGrid();
                gridOn = true;
            }

        }
    }

    public void toggleDebugging()
    {
        if (debug)
        {
            Meteors = GameObject.FindGameObjectsWithTag("Meteor");
            AstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
            AstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
            foreach (GameObject m in Meteors)
            {
                removeLine(m);
            }
            foreach (GameObject aW in AstronautsWhite)
            {
                removeLine(aW);
            }
            foreach (GameObject aO in AstronautsOrange)
            {
                removeLine(aO);
            }
            removeLine(rocket);
            deleteGrid();//todo olepsaj
        }

        debug = !debug;
        gridOn = !debug;
    }


    void setLine(GameObject obj)
    {
        if (obj.activeSelf)
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
            line.enabled = true;
        }
    }

    public void removeLine(GameObject obj)
    {
        obj.GetComponent<LineRenderer>().enabled = false;
    }

    private void overlayGrid()
    {
        int startW = (int)-GameManager.instance.halfWidth;
        int endW = (int)GameManager.instance.halfWidth;
        int startH = (int)-GameManager.instance.halfHeight;
        int endH = (int)GameManager.instance.halfHeight;


        int count = 0;
        for (int i = startW; i <= endW; i++)
        {
            LineDrawer ld = new LineDrawer();
            ld.DrawLineInGameView(new Vector3(startW + count, startH, 0), new Vector3(startW + count, endH, 0), Color.yellow);
            lines.Enqueue(ld);
            count++;
        }
        count = 0;
        for (int i = startH; i <= endH; i++)
        {
            LineDrawer ld = new LineDrawer(0.05f);
            ld.DrawLineInGameView(new Vector3(startW, startH + count, 0), new Vector3(endW, startH + count, 0), Color.yellow);
            lines.Enqueue(ld);
            count++;
        }
    }

    private void deleteGrid()
    {
        foreach (var item in lines)
        {
            item.Destroy();
        }
        lines.Clear();
    }

    //private void overlayGrid()
    //{
    //    var line = GameManager.instance.GetComponent<LineRenderer>();

    //    int startW = (int)-GameManager.instance.halfWidth;
    //    int endW = (int)GameManager.instance.halfWidth;
    //    int startH = (int)-GameManager.instance.halfHeight;
    //    int endH = (int)GameManager.instance.halfHeight;

    //    Queue<Vector3> pos = new Queue<Vector3>();

    //    int count = 0;
    //    for (int i = startW; i <= endW; i++)
    //    {
    //        //Debug.DrawLine(new Vector3(startW + count, startH, 0), new Vector3(startW + count, endH, 0), Color.yellow);
    //        pos.Enqueue(new Vector3(startW + count, startH, 0));
    //        pos.Enqueue(new Vector3(startW + count, endH, 0));

    //        count++;
    //    }
    //    count = 0;
    //    for (int i = startH; i <= endH; i++)
    //    {
    //        pos.Enqueue(new Vector3(startW, startH + count, 0));
    //        pos.Enqueue(new Vector3(endW, startH + count, 0));
    //        //Debug.DrawLine(new Vector3(startW, startH + count, 0), new Vector3(endW, startH + count, 0), Color.yellow);
    //        count++;
    //    }


    //}
}
