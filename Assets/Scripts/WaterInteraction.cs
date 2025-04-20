using Unity.VisualScripting;
using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxSpeed = 3f;
    public static float speed;

    void Start()
    {
        speed = maxSpeed;
    }

    // Update is called once per frame

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
 }
    

