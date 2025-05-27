using System.Runtime.InteropServices;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    private MovementBehaviour movementBehaviour;
    public float lerpVariable;
    private float desiredZ = -10;
    private float desiredY = 1;
    public float offset;
    public Transform xLimit1, xLimit2;
    void Start()
    {
        movementBehaviour = player.GetComponent<MovementBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movementBehaviour.direction == Vector2.right && movementBehaviour.movementRight.action.IsInProgress())
        {
            offset = Mathf.Abs(offset);       
        }
        if (movementBehaviour.direction == Vector2.left && movementBehaviour.movementLeft.action.IsInProgress())
        {
            offset = -Mathf.Abs(offset);
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offset, player.transform.position.y, player.transform.position.z), lerpVariable * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xLimit1.position.x, xLimit2.position.x), desiredY, desiredZ);
    }

}
