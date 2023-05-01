using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclicMovement : MonoBehaviour
{
    public float speed;
    public float threshold;
    private bool up;
    private Vector3 upperLimit;
    private Vector3 downerLimit;

    void Start()
    {
        upperLimit = this.transform.position + new Vector3(0, threshold, 0);
        downerLimit = this.transform.position - new Vector3(0, threshold, 0);
        up = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.transform.position.y >= upperLimit.y)
        {
            up = false;
        }

        if (this.transform.position.y <= downerLimit.y)
        {
            up = true;
        }

        if (up)
        {
            this.transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
        }
    }
}
