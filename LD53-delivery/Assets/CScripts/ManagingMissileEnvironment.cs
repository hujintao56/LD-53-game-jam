using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
/// <summary>
/// ���ڴ洢�����뻷�������ķ���
/// </summary>
public class ManagingMissileEnvironment : MonoBehaviour
{
    private PackageManagement PkGManage;
    /// <summary>
    /// �����������ýű�
    /// </summary>
    private RocketLauncher launcher;
    /// <summary>
    /// Tag���� �����ƻ��Ľ����͵���
    /// </summary>
    public string TagName_FloorOrBuild;
    /// <summary>
    /// Tag���� �Ʋ�
    /// </summary>
    public string TagName_Cloud;
    /// <summary>
    /// Tag���� �������ߵ����յ�
    /// </summary>
    public string TagName_WinOrDirection;
    /// <summary>
    /// Tag���� ����
    /// </summary>
    public string TagName_UnrulyPeople;
    /// <summary>
    /// tag����  ��ˮ���ٲ�
    /// </summary>
    public string TagName_Flowing_Water;
    /// <summary>
    /// Tag���� ����
    /// </summary>
    public string TagName_Bamboo_Forest;
    /// <summary>
    /// Tag���� ����
    /// </summary>
    public string TagName_Swamp;
    /// <summary>
    /// Tag���� ѩɽ
    /// </summary>
    public string TagName_Snow_Mountain;
    /// <summary>
    /// Tag���� �³�
    /// </summary>
    public string TagName_Cable_Car;
    /// <summary>
    /// Tag���� ��
    /// </summary>
    public string TagName_Fish;
    /// <summary>
    /// Tag���� ľ��
    /// </summary>
    public string TagName_Raft;
    /// <summary>
    /// Tag���� ����
    /// </summary>
    public string TagName_TreasureChest;


    /// <summary>
    /// �����Ʋ������ٵ��ٶ�
    /// </summary>
    public float SlowSpeed_CrossCloud;


    private void Start()
    {
        launcher = GetComponent<RocketLauncher>();
        PkGManage = GetComponent<PackageManagement>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagName_FloorOrBuild)
        {
            Debug.Log("MissileBoom");
        }
        if (collision.gameObject.tag == TagName_WinOrDirection)
        {
            Debug.Log("Win");
        }
        if (collision.gameObject.tag == TagName_UnrulyPeople)
        {
            Destroy(collision.gameObject);
            //���õ�����������     ������Ϊ����һ��Ҫ�½ű� 
        }
        if (collision.gameObject.tag == TagName_Flowing_Water)
        {
            PkGManage.TurnOn_Rig();
            PkGManage.RankDown();
            //���ð����������ã���������������߽���������ƣ������������÷�������
            //���õ������� ����ճ�Է�������
        }
        if (collision.gameObject.tag == TagName_Bamboo_Forest)
        {
            Destroy(collision.gameObject);
            PkGManage.TurnOff_Rig();
            //���ð����������ã����ð���λ��
        }
        if (collision.gameObject.tag == TagName_Swamp)
        {
            //���õ�������
            PkGManage.RankDown();
        }

        if (collision.gameObject.tag == TagName_Fish)
        {
            Destroy(collision.gameObject);
            PkGManage.RankDown();
        }

        if (collision.gameObject.tag == TagName_Raft)
        {
            Destroy(collision.gameObject);
            PkGManage.RankDown();
        }

        if (collision.gameObject.tag == TagName_TreasureChest)
        {
            //bool 
        }
        





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagName_Cloud)
        {
            launcher.launchForce -= SlowSpeed_CrossCloud;
        }

        if (collision.gameObject.tag == TagName_Snow_Mountain)
        {
            //������������
        }

        if (collision.gameObject.tag == TagName_Cable_Car)
        {
            //������������
        }


    }





}
