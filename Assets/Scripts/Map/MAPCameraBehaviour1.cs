using UnityEngine;

public class MAPCameraBehaviour : MonoBehaviour //MAP CAMERA
{
    public GameObject player;
    private MovementBehaviour movementBehaviour;
    public float lerpVariable;
    private float desiredZ = -10;
    private float desiredY = 0;
    private float timeOfPressing;

    public Transform xLimit1, xLimit2;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), lerpVariable * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xLimit1.position.x, xLimit2.position.x), desiredY, desiredZ);
    }

}
