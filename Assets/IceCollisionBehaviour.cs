using UnityEngine;

public class IceCollisionBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private MovementBehaviour _movementBehaviour;
    public float desiredLinearDamping;

    void Start()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "TilemapIce")
        {
            _movementBehaviour.rb.linearDamping = 0;
            _movementBehaviour.maxSpeed += 2;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TilemapIce")
        {
            _movementBehaviour.rb.linearDamping = desiredLinearDamping;
        }
    }
}
