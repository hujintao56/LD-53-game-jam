using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �������Ĺ���
/// </summary>
public class PackageManagement : MonoBehaviour
{
    private BoxCollider2D col;
    private Rigidbody2D rig;

    public int PackageRank;


    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    public void RankDown()
    {
        PackageRank -= 1;
    }

    public void TurnOff_Rig()
    {
        rig.Sleep();
    }

    public void TurnOn_Rig()
    {
        rig.WakeUp();
    }


}
