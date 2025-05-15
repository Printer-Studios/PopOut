using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shoot1 : MonoBehaviour
{
    public AbsorbBehaviour1 absorb;
    public MovementBehaviour movement;
    public GameObject waterParticle;
    public float shootForce;
    public Transform waterPosition;
    public StatePopeyeHandler state;
    [SerializeField] public InputActionReference shoot;
    //public Slider sliderShot;
    //public float sliderSpeed;

    // Update is called once per frame
    void Start()
    {
        //sliderShot.gameObject.SetActive(false);
    }
    void Update()
    {
        if (shoot.action.WasPressedThisFrame() && !shoot.action.WasReleasedThisFrame())
        {
            if (absorb.currentAmmo > 0)
            {
                GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
                newWater.GetComponent<Rigidbody2D>().AddForce(shootForce * movement.direction);
                absorb.currentAmmo--;
            }
        }
        //ShootFunction();
        Debug.Log(state.currentState);
    }
    void ShootFunction()
    {
        //if (shoot.action.WasPressedThisFrame() && !shoot.action.WasReleasedThisFrame())
        //{
        //    if (absorb.currentAmmo > 0 && state.currentState != StatePopeyeHandler.States.big)
        //    {
        //        GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
        //        newWater.GetComponent<Rigidbody2D>().AddForce(shootForce * movement.direction);
        //        absorb.currentAmmo--;
        //    }
        //    else
        //    {

        //    }
        //}

    }
    //private void ChargeBar()
    //{
    //    sliderShot.gameObject.SetActive(true);
    //    sliderShot.value += (sliderSpeed * Time.deltaTime);
    //}
}
