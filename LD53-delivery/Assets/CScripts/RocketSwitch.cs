using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSwitch : MonoBehaviour
{
    [SerializeField] private Transform normalRocketTransform; // reference to the normal rocket prefab
    [SerializeField] private Transform crossRocketTransform; // reference to the cross rocket prefab

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (normalRocketTransform.gameObject.activeSelf)
        {
            normalRocketTransform.gameObject.SetActive(false);
            crossRocketTransform.gameObject.SetActive(true);
        }
        else
        {
            normalRocketTransform.gameObject.SetActive(true);
            crossRocketTransform.gameObject.SetActive(false);
        }
    }


}
