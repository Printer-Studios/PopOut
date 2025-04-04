using Unity.VisualScripting;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float maxSpeed = 5f;
    float speed;

    void Start()
    {
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move(speed);
    }
    public void OnTriggerStay2D(Collider2D water)
    {
        if (speed > maxSpeed/2f) 
            speed = maxSpeed / 2f;
            Debug.Log("contact with water"+speed);
    }
    public void OnTriggerExit2D(Collider2D water)
    {
        speed = maxSpeed;
        Debug.Log("stopped contact with water");
    }
    void Move(float speed)
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }
    }
}
