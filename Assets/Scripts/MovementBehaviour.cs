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
    }

    private void Movement()
    {
        isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer);

        if (movementRight.action.IsInProgress() && !isLockedIn)
        {
            direction = Vector2.right;
            transform.Translate(direction * speed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (movementLeft.action.IsInProgress() && !isLockedIn)
        {
            direction = Vector2.left;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (jump.action.WasPerformedThisFrame())
        {
            timePressed = Time.time;
        }

        if(jump.action.IsInProgress() && isGrounded && (Time.time - timePressed > jumpDelay))
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
    private void Jump(bool maxJump = false)
    {
        float jumpValue;
        if (maxJump) jumpValue = sliderJump.maxValue;
        else jumpValue = sliderJump.value;

        rb.AddForce(jumpForce * jumpValue * Vector2.up);
        sliderJump.gameObject.SetActive(false);
        sliderJump.value = 0f;
        isLockedIn = false;
        timePressed = Time.time;
    }


}
