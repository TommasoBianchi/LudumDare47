using UnityEngine;
using UnityEditor;

public class WheelMover : MonoBehaviour
{

    [SerializeField, Range(0.0f, 1.0f)]
    private float circleTimeTresholdPerc = 0.9f;
    [SerializeField]
    private float angularVelocityMultiplier = 5;
    [SerializeField]
    private float angularAcceleration = 1;
    [SerializeField]
    public Transform wheel;

    private float currentAngularVelocity;
    private float targetAngularVelocity;
    private float circleTimeTreshold;
    private float circleCompletionLimitTime;

    private void Start()
    {
        circleTimeTreshold = Mathf.Clamp01(circleTimeTreshold);
        circleTimeTreshold = Mathf.Infinity;
        currentAngularVelocity = 0.0f;
        circleCompletionLimitTime = Mathf.Infinity;

        InputLoopCounter.onCircleCompleted += (circleTime) =>
        {
            // Detect circle time treshold was exceeded
            if (circleTime > circleTimeTreshold)
            {
                OnCircleTimeTresholdExceeded();
            }

            // Update circle time treshold
            circleTimeTreshold = Mathf.Min(circleTimeTreshold, circleTime / circleTimeTresholdPerc);

            // Update wheel target angular velocity
            targetAngularVelocity = Mathf.Max(targetAngularVelocity, angularVelocityMultiplier / circleTime);

            // Update next circle completion limit time
            circleCompletionLimitTime = Time.time + circleTimeTreshold;
        };
    }

    private void Update()
    {
        if (Time.time > circleCompletionLimitTime)
        {
            OnCircleTimeTresholdExceeded();
            return;
        }

        currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, targetAngularVelocity, Time.deltaTime * angularAcceleration);
        transform.Rotate(0, 0, -currentAngularVelocity * Time.deltaTime);
    }

    private void OnCircleTimeTresholdExceeded()
    {
        Debug.LogError("Game over");
        EditorApplication.isPlaying = false;
    }
}