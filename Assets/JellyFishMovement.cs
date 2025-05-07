using UnityEngine;

public class JellyFishMovement : MonoBehaviour
{
    public float distance;
    public float speed;
    public Collider2D jumpingCollision;
    
    void Update()
    {
        HoverMovement();
    }

    public void HoverMovement()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Sin(distance * Time.time)* speed);
    }


}
