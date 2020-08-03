using System.Collections.Generic;
using System.Diagnostics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class DebugMode : MonoBehaviour
{
    public static DebugMode instance;
    public bool debug = false;
    public bool grid = false;
    private bool gridOn = false;

    private GameObject[] Meteors;
    private GameObject[] AstronautsWhite;
    private GameObject[] AstronautsOrange;

    private GameObject[] objects;

    public GameObject rocket;

    public float camspeed = 2f;
    private float camXstart;
    private float camYstart;
    private float camZstart;
    private float camOrtho;

    public Text debugText;


    private Queue<LineDrawer> lines;

    int activeItems;

    DebugUIManager cpuUsage;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        activeItems = 0;
        cpuUsage = gameObject.GetComponent<DebugUIManager>();

        camOrtho = Camera.main.orthographicSize;
        camXstart = Camera.main.transform.position.x;
        camYstart = Camera.main.transform.position.y;
        camZstart = Camera.main.transform.position.z;
        lines = new Queue<LineDrawer>();

        Meteors = GameObject.FindGameObjectsWithTag("Meteor");
        AstronautsWhite = GameObject.FindGameObjectsWithTag("Astronaut_white");
        AstronautsOrange = GameObject.FindGameObjectsWithTag("Astronaut_orange");
    }
    void Update()
    {
        if (debug)
        {
            activeItems = 0;
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
            cameraMovement();
            memoryInfo();


            if (grid && !gridOn)
            {
                //Debug.Log("Grid on");
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

            debugText.text = "";
        }

        debug = !debug;
        gridOn = !debug;
    }


    void setLine(GameObject obj)
    {
        if (obj.activeSelf)
        {
            activeItems++;
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
        //obj.GetComponent<LineRenderer>().enabled = false;
        obj.GetComponent<LineRenderer>().enabled = false;
    }

    private void overlayGrid()
    {
        int startW = (int)-GameManager.instance.halfWidth+(int)Camera.main.transform.position.x;
        int endW = (int)GameManager.instance.halfWidth+(int)Camera.main.transform.position.x;
        int startH = (int)-GameManager.instance.halfHeight + (int)Camera.main.transform.position.y;
        int endH = (int)GameManager.instance.halfHeight + (int)Camera.main.transform.position.y;


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

    private void memoryInfo()
    {
        debugText.text = "CPU: " + cpuUsage.CpuUsage + "%\n"+
                         "Active objects: "+ activeItems;
    }

    private void cameraMovement()
    {
        if (Input.GetKey(KeyCode.Keypad6))
        {
            Camera.main.transform.Translate(new Vector3(camspeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            Camera.main.transform.Translate(new Vector3(-camspeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            Camera.main.transform.Translate(new Vector3(0, camspeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            Camera.main.transform.Translate(new Vector3(0, -camspeed * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.Keypad7))
        {
            //Camera.main.transform.Translate(transform.forward * camspeed * Time.deltaTime, Space.Self);
            Camera.main.orthographicSize += camspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad9))
        {
            Camera.main.orthographicSize -= camspeed * Time.deltaTime;
            //Camera.main.transform.Translate(transform.forward * -camspeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.Keypad0))
        {
            Camera.main.orthographicSize = camOrtho;
            Camera.main.transform.position = new Vector3(camXstart, camYstart, camZstart);
        }

        deleteGrid();
        overlayGrid();
    }


}
