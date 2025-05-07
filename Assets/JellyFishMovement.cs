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
        transform.position = new Vector2(transform.position.x, Mathf.Sin(distance * speed * Time.time));
    }


}
