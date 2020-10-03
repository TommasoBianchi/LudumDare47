using UnityEngine;

public class Hamster : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float animationSpeedNormalizationFactor = 100;
    [SerializeField]
    private ParticleSystemRenderer explosionPrefab;

    private void Start()
    {
        WheelMover.onWheelVelocityUpdate += (speed) =>
        {
            animator.SetFloat("Speed", speed / animationSpeedNormalizationFactor);
        };

        WheelMover.onGameOver += () =>
        {
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            Destroy(gameObject);
        };
    }
}