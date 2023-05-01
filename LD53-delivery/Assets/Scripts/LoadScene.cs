using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    PlayerProgression pp;
    private Vector3 hoverScale;
    private Color hoverColor;
    private Color originalColor;
    private AudioSource enterAudio;
    private SpriteRenderer sprite;
    private void OnMouseDown()
    {
        //点击后加载gameobject同名场景
        SceneManager.LoadScene(this.gameObject.name);
        
        //获取UI交互数据
        pp = GameObject.Find("PlayerProfileModule").GetComponent<PlayerProgression>();
        if(pp != null)
        {
            hoverScale = pp.uiHoverScale;
            hoverColor = pp.uiHoverColor;
            originalColor = pp.uiOriginalColor;
            if (pp.uiEnterAudio != null && enterAudio != null)
            {
                enterAudio = pp.uiEnterAudio;
            }
        }

        //设置自身UI颜色
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = originalColor;
    }

    //鼠标进出UI碰撞，触发交互
    private void OnMouseExit()
    {
        sprite.color = originalColor;
        this.transform.localScale = Vector3.one;
    }
    private void OnMouseEnter()
    {
        sprite.color = hoverColor;
        this.transform.localScale = hoverScale;
        if (!enterAudio.isPlaying && enterAudio != null)
        {
            enterAudio.Play();
        }
    }
}
