using System.Runtime.InteropServices;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    private MovementBehaviour movementBehaviour;
    public float lerpVariable;
    private float desiredZ = -10;
    private float offset;
    private float timeOfPressing;
    public Camera mainCamera;

    public Transform xLimit1, xLimit2;
    void Start()
    {
        movementBehaviour = player.GetComponent<MovementBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movementBehaviour.direction == Vector2.right && movementBehaviour.movementLeft.action.IsInProgress())
        {
            offset = -2f;       
        }
        if (movementBehaviour.direction == Vector2.left && movementBehaviour.movementRight.action.IsInProgress())
        {
            offset = 2f;
        }

/*        if (Time.time - timeOfPressing > 3)
        {
            offset = 0f;
            Debug.LogWarning("3 segondos");
            timeOfPressing = 0;
        }

        if(movementBehaviour.movementRight.action.WasReleasedThisFrame() || movementBehaviour.movementLeft.action.WasReleasedThisFrame())
        {
            timeOfPressing = Time.time;
        }*/

        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offset, player.transform.position.y, player.transform.position.z), lerpVariable);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xLimit1.position.x, xLimit2.position.x), 0, desiredZ);
    }

}
