using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive UDPReceive;
    public GameObject[] handPoints;
    public string[] points;
    void Start()
    {

    }

    void Update()
    {
        string data = UDPReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        print(data);
        points = data.Split(',');
        print(points[0]);
        for (int i = 0; i < 21; i++)
        {
            float x = (float.Parse(points[i * 3]) - 640) / 70;
            float y = (float.Parse(points[i * 3 + 1])) / 70;
            float z = float.Parse(points[i * 3 + 2]) / 70;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        }
    }
}
delegate void EventHandler(string[] s);

public class MyGrab : MonoBehaviour
{
    event EventHandler EventHappened;
    UnityEvent myEvent;
    HandTracking h = new HandTracking();

    void Start()
    {
        if (myEvent == null)
            myEvent = new UnityEvent();
    }
    void Update()
    {
        if ((string.Compare(h.points[25], h.points[19])) >= 0 && (string.Compare(h.points[37], h.points[31])) >= 0 && (string.Compare(h.points[49], h.points[43])) >= 0 && (string.Compare(h.points[61], h.points[55]) >= 0))
        {
            myEvent.Invoke();
        }
    }

    void OnDestroy()
    {
        print("Grab");
    }
}
