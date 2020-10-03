using UnityEngine;
using System;

public class InputLoopCounter : MonoBehaviour
{

    private float degreesCounter;
    private Vector2 screenCenter;
    private Vector2 previousMousePosition;
    private bool circleStarted;
    private float circleStartTime;

    public static event Action<float> onCircleCompleted;

    private void Start()
    {
        screenCenter = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
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
            float angle = Vector2.SignedAngle(currentMousePosition - screenCenter, previousMousePosition - screenCenter);

            if (angle < 0)
            {
                degreesCounter = 0;
                circleStarted = false;
            }
            else
            {
                if (!circleStarted)
                {
                    circleStarted = true;
                    circleStartTime = Time.time;
                }

                degreesCounter += angle;

                if (degreesCounter >= 360.0f)
                {
                    circleStarted = false;
                    degreesCounter -= 360.0f;
                    float circleDuration = Time.time - circleStartTime;

                    if (onCircleCompleted != null)
                    {
                        onCircleCompleted(circleDuration);
                    }
                }
            }

            previousMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            degreesCounter = 0;
            circleStarted = false;
        }
    }
}
