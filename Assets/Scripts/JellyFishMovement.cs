using UnityEngine;

public class JellyFishMovement : MonoBehaviour
{
    public float speed;
    public float distance;
    public Collider2D jumpingCollision;
    
    void Update()
    {
        HoverMovement();
    }

    public void HoverMovement()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (Mathf.Sin(distance * Time.realtimeSinceStartup) * speed * Time.deltaTime));
    }


}
