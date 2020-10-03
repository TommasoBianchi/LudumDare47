using UnityEngine;

public class Hamster : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float animationSpeedNormalizationFactor = 100;

    private void Start()
    {
        WheelMover.onWheelVelocityUpdate += (speed) =>
        {
            animator.SetFloat("Speed", speed / animationSpeedNormalizationFactor);
        };
    }
}