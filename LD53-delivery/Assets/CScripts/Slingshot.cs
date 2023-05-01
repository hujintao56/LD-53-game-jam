using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Transform launchTransform; // The Transform component of the launch object
    [SerializeField] private Transform barrelTransform; // The Transform component of the barrel
    [SerializeField] private Transform rocketTransform; // The Transform component of the rocket
    [SerializeField] private float minLaunchForce = 0.1f; // The minimum launch force
    [SerializeField] private float maxLaunchForce = 1f; // The maximum launch force
    [SerializeField] private float maxChargeTime = 2f; // The maximum charge time
    [SerializeField] private float launchAngleRange = 360f; // The range of launch angles
    [SerializeField] private LineRenderer aimingLine; // The LineRenderer component of the aiming line
    [SerializeField] private float reloadTime = 3f; // The time it takes to reload the slingshot

    private float launchSpeedMultiplier = 0.1f; // Change this value to adjust the launch speed
    private Vector3 rocketOriginalPosition;   // The original position of the rocket
    private Quaternion rocketOriginalRotation; // The original rotation of the rocket
    private Vector3 barrelOriginalPosition;   // The original position of the barrel
    private Quaternion barrelOriginalRotation; // The original rotation of the barrel
    private Quaternion launchOriginalRotation; // The original rotation of the launch

    private Vector3 barrelOriginalAngles;   // The original rotation of the barrel
    private Vector3 mouseDownPosition;        // The screen position where the mouse is clicked down
    private Vector3 direction;
    private float chargeTime;                 // The time that the slingshot is charged up
    private float launchForce;                // The force that the rocket will be launched with
    private bool isAiming;                    // Whether the slingshot is in aiming state

    public float setGravity = 1.0f;

    private void Start()
    {
        rocketOriginalPosition = rocketTransform.position;
        rocketOriginalRotation = rocketTransform.rotation;
        barrelOriginalPosition = barrelTransform.position;
        barrelOriginalRotation = barrelTransform.rotation;
        launchOriginalRotation = launchTransform.rotation;

        barrelOriginalAngles = barrelTransform.localEulerAngles;
        aimingLine.enabled = false;

        /* Set the launch object's position to the left bottom corner of the barrel and rocket
        Vector3 launchPosition = new Vector3(
            barrelTransform.position.x - (barrelTransform.lossyScale.x / 2f),
            barrelTransform.position.y - (barrelTransform.lossyScale.y / 2f),
            0f);
        launchTransform.position = launchPosition;
        */
    }

    private void Update()
    {
        if (!isAiming)
            return;

        Aim();
        Charge();
    }
    void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter()");
        Debug.Log("Mouse enter");
    }

    void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit()");
        Debug.Log("Mouse exit");
    }

    void OnMouseOver()
    {
        //print("Slingshot:OnMouseEnter()");
        Aim();
    }

    private void OnMouseDown()
    {
        isAiming = true;
        mouseDownPosition = Input.mousePosition;
        launchForce = minLaunchForce;
        aimingLine.enabled = true;
    }

    private void OnMouseUp()
    {
        isAiming = false;
        Fire();
        aimingLine.enabled = false;
    }

    private void Aim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        direction = (targetPos - launchTransform.position).normalized;

        // Rotate the direction by 45 degrees to account for the angle between the launch and barrel
        // float angle = 45f * Mathf.Deg2Rad;
        // direction = Quaternion.Euler(0f, 0f, angle) * direction;

        // Calculate the rotation angle based on mouse position
        // float angle = Mathf.Clamp(Vector3.Angle(direction, Vector3.up), 0f, launchAngleRange);
        Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Apply the new rotation to launchTransform without changing position
        launchTransform.rotation = Quaternion.Euler(launchOriginalRotation.eulerAngles.x, launchOriginalRotation.eulerAngles.y, newRotation.eulerAngles.z);

        // Apply the new angle to barrelTransform
        // barrelTransform.localEulerAngles = barrelOriginalAngles + angle * Vector3.forward;


        // Calculate the trajectory arc based on launch position, launch direction, and launch force
        Vector3[] linePositions = CalculateArcPoints(launchTransform.position, direction, launchForce);
        aimingLine.positionCount = linePositions.Length;
        aimingLine.SetPositions(linePositions);

    }

    private Vector3[] CalculateArcPoints(Vector3 origin, Vector3 direction, float force)
    {
        int numSegments = 20;
        float timeStep = 0.1f;
        Vector3[] arcPoints = new Vector3[numSegments + 1];

        Vector3 velocity = direction.normalized * force;
        float gravity = Physics.gravity.magnitude;

        for (int i = 0; i <= numSegments; i++)
        {
            float t = timeStep * i;
            arcPoints[i] = origin + (velocity * t) + (0.5f * gravity * t * t * Vector3.down);
        }

        return arcPoints;
    }


    private void Charge()
    {
        if (Input.mousePosition != mouseDownPosition)
            return;

        chargeTime += Time.deltaTime;
        launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, chargeTime / maxChargeTime);
        Debug.Log("Charge Time: " + chargeTime);
        Debug.Log("Launch Force: " + launchForce);
    }

    private void Fire()
    {
        // Vector3 direction = (Input.mousePosition - mouseDownPosition).normalized;
        // launchTransform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Vector3 direction = Quaternion.Euler(barrelTransform.localEulerAngles) * Vector3.up;
        Debug.Log("Launch Direction: " + direction);
        Debug.Log("Launch Magnitude: " + launchForce);
        rocketTransform.GetComponent<Rigidbody2D>().AddForce(launchSpeedMultiplier * launchForce * direction, ForceMode2D.Impulse);
        rocketTransform.GetComponent<Rigidbody2D>().gravityScale = setGravity;
        Reset();
    }

    /* private IEnumerator SpawnRocket()
    {
        yield return new WaitForSeconds(reloadTime);
        Instantiate(rocketTransform, launchTransform.position, Quaternion.identity);
    }

    private void Reset()
    {
        barrelTransform.localEulerAngles = barrelOriginalRotation;
        StartCoroutine(SpawnRocket());
    }*/

    private void Reset()
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(reloadTime);
        //rocketTransform.position = launchTransform.position;
        //rocketTransform.rotation = Quaternion.identity;
        rocketTransform.position = rocketOriginalPosition;
        rocketTransform.rotation = rocketOriginalRotation;
        rocketTransform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        rocketTransform.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        rocketTransform.GetComponent<Rigidbody2D>().gravityScale = 0;
        // barrelTransform.localEulerAngles = barrelOriginalRotation;
        // barrelTransform.position = launchTransform.position;
        // barrelTransform.rotation = Quaternion.identity;
        barrelTransform.position = barrelOriginalPosition;
        barrelTransform.rotation = barrelOriginalRotation;
    }




}