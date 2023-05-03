using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Package : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 offsetPos;
    private GameObject launcherRef;
        
    public UnityEvent<Vector3> packageStopped;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        launcherRef = FindFirstObjectByType<Slingshot>().gameObject;
        packageStopped.AddListener(launcherRef.GetComponent<Slingshot>().Reset);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        offsetPos = new Vector3(pos.x + 7.9f, pos.y + 3.8f, pos.z);
        
        if (rb2d.velocity.magnitude <= 0.0001f)
        {
            packageStopped.Invoke(offsetPos);
            enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        packageStopped.Invoke(launcherRef.transform.position);
    }
}
