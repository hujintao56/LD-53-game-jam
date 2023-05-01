using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this script on the objective gameobject, add an indicator sprite object as its child and assign to indicator
// Indicator only works when the objective is invisible from the camera (including Editor Scene View!)
public class IndicatorScript : MonoBehaviour
{
    public GameObject indicator;
    public GameObject cam;

    private Renderer rd;
    
    void Start()
    {
        if (cam == null)
        {
            cam = FindFirstObjectByType<UnityEngine.Camera>().gameObject;
        }
        rd = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!rd.isVisible)
        {
            print("invisible");
            if (!indicator.activeSelf)
            {
                indicator.SetActive(true);
            }

            Vector3 pos = transform.position;
            Vector2 dir = cam.transform.position - pos;

            RaycastHit2D ray = Physics2D.Raycast(pos, dir);
            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
                float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
                indicator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else
        {
            print("visible");
            if (indicator.activeSelf)
            {
                indicator.SetActive(false);
            }
        }
    }
}
