using UnityEngine;

public class CatcherScript : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float MovementForce, maxSpeed, detectionRadius;
    private float minSpeed, timer;
    private Vector2 direction;
    private bool isInFloor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minSpeed = -maxSpeed;
        timer = 0;
        isInFloor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.x > transform.position.x)
        {
            direction = Vector2.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            direction = Vector2.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if((target.position - transform.position).magnitude < detectionRadius)
        {
            rb.AddForce(MovementForce * direction);
            rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, minSpeed, maxSpeed);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthBehaviour>().GetHit(collision.gameObject.GetComponent<HealthBehaviour>().currentHealth);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) isInFloor = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        isInFloor = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (timer > 3 && isInFloor)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) Jump();
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Default")) Jump(true);
            timer = 0;
        }
    }

    private void Jump(bool isPlayer = false)
    {
        float jumpForce;
        if (isPlayer) jumpForce = 8f;
        else jumpForce = 6;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
