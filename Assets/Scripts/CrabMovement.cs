using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    public int health;
    public float speed;
    public bool startRight; //Bool to decide starting direction
    private Collider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
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

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            startRight = !startRight;
        }
    }
}
