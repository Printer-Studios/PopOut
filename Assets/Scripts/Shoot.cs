using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{
    public AbsorbBehaviour absorb;
    public MovementBehaviour movement;
    public GameObject waterParticle;
    public float shootForce;
    public Transform waterPosition;
    [SerializeField] public InputActionReference shoot;

    // Update is called once per frame
    void Update()
    {
        if (shoot.action.WasPressedThisFrame() && !shoot.action.WasReleasedThisFrame())
        {
            if (absorb.currentAmmo > 0)
            {
                GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
                newWater.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce * movement.direction);
                absorb.currentAmmo--;
            }
        }
    }
}
