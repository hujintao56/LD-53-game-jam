using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float leftRemoveX = -25;
    [SerializeField] private float rightSpawnX = 125;
    [SerializeField] private float speed = 1f;
    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * speed), Space.World);
        
        if (transform.position.x < leftRemoveX)
        {
            transform.position = new Vector3(rightSpawnX, transform.position.y, transform.position.z);
        }
    }
}
