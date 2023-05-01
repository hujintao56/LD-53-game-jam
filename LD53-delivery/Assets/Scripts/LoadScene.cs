using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //按钮种类
    enum ButtonType{Start, Rule, Exit};
    [SerializeField]
    ButtonType type;
    PlayerProgression pp;
    private Vector3 hoverScale;
    private Color hoverColor;
    private Color originalColor;
    private AudioSource enterAudio;
    private SpriteRenderer sprite;
    private float originalScale;

    private void Start()
    {
        //获取UI交互数据
        pp = GameObject.Find("PlayerProfileModule").GetComponent<PlayerProgression>(); 
        if (pp != null)
        {
            hoverScale = pp.uiHoverScale;
            hoverColor = pp.uiHoverColor;
            originalColor = pp.uiOriginalColor;
            originalScale = this.transform.localScale.x;
            if (pp.uiEnterAudio != null && enterAudio != null)
            {
                enterAudio = pp.uiEnterAudio;
            }
        }

        //设置自身UI颜色
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = originalColor;
    }
    private void OnMouseDown()
    {
        if(type == ButtonType.Start)
        {
            //点击后加载gameobject同名场景
            SceneManager.LoadScene(this.gameObject.name);
        }
        else if (type == ButtonType.Rule)
        {
            pp.Rule();
        }
        else if (type == ButtonType.Exit)
        {
            Application.Quit();
        }
    }

    //鼠标进出UI碰撞，触发交互
    private void OnMouseExit()
    {
        sprite.color = originalColor;
        this.transform.localScale = Vector3.one * originalScale;
    }
    private void OnMouseEnter()
    {
        sprite.color = hoverColor;
        this.transform.localScale = originalScale * hoverScale;
        if (enterAudio != null)
        {
            if (!enterAudio.isPlaying)
            {
                enterAudio.Play();
            }
        }
    }
}
