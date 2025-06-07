using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    public int health;
    public float speed;
    public bool startRight; //Bool to decide starting direction
    private Collider2D colliderBody;
    public Transform groundDetection;
    public EnemyShotHandler shotHandler;
    public LayerMask floorLayer;
    public bool falls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliderBody = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shotHandler.isWeak)
        {
            if (startRight)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (!falls)
            {
                RaycastHit2D hit = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f, floorLayer);
                Debug.DrawRay(groundDetection.position, Vector2.down);
                if (hit.collider == false)
                {
                    startRight = !startRight;
                }
            }

            if (startRight)
            {
                RaycastHit2D hitWall = Physics2D.Raycast(groundDetection.position, Vector2.right, 0.3f, floorLayer);
                if (hitWall.collider == true)
                {
                    startRight = !startRight;
                }
            }
            else
            {
                RaycastHit2D hitWall = Physics2D.Raycast(groundDetection.position, Vector2.left, 0.3f, floorLayer);
                if (hitWall.collider == true)
                {
                    startRight = !startRight;
                }
            }


            
        }
    }

    //public void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.tag != "Floor" && col.gameObject.tag != "Water")
    //    {
    //        startRight = !startRight;
    //    }
    //}
}
