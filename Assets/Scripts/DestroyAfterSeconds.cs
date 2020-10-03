using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    [SerializeField]
    private float lifetime;

    private float deathTime;

    private void Start()
    {
        deathTime = Time.time + lifetime;
    }

    private void Update()
    {
        if (Time.time > deathTime)
        {
            Destroy(gameObject);
        }
    }
}