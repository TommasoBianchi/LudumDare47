using UnityEngine;

public class WheelTrail : MonoBehaviour
{

    [SerializeField]
    private Color slowColor;
    [SerializeField]
    private Color fastColor;
    [SerializeField]
    private float fastSpeedTreshold;
    [SerializeField, Range(0.0f, 1.0f)]
    private float alphaMultiplier;

    private TrailRenderer trailRenderer;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();

        WheelMover.onWheelVelocityUpdate += (speed) =>
        {
            float fastness = Mathf.Clamp01(speed / fastSpeedTreshold);

            Gradient gradient = trailRenderer.colorGradient;

            GradientAlphaKey[] alphaKeys = gradient.alphaKeys;
            GradientColorKey[] colorKeys = gradient.colorKeys;

            alphaKeys[0].alpha = fastness * alphaMultiplier;
            colorKeys[0].color = Color.Lerp(slowColor, fastColor, fastness);

            gradient.SetKeys(colorKeys, alphaKeys);

            trailRenderer.colorGradient = gradient;
        };
    }
}