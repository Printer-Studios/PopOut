using Unity.VisualScripting;
using UnityEngine;

public class FloatableObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public float underWaterFloatability;
    public GameObject parentObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            rb.AddForce(Vector2.up * underWaterFloatability, ForceMode2D.Impulse);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            rb.AddForce(Vector2.up * underWaterFloatability * 10, ForceMode2D.Force);
        }
    }
}
