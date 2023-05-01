using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
/// <summary>
/// 用于存储导弹与环境交互的方法
/// </summary>
public class ManagingMissileEnvironment : MonoBehaviour
{
    private PackageManagement PkGManage;
    /// <summary>
    /// 导弹属性配置脚本
    /// </summary>
    private RocketLauncher launcher;
    /// <summary>
    /// Tag名字 不可破坏的建筑和地面
    /// </summary>
    public string TagName_FloorOrBuild;
    /// <summary>
    /// Tag名字 云层
    /// </summary>
    public string TagName_Cloud;
    /// <summary>
    /// Tag名字 包裹或者导弹终点
    /// </summary>
    public string TagName_WinOrDirection;
    /// <summary>
    /// Tag名字 刁民
    /// </summary>
    public string TagName_UnrulyPeople;
    /// <summary>
    /// tag名字  流水或瀑布
    /// </summary>
    public string TagName_Flowing_Water;
    /// <summary>
    /// Tag名字 竹林
    /// </summary>
    public string TagName_Bamboo_Forest;
    /// <summary>
    /// Tag名字 沼泽
    /// </summary>
    public string TagName_Swamp;
    /// <summary>
    /// Tag名字 雪山
    /// </summary>
    public string TagName_Snow_Mountain;
    /// <summary>
    /// Tag名字 缆车
    /// </summary>
    public string TagName_Cable_Car;
    /// <summary>
    /// Tag名字 鱼
    /// </summary>
    public string TagName_Fish;
    /// <summary>
    /// Tag名字 木筏
    /// </summary>
    public string TagName_Raft;
    /// <summary>
    /// Tag名字 宝箱
    /// </summary>
    public string TagName_TreasureChest;


    /// <summary>
    /// 穿过云层所减少的速度
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
            //调用刁民死亡方法     刁民行为不单一需要新脚本 
        }
        if (collision.gameObject.tag == TagName_Flowing_Water)
        {
            PkGManage.TurnOn_Rig();
            PkGManage.RankDown();
            //调用包裹属性配置，增加物理下落或者解除物理限制，包裹属性设置分数减少
            //设置导弹属性 禁用粘性方法调用
        }
        if (collision.gameObject.tag == TagName_Bamboo_Forest)
        {
            Destroy(collision.gameObject);
            PkGManage.TurnOff_Rig();
            //调用包裹属性配置，设置包裹位置
        }
        if (collision.gameObject.tag == TagName_Swamp)
        {
            //设置导弹属性
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
            //导弹属性设置
        }

        if (collision.gameObject.tag == TagName_Cable_Car)
        {
            //包裹属性设置
        }


    }





}
