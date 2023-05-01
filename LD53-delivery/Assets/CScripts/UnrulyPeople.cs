using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnrulyPeople : MonoBehaviour
{
    private PackageManagement PKGManage;
    private int range;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        range = Random.Range(-3,3);
        transform.Translate(range *Time.deltaTime, 0, 0);



    }



}
