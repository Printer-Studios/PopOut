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
    public Slider sliderShot;
    public float sliderSpeed;
    public float shotDelay;
    float timePressed;
    const float minShootForce = 100;

    // Update is called once per frame
    void Start()
    {
        sliderShot.gameObject.SetActive(false);
    }
    void Update()
    {
        ShootFunction();
    }
    void ShootFunction()
    {
        if (shoot.action.WasPressedThisFrame() && !shoot.action.WasReleasedThisFrame() && state.currentState != StatePopeyeHandler.State.big)
        {
            if (absorb.currentAmmo > 0)
            {
                GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
                newWater.GetComponent<Rigidbody2D>().AddForce(shootForce * movement.direction);
                absorb.currentAmmo--;
            }
        }
        else if(state.currentState == StatePopeyeHandler.State.big)
        {
            if (shoot.action.WasPerformedThisFrame())
            {
                timePressed = Time.time;
            }
            if (shoot.action.IsInProgress() && (Time.time - timePressed > shotDelay))
            {
                ChargeBar();
            }
            if (shoot.action.WasReleasedThisFrame())
            {
                GameObject newWater = Instantiate(waterParticle, new Vector2(waterPosition.position.x, waterPosition.position.y), Quaternion.identity);
                if (sliderShot.value > 0)
                {
                    newWater.GetComponent<Rigidbody2D>().AddForce(minShootForce * sliderShot.value * movement.direction);
                }
                absorb.currentAmmo--;
                sliderShot.value = 0f;
            }
            if (Time.time - timePressed > 3.5)
            {
                sliderShot.gameObject.SetActive(false);
                sliderShot.value = 0f;
            }
        }
    }
    private void ChargeBar()
    {
        sliderShot.gameObject.SetActive(true);
        sliderShot.value += (sliderSpeed * Time.deltaTime);
    }
}
