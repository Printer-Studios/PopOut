using UnityEngine;

public class JellyFishMovement : MonoBehaviour
{
    public float speed;
    public float distance;
    public Collider2D jumpingCollision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HoverMovement();
    }

    public void HoverMovement()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Sin(distance * speed * Time.time));
    }
}
