using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerProgression : MonoBehaviour
{
    [Header("UI��������")]
    public Vector3 uiHoverScale;
    public Color uiHoverColor;
    public Color uiOriginalColor;
    public AudioSource uiEnterAudio;

    [Header("����")]
    public Vector3 boardMax;
    public float animTime;
    bool rule;
    GameObject board;
    Vector3 boardScale;
    bool isShrinking;

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ����ҽ���
        PlayerPrefs.GetInt("PlayerProgression", 0);

        //���ع���
        rule = false;

        //��ȡ����,��ʼ��
        board = GameObject.Find("Board");
        boardScale = board.transform.localScale;
        board.GetComponent<SpriteRenderer>().enabled = false;
        isShrinking = false;
    }


    //��ʾor���ع������
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

    //ִ�а��Ӷ���
    IEnumerator BoardShow()
    {
        yield return new WaitForSeconds(animTime);
        isShrinking = false;
        board.transform.localScale = boardScale;
    }

}
