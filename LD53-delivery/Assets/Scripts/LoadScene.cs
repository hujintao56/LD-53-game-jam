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
        //��������gameobjectͬ������
        SceneManager.LoadScene(this.gameObject.name);
        
        //��ȡUI��������
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

        //��������UI��ɫ
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = originalColor;
    }

    //������UI��ײ����������
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
