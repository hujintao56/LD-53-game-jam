using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerProgression : MonoBehaviour
{
    [Header("UI交互参数")]
    public Vector3 uiHoverScale;
    public Color uiHoverColor;
    public Color uiOriginalColor;
    public AudioSource uiEnterAudio;

    [Header("板子")]
    public Vector3 boardMax;
    public float animTime;
    bool rule;
    GameObject board;
    Vector3 boardScale;
    bool isShrinking;

    // Start is called before the first frame update
    void Start()
    {
        //初始化玩家进度
        PlayerPrefs.GetInt("PlayerProgression", 0);

        //隐藏规则
        rule = false;

        //获取板子,初始化
        board = GameObject.Find("Board");
        boardScale = board.transform.localScale;
        board.GetComponent<SpriteRenderer>().enabled = false;
        isShrinking = false;
    }


    //显示or隐藏规则板子
    public void Rule()
    {
        if (!rule)
        {
            StartCoroutine("BoardShow");
            board.transform.localScale = boardMax;
            board.GetComponent<SpriteRenderer>().enabled = true;
            isShrinking = true;
        }
        else
        {
            board.GetComponent<SpriteRenderer>().enabled = false;
            StopCoroutine("BoardShow");
        }

        rule = !rule;
    }

    void Update()
    {
        if (isShrinking)
        {
            board.transform.localScale -= new Vector3((boardMax.x - boardScale.x) * Time.deltaTime / animTime, (boardMax.y - boardScale.y) * Time.deltaTime / animTime, (boardMax.y - boardScale.y) * Time.deltaTime / animTime);
        }
    }

    //执行板子动画
    IEnumerator BoardShow()
    {
        yield return new WaitForSeconds(animTime);
        isShrinking = false;
        board.transform.localScale = boardScale;
    }

}
