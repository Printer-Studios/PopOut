using UnityEngine;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{
    public AbsorbBehaviour absorb;
    public GameObject waterParticle;
    public float shootForce;
    public Transform waterPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (absorb.currentAmmo > 0)
            {
                GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
                newWater.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce);
                absorb.currentAmmo--;
            }
        }
    }
}
