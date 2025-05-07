using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEditor.Experimental.GraphView;

public class MovementBehaviour : MonoBehaviour
{
    public static float speed;
    private float timePressed;
    public int jumpForce;
    public float sliderSpeed;
    bool isGrounded, isLockedIn;
    public Rigidbody2D rb;
    public Transform p1, p2;
    public LayerMask floorLayer;
    public Slider sliderJump;
    public float jumpDelay;
    [SerializeField] public InputActionReference movementLeft;
    [SerializeField] public InputActionReference movementRight;
    [SerializeField] public InputActionReference jump;
    [SerializeField] public Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderJump.gameObject.SetActive(false);
        isLockedIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        speed = WaterInteraction.speed;
        Movement();
        //isGrounded = true; //Debug ONLY
    }

    private void Movement()
    {
        isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer);

        if (movementRight.action.IsInProgress() && !isLockedIn)
        {
            direction = Vector2.right;
            transform.Translate(direction * speed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            sliderJump.direction = Slider.Direction.LeftToRight;
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (movementLeft.action.IsInProgress() && !isLockedIn)
        {
            direction = Vector2.left;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            sliderJump.direction = Slider.Direction.RightToLeft;
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

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


        rb.AddForce(jumpForce * jumpValue * Vector2.up);
        sliderJump.gameObject.SetActive(false);
        sliderJump.value = 0f;
        isLockedIn = false;
        timePressed = Time.time;
    }
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.GetComponent<HitHandler>() != null)
    //    {
    //        Debug.Log("no null");
    //        if (col.gameObject.GetComponent<HitHandler>().canBeHit && GetComponent<PolygonCollider2D>().IsTouching(col.gameObject.GetComponent<HitHandler>().weakspot))
    //        {
    //            Debug.LogWarning("Jumped crap");
    //            if (!jump.action.IsPressed())
    //            {
    //                sliderJump.value = sliderJump.minValue;
    //            }
    //            else
    //            {
    //                sliderJump.value = sliderJump.maxValue;
    //            }
    //            rb.linearVelocityY = 0f;
    //            Jump();
    //            col.gameObject.GetComponent<HitHandler>().Die();
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<EnemyHitHandler>() != null)
        {
            Debug.Log("no null");
            EnemyHitHandler hitHandler = col.gameObject.GetComponent<EnemyHitHandler>();
            EnemyShotHandler shotHandler = col.gameObject.GetComponent<EnemyShotHandler>();
            if (shotHandler.isWeak && hitHandler.hitType == EnemyHitHandler.Hit.Jump && GetComponent<PolygonCollider2D>().IsTouching(hitHandler.weakspot))
            //The enemy is upside down, can be killed by jumping, and it's weakspot is touching the player
            {
                Debug.Log("Jump Kill");
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
                hitHandler.Die();
            }
        }

        if (col.gameObject.GetComponent<JellyFishMovement>() != null)
        {
            JellyFishMovement jellyfishMov = col.gameObject.GetComponent<JellyFishMovement>();
            if (GetComponent<PolygonCollider2D>().IsTouching(jellyfishMov.jumpingCollision)) //if player is touching the jellyfish too --> Can jump
            {
                //if the jump button isn't pressed, it jumps the max jump height.
                if (!jump.action.IsPressed())
                {
                    sliderJump.value = sliderJump.maxValue;
                }
                //if the jump button is pressed, it jumps the max jump height * 1.5.
                else
                {
                    sliderJump.value = sliderJump.maxValue * 1.25f;
                }
                Debug.Log("Jump");
                rb.linearVelocityY = 0f;
                Jump();
            }
        }
    }


}
