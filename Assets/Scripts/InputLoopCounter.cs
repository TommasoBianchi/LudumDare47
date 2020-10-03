using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class InputLoopCounter : MonoBehaviour
{

    private float degreesCounter;
    private Vector2 circleCenter;
    private Vector2 previousMousePosition;
    private bool circleStarted;
    private float circleStartTime;
    private List<Vector2> lastCirclePoints;

    public static event Action<float> onCircleCompleted;

    private void Start()
    {
        UpdateCircleCenter(new Vector2(Screen.width / 2.0f, Screen.height / 2.0f), averageWithPrevious: false);
        lastCirclePoints = new List<Vector2>();
        circleStarted = false;
    }

    private void Update()
    {
        Vector2 currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            float angle = Vector2.SignedAngle(currentMousePosition - circleCenter, previousMousePosition - circleCenter);
            lastCirclePoints.Add(currentMousePosition);

            if (angle < 0)
            {
                StopCircle();
            }
            else
            {
                if (!circleStarted)
                {
                    StartCircle();
                }

                degreesCounter += angle;

                if (degreesCounter >= 360.0f)
                {
                    FinishCircle();
                }
            }

            previousMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCircle();
        }
    }

    private void StartCircle()
    {
        circleStarted = true;
        circleStartTime = Time.time;
    }

    private void FinishCircle()
    {
        circleStarted = false;
        degreesCounter -= 360.0f;
        float circleDuration = Time.time - circleStartTime;

        if (onCircleCompleted != null)
        {
            onCircleCompleted(circleDuration);
        }

        // Compute new circle center
        Vector2 lastCirclePointsSum = lastCirclePoints.Aggregate<Vector2, Vector2>(Vector2.zero, (a, b) => a + b);
        Vector2 lastCirclePointsCenter = lastCirclePointsSum / Mathf.Max(1, lastCirclePoints.Count);
        UpdateCircleCenter(lastCirclePointsCenter);
        lastCirclePoints.Clear();
    }

    private void StopCircle()
    {
        degreesCounter = 0;
        circleStarted = false;
        lastCirclePoints.Clear();
    }

    private void UpdateCircleCenter(Vector2 newCircleCenter, bool averageWithPrevious = true)
    {
        if (averageWithPrevious)
        {
            newCircleCenter = (circleCenter + newCircleCenter) / 2.0f;
        }

        circleCenter = newCircleCenter;
        transform.position = Camera.main.ScreenToWorldPoint((Vector3)circleCenter + Vector3.forward * 10);
    }
}
