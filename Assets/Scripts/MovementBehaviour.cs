using UnityEngine;
using UnityEngine.UI;

public class MovementBehaviour : MonoBehaviour
{
    public static float speed;
    private float timePressed;
    public int jumpForce;
    public float sliderSpeed;
    bool isGrounded, isLockedIn;
    public bool isDirectionRight;
    public Rigidbody2D rb;
    public Transform p1, p2;
    public LayerMask floorLayer;
    public Slider sliderJump;
    public float jumpDelay, horizontal;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderJump.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        speed = WaterInteraction.speed;
        Movement();
        FlipDirection();
    }

    private void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");


        isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer);

        if (horizontal > 0 && !isLockedIn)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }

        if (horizontal < 0 && !isLockedIn)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timePressed = Time.time;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded && (Time.time - timePressed > jumpDelay))
        {
            sliderJump.gameObject.SetActive(true);
            isLockedIn = true;
            sliderJump.value += (sliderSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * sliderJump.value);
            sliderJump.gameObject.SetActive(false);
            sliderJump.value = 0f;
            isLockedIn = false;
        }
    }

    public void FlipDirection()
    {
        if (horizontal < 0 && isDirectionRight || horizontal > 0 && !isDirectionRight)
        {
            isDirectionRight = !isDirectionRight;
            transform.Rotate(new Vector3(0, 180, 0));
            sliderJump.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
