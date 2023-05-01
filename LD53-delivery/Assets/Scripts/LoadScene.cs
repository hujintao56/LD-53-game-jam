using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //��ť����
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
        //��ȡUI��������
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

        //��������UI��ɫ
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = originalColor;
    }
    private void OnMouseDown()
    {
        if(type == ButtonType.Start)
        {
            //��������gameobjectͬ������
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

    //������UI��ײ����������
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
