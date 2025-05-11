using UnityEngine;

public class floatingBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    public Rigidbody2D rb;
    public int floatingForce;
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 4)
        {
            Debug.Log("floating");
            rb.AddForce(floatingForce * Vector2.up);
        }
    }
}
