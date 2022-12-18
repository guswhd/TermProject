using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive UDPReceive;
    public GameObject[] handPoints;
    public Rigidbody rb;
    public float distance=0;
    public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = UDPReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);
        //print(data);
        string[] points = data.Split(',');
       // print(points[0]);
        for (int i = 0; i < 21; i++)
        {
            float x = (float.Parse(points[i * 3]) - 640)/70;
            float y = (float.Parse(points[i * 3 + 1])) / 70;
            float z = float.Parse(points[i * 3 + 2]) / 70;
            if((x>-1.5&&x<6.4)&&(y>-1.2&&y<5.5)){
                handPoints[i].transform.localPosition = new Vector3(x, 9, y);
        }
        
    }
}
}
