using UnityEngine;

public class InstructionOverlayPanel : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.SkipTutorial();
            Destroy(gameObject);
        }
    }
}