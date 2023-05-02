using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this script on the objective gameobject, add an indicator sprite object as its child and assign to indicator
// Indicator only works when the objective is invisible from the camera (including Editor Scene View!)
public class IndicatorScript : MonoBehaviour
{
    public GameObject indicator;
    public GameObject cam;
    public GameObject board;
    private Renderer rd;
    private bool isLevelCompleted = false;
    private bool isBoardDismissed = false;

    void Start()
    {
        if (cam == null)
        {
            cam = FindFirstObjectByType<UnityEngine.Camera>().gameObject;
        }
        rd = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!rd.isVisible)
        {
            if (!indicator.activeSelf)
            {
                indicator.SetActive(true);
            }

            Vector3 pos = transform.position;
            Vector2 dir = cam.transform.position - pos;

            RaycastHit2D ray = Physics2D.Raycast(pos, dir);
            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
                float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
                indicator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else
        {
            if (indicator.activeSelf)
            {
                indicator.SetActive(false);
            }
        }

        if (isLevelCompleted && !isBoardDismissed && Input.GetMouseButtonDown(0))
        {
            isBoardDismissed = true;
            board.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Package" && !isLevelCompleted)
        {
            isLevelCompleted = true;
            board.SetActive(true);

            // Move the board to the center of the camera
            Vector3 cameraPosition = cam.transform.position;
            Vector3 boardPosition = board.transform.position;
            boardPosition.x = cameraPosition.x;
            boardPosition.y = cameraPosition.y;
            board.transform.position = boardPosition;

            // Set the sorting order of the board's sprite renderer to a high value
            SpriteRenderer boardSpriteRenderer = board.GetComponent<SpriteRenderer>();
            if (boardSpriteRenderer != null)
            {
                boardSpriteRenderer.sortingOrder = 999;
            }
        }
    }
}