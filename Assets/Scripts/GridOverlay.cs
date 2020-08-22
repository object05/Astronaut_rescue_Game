using UnityEngine;
using System.Collections;

public class GridOverlay : MonoBehaviour
{
    public bool enabled;

    void Update()
    {
        if (enabled)
        {
            int startW = (int)-GameManager.instance.halfWidth;
            int endW = (int)GameManager.instance.halfWidth;

            int startH = (int)-GameManager.instance.halfHeight;
            int endH = (int)GameManager.instance.halfHeight;

            int count = 0;
            for (int i = startW; i <= endW; i++)
            {
                Debug.DrawLine(new Vector3(startW + count, startH, 0), new Vector3(startW + count, endH, 0), Color.yellow);
                count++;
            }
            count = 0;
            for (int i = startH; i <= endH; i++)
            {
                Debug.DrawLine(new Vector3(startW, startH + count, 0), new Vector3(endW, startH + count, 0), Color.yellow);
                count++;
            }
        }
    }
}