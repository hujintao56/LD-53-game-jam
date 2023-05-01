using System;
using System.Collections;
using System.Collections.Generic;
using Camera;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public GameObject currentLauncher;
    public GameObject currentObjective;
    
    private CameraManager cameraManager;
    public int missilesUsed;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        cameraManager = FindFirstObjectByType<CameraManager>();
        missilesUsed = 0;
    }
    
    void Update()
    {
        
    }

    public void MissileUseCountUp()
    {
        missilesUsed++;
    }

    public void MissileUsedReset()
    {
        missilesUsed = 0;
    }
}
