using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public MovementBehaviour playerMovement;
    private PlatformEffector2D effector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.movementDown.action.IsInProgress() && playerMovement.jump.action.IsInProgress())
        {
            effector.useColliderMask = true;
        }
        else
        {
            effector.useColliderMask = false;
        }
    }
}
