using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public delegate MyClassEventArgs : EventArgs {  }
event EventHandler<MyClassEventArgs> MyClassEvent;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive UDPReceive;
    public GameObject[] handPoints;
    
    void Start()
    {
        if (this.MyGrab != null)
        {
            MyGrab(this, EventArgs.Empty);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandTracking htk = new HandTracking();
        htk.Send();
        htk.Text = "Grab";
        string data = UDPReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);
        print(data);
        string[] points = data.Split(',');
        print(points[0]);
        for (int i = 0; i < 21; i++)
        {
            float x = (float.Parse(points[i * 3]) - 640)/70;
            float y = (float.Parse(points[i * 3 + 1])) / 70;
            float z = float.Parse(points[i * 3 + 2]) / 70;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
            if (points[12] < points[15] && points[25] > points[19] && points[37] > points[31] && points[49] > points[43] && points[61] > points[55])
            {
                MyGrab.Send();
            }
        }
    }
    void Send()
    {
        htk.SendMessage("Grab");
    }
}
