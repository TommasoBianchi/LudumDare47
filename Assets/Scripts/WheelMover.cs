using UnityEngine;
using System;

public class WheelMover : MonoBehaviour
{

    [SerializeField, Range(0.0f, 1.0f)]
    private float circleTimeTresholdPerc = 0.9f;
    [SerializeField]
    private float angularVelocityMultiplier = 5;
    [SerializeField]
    private float angularAcceleration = 1;
    [SerializeField]
    private float targetVelocityTimeExponent;
    [SerializeField]
    private Transform wheel;
    [SerializeField]
    private float gameOverWaitTime = 3.0f;

    private float currentAngularVelocity;
    private float targetAngularVelocity;
    private float circleTimeTreshold;
    private float circleCompletionLimitTime;
    private bool gameOverDetected;

    public static event Action<float> onCircleTimeTresholdUpdate;
    public static event Action<float> onWheelVelocityUpdate;
    public static event Action onGameOver;

    private void Start()
    {
        circleTimeTreshold = Mathf.Clamp01(circleTimeTreshold);
        circleTimeTreshold = Mathf.Infinity;
        currentAngularVelocity = 0.0f;
        targetAngularVelocity = 0.0f;
        circleCompletionLimitTime = Mathf.Infinity;
        gameOverDetected = false;

        InputLoopCounter.onCircleCompleted += (circleTime) =>
        {
            // Detect circle time treshold was exceeded
            if (circleTime > circleTimeTreshold)
            {
                OnCircleTimeTresholdExceeded();
            }

            // Update circle time treshold
            circleTimeTreshold = Mathf.Min(circleTimeTreshold, circleTime / circleTimeTresholdPerc);

            onCircleTimeTresholdUpdate?.Invoke(circleTimeTreshold);

            // Update wheel target angular velocity
            targetAngularVelocity = Mathf.Max(targetAngularVelocity, angularVelocityMultiplier / Mathf.Pow(circleTime, targetVelocityTimeExponent));

            // Update next circle completion limit time
            circleCompletionLimitTime = Time.time + circleTimeTreshold;
        };
    }

    private void Update()
    {
        if (Time.time > circleCompletionLimitTime)
        {
            OnCircleTimeTresholdExceeded();
        }

        if (gameOverDetected)
        {
            // Slow down after game over
            targetAngularVelocity = Mathf.Lerp(targetAngularVelocity, 0.0f, Time.deltaTime * angularAcceleration);
        }

        currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, targetAngularVelocity, Time.deltaTime * angularAcceleration);

        onWheelVelocityUpdate?.Invoke(currentAngularVelocity);

        wheel.Rotate(0, 0, -currentAngularVelocity * Time.deltaTime);
    }

    private void OnCircleTimeTresholdExceeded()
    {
        if (gameOverDetected) return;

        gameOverDetected = true;

        onGameOver?.Invoke();

        GameManager.AddFuture(gameOverWaitTime, () => GameManager.GameOver());
    }

    private void OnDestroy()
    {
        onCircleTimeTresholdUpdate = null;
        onWheelVelocityUpdate = null;
        onGameOver = null;
    }
}