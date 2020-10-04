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
        WheelMover.onWheelVelocityUpdate += UpdateAnimationSpeed;

        WheelMover.onGameOver += () =>
        {
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            SoundManager.PlayExplosion();
            SoundManager.StopMusic();
            WheelMover.onWheelVelocityUpdate -= UpdateAnimationSpeed;
            Destroy(gameObject);
        };
    }

    private void UpdateAnimationSpeed(float speed)
    {
        animator.SetFloat("Speed", speed / animationSpeedNormalizationFactor);
    }
}