using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public AbsorbBehaviour absorb;
    public MovementBehaviour movement;
    public GameObject waterParticle;
    public float shootForce;
    public Transform waterPosition;

    // Update is called once per frame
    void Update()
    {
        var shoot = Input.GetButtonDown("Shoot");

        if (shoot)
        {
            if (absorb.currentAmmo > 0)
            {
                GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
                if (movement.isDirectionRight)
                {
                    newWater.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce);
                }
                else
                {
                    newWater.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootForce);
                }
                absorb.currentAmmo--;
            }
        }
    }
}
