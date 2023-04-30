using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineVirtualCamera cam1;
        public GameObject camFollowObject;
        private Vector3 camFollowPos;
        public GameObject packageFollowObject;
        public float edgeSize = 30f;
        public float moveSpeed = 1f;
        private bool followingPackage;
            
        void Start()
        {
            cam1 = GetComponent<CinemachineVirtualCamera>();
            followingPackage = false;
            camFollowPos = camFollowObject.transform.position = cam1.transform.position;
        }

        void FollowPackage()
        {
            followingPackage = true;
            cam1.m_LookAt = null;
            cam1.m_Follow = packageFollowObject.transform;
        }

        void Update()
        {
            if (!followingPackage)
            {
                cam1.m_Follow = cam1.m_LookAt = camFollowObject.transform;

                //right,left,up,down
                if (Input.mousePosition.x > Screen.width - edgeSize)
                {
                    camFollowPos.x += moveSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.x < edgeSize)
                {
                    camFollowPos.x -= moveSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y > Screen.height - edgeSize)
                {
                    camFollowPos.y += moveSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y < edgeSize)
                {
                    camFollowPos.y -= moveSpeed * Time.deltaTime;
                }

                camFollowObject.transform.position = camFollowPos;
            }
        }
    }
}
