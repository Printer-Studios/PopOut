using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    [SerializeField] public InputActionReference movement;
    private Rigidbody2D rb2D;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderJump.gameObject.SetActive(false);
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = WaterInteraction.speed;
        rb2D.linearVelocity = movement.action.ReadValue<Vector2>() * speed;
        //Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer);

        if (horizontal > 0 && !isLockedIn)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (horizontal < 0 && !isLockedIn)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timePressed = Time.time;
            while (Input.GetKey(KeyCode.Space))
            {
                sliderJump.gameObject.SetActive(true);
                isLockedIn = true;
                sliderJump.value += (sliderSpeed * Time.deltaTime);

            }
            /*if (Input.GetKey(KeyCode.Space) && isGrounded && (Time.time - timePressed > jumpDelay))
            {
                
            }*/
            rb.AddForce(Vector2.up * jumpForce * sliderJump.value);
            sliderJump.gameObject.SetActive(false);
            sliderJump.value = 0f;
            isLockedIn = false;
        }

        
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            
        }
    }

}
