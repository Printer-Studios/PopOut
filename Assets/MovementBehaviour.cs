using UnityEngine;
using UnityEngine.UI;

public class MovementBehaviour : MonoBehaviour
{
    public int speed, jumpForce;
    public float sliderSpeed;
    bool isGrounded;
    public Rigidbody2D rb;
    public Transform p1, p2;
    public LayerMask floorLayer;
    public Slider sliderJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderJump.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer);

        if (horizontal > 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (horizontal < 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            sliderJump.gameObject.SetActive(true);
            sliderJump.value += (sliderSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * sliderJump.value);
            sliderJump.gameObject.SetActive(false);
            sliderJump.value = 0f;
        }
    }
}
