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

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ����ҽ���
        PlayerPrefs.GetInt("PlayerProgression", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
