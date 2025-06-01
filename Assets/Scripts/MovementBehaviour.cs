using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class MovementBehaviour : MonoBehaviour
{
    public static float speed;
    private float timePressed, minSpeed;
    public float jumpForce, acceleration, maxSpeed, swimmingSpeed;
    public float sliderSpeed;
    internal bool isGrounded, isLockedIn;
    public Rigidbody2D rb;
    public Transform p1, p2;
    public LayerMask floorLayer, platformLayer;
    public Slider sliderJump;
    public float jumpDelay;
    [SerializeField] public InputActionReference movementLeft;
    [SerializeField] public InputActionReference movementRight;
    [SerializeField] public InputActionReference movementUp;
    [SerializeField] public InputActionReference movementDown;
    [SerializeField] public InputActionReference jump;
    [SerializeField] public Vector2 direction;
    public WaterInteraction waterInt;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderJump.gameObject.SetActive(false);
        isLockedIn = false;
        minSpeed = -maxSpeed;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        speed = WaterInteraction.speed;
        JumpControl();

        if(!isGrounded && sliderJump.gameObject.activeSelf)
        {
            sliderJump.gameObject.SetActive(false);
            sliderJump.value = 0;
        }

        if(isGrounded && !sliderJump.gameObject.activeSelf)
        {
            isLockedIn = false;
        }

        //isGrounded = true; //Debug ONLY
    }

    private void JumpControl()
    {
        if (jump.action.WasPerformedThisFrame())
        {
            timePressed = Time.time;
        }

        if (jump.action.IsInProgress() && isGrounded && (Time.time - timePressed > jumpDelay))
        {
            ChargeBar();
        }
        if (jump.action.WasReleasedThisFrame() && isGrounded)
        {
            Jump();
        }

        if (Time.time - timePressed > 3.5 && isGrounded && jump.action.IsInProgress())
        {
            sliderJump.gameObject.SetActive(false);
            isLockedIn = false;
            sliderJump.value = 0f;
            Jump(true);
        }
    }

    private void Movement()
    {
        isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer) || Physics2D.OverlapArea(p1.position, p2.position, platformLayer);

        if (movementRight.action.IsInProgress() && !isLockedIn)
        {
            direction = Vector2.right;
            rb.AddForce(direction * acceleration);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            sliderJump.direction = Slider.Direction.LeftToRight;
        }
        if (movementLeft.action.IsInProgress() && !isLockedIn)
        {
            direction = Vector2.left;
            rb.AddForce(direction * acceleration);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            sliderJump.direction = Slider.Direction.RightToLeft;
        }
        if (movementUp.action.IsInProgress() && waterInt.isTouchingWater)
        {
            rb.AddForce(Vector2.up * swimmingSpeed);
        }

        if(movementDown.action.IsInProgress() && jump.action.IsInProgress())
        {
            isGrounded = false;
        }

        rb.linearVelocityX = Math.Clamp(rb.linearVelocityX, minSpeed, maxSpeed);
    }

    private void ChargeBar()
    {
        sliderJump.gameObject.SetActive(true);
        isLockedIn = true;
        sliderJump.value += (sliderSpeed * Time.deltaTime);
    }
    public void Jump(bool maxJump = false)
    {
        float jumpValue;
        if (maxJump) jumpValue = sliderJump.maxValue;
        else jumpValue = sliderJump.value;
        
        //rb.AddRelativeForceY(jumpForce * jumpValue, ForceMode2D.Impulse);

        rb.AddForce(jumpForce * jumpValue * Vector2.up, ForceMode2D.Impulse);
        sliderJump.gameObject.SetActive(false);
        sliderJump.value = 0f;
        isLockedIn = false;
        timePressed = Time.time;
    }

    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<EnemyHitHandler>() != null)
        {
            EnemyHitHandler hitHandler = col.gameObject.GetComponent<EnemyHitHandler>();
            EnemyShotHandler shotHandler = col.gameObject.GetComponent<EnemyShotHandler>();
            for (int i = 0; i < hitHandler.hitTypes.Length; i++)
            {
                if (shotHandler.isWeak && hitHandler.hitTypes[i] == EnemyHitHandler.Hit.Jump && GetComponent<Collider2D>().IsTouching(hitHandler.weakspot))
                //The enemy is upside down, can be killed by jumping, and it's weakspot is touching the player
                {
                    if (!jump.action.IsPressed())
                    {
                        sliderJump.value = sliderJump.minValue;
                    }
                    else
                    {
                        sliderJump.value = sliderJump.maxValue;
                    }
                    rb.linearVelocityY = 0f;
                    Jump();
                    hitHandler.StartCoroutine("TimerDeath");
                }
            }
        }

        if (col.gameObject.GetComponent<JellyFishMovement>() != null)
        {
            JellyFishMovement jellyfishMov = col.gameObject.GetComponent<JellyFishMovement>();
            if (GetComponent<Collider2D>().IsTouching(jellyfishMov.jumpingCollision)) //if player is touching the jellyfish too --> Can jump
            {
                //if the jump button isn't pressed, it jumps the max jump height.
                if (!jump.action.IsPressed())
                {
                    sliderJump.value = sliderJump.maxValue * 0.75f;
                }
                //if the jump button is pressed, it jumps the max jump height * 1.25.
                else
                {
                    sliderJump.value = sliderJump.maxValue * 1.25f;
                }
                rb.linearVelocityY = 0f;
                Jump();
            }
        }
    }
}
