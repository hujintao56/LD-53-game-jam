using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform launchTransform;
    public Transform barrelTransform;
    public float launchForce = 20f;
    public float launchAngleRange = 45f;
    public GameObject rocketPrefab;
    public float reloadTime = 2f;

    private Vector3 barrelOriginalRotation;
    private LineRenderer aimingLine;
    private bool isReloading = false;

    void Start()
    {
        aimingLine = GetComponent<LineRenderer>();
        barrelOriginalRotation = barrelTransform.localEulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            FireRocket();
            StartCoroutine(Reload());
        }
    }

    private void FireRocket()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = targetPos - launchTransform.position;
        float angle = Mathf.Clamp(Vector3.Angle(direction, Vector3.up), 0f, launchAngleRange);
        launchTransform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        barrelTransform.localEulerAngles = barrelOriginalRotation + angle * Vector3.forward;

        GameObject rocket = Instantiate(rocketPrefab, launchTransform.position, launchTransform.rotation);
        Rigidbody rocketRigidbody = rocket.GetComponent<Rigidbody>();
        rocketRigidbody.AddForce(direction.normalized * launchForce, ForceMode.Impulse);

        Vector3[] linePositions = CalculateArcPoints(launchTransform.position, targetPos - launchTransform.position, launchForce);
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

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;

        barrelTransform.localEulerAngles = barrelOriginalRotation;
        Instantiate(rocketPrefab, launchTransform.position, Quaternion.identity);
    }


}
