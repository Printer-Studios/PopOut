using UnityEngine;

public class CatcherBehaviour : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
    public LayerMask floorLayer;
    public Transform p1, p2;
    bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CatcherMovement();
    }

    void CatcherMovement()
    {
        if (player != null)
        {
            RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
            RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);
            Debug.DrawRay(transform.position, Vector2.left);
            Debug.DrawRay(transform.position, Vector2.right);

            isGrounded = Physics2D.OverlapArea(p1.position, p2.position, floorLayer);

            //if (!(hitWallLeft.collider.IsTouchingLayers(floorLayer) || hitWallRight.collider.IsTouchingLayers(floorLayer)))
            {
                //transform.Translate(Vector2.right * speed * Time.deltaTime);
                rb.AddForce(Vector2.right * Mathf.Sign(player.transform.position.x - gameObject.transform.position.x) * speed, ForceMode2D.Impulse);
                Debug.Log("Moving right");
            }

            if ((hitWallLeft.collider.IsTouchingLayers(floorLayer) || hitWallRight.collider.IsTouchingLayers(floorLayer)) && isGrounded)
            {
                //rb.linearVelocityX = 0;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("Moving Jump");
            }
        }
    }

}
