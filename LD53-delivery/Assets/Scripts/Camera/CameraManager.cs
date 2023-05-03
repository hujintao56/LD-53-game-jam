using System;
using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        private GameManager gameManager;
        private UnityEngine.Camera mainCam;
        public CinemachineVirtualCamera cam1; //Cinemachine camera
        
        public GameObject camFollowObject; //Empty gameObject that cinemachine camera will follow
        private Vector3 camFollowPos; 
        
        [SerializeField] private float boundOffset = -1f;
        public GameObject packageFollowObject;
        public float edgeSize = 30f;
        public float moveSpeed = 5f;
        [SerializeField] private bool followingPackage;
            
        void Start()
        {
            gameManager = GameManager.Instance;
            mainCam = FindFirstObjectByType<UnityEngine.Camera>();
            cam1 = GetComponent<CinemachineVirtualCamera>();
            
            followingPackage = false;
            camFollowPos = camFollowObject.transform.position = cam1.transform.position;

            FocusOnLauncher();
        }

        public void FollowPackage(GameObject package)
        {
            followingPackage = true;
            cam1.m_LookAt = null;
            cam1.m_Follow = package.transform;
        }

        void Update()
        {
            if (!followingPackage)
            {
                cam1.m_Follow = cam1.m_LookAt = camFollowObject.transform;
                Vector3 mainCamPos = mainCam.transform.position;
                //right,left,up,down
                if (Input.mousePosition.x > Screen.width - edgeSize && camFollowPos.x < mainCamPos.x - boundOffset)
                {
                    camFollowPos.x += moveSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.x < edgeSize && camFollowPos.x > mainCamPos.x + boundOffset)
                {
                    camFollowPos.x -= moveSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y > Screen.height - edgeSize && camFollowPos.y < mainCamPos.y - boundOffset)
                {
                    camFollowPos.y += moveSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y < edgeSize && camFollowPos.y > mainCamPos.y + boundOffset)
                {
                    camFollowPos.y -= moveSpeed * Time.deltaTime;
                }

                camFollowObject.transform.position = camFollowPos;

                if (Input.GetMouseButtonDown(1))
                {
                    FocusOnLauncher();
                }
            }
        }

        public void FocusOnLauncher()
        {
            followingPackage = false;
            camFollowPos = gameManager.currentLauncher.transform.position;
        }
    }
}
