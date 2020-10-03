using UnityEngine;
using UnityEngine.UI;

public class CircleTimeTresholdSlider : MonoBehaviour
{

    private Slider slider;
    private float timeElapsed;
    private float totalTime;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1.0f;
        timeElapsed = 0.0f;
        totalTime = Mathf.Infinity;

        WheelMover.onCircleTimeTresholdUpdate += (circleTimeTreshold) =>
        {
            totalTime = circleTimeTreshold;
            timeElapsed = 0.0f;
            slider.value = 1.0f;
        };
    }

    void Update()
    {
        if (timeElapsed > totalTime || totalTime >= Mathf.Infinity)
        {
            return;
        }

        timeElapsed += Time.deltaTime;

        slider.value = Mathf.Clamp01((totalTime - timeElapsed) / totalTime);
    }
}
