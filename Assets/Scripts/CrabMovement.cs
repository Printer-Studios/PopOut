using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    public int health;
    public float speed;
    public bool startRight; //Bool to decide starting direction
    private Collider2D collider;
    public CrabHitHandler hitHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitHandler.canBeHit)
        {
            if (startRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Floor" && col.gameObject.tag != "Water")
        {
            startRight = !startRight;
        }
    }
}
