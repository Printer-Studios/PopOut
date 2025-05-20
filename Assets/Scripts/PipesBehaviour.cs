using UnityEngine;
using UnityEngine.UIElements;

public class PipesBehaviour : MonoBehaviour
{
    public GameObject pos1, pos2;
    public enum Directions { Nothing, North, South, West, East }
    public Directions directionToSpawn1, directionToSpawn2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GetHit");
        if (col.gameObject.tag == "Water" || col.gameObject.tag == "Ice" || col.gameObject.tag == "Steam")
        {
            if (col.IsTouching(pos1.GetComponent<Collider2D>()))
            {
                SpawnObject(col.gameObject, pos1.transform, directionToSpawn1);
            }
            if (col.IsTouching(pos2.GetComponent<Collider2D>()))
            {
                SpawnObject(col.gameObject, pos2.transform, directionToSpawn2);
            }
        }
    }

    void SpawnObject(GameObject toSpawn,Transform pos, Directions exitDirection)
    {
        GameObject obj = Instantiate(toSpawn, pos.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(SetVelocity(toSpawn, exitDirection));
        Destroy(toSpawn);
    }

    Vector2 GetVelocity(GameObject obj)
    {
        return new Vector2(obj.GetComponent<Rigidbody2D>().linearVelocityX, obj.GetComponent<Rigidbody2D>().linearVelocityY);
    }

    Vector2 SetVelocity(GameObject obj, Directions exitDirection)
    {
        Vector2 currentVelocity = GetVelocity(obj);

        switch (exitDirection)
        {
            case Directions.North:
                if (currentVelocity.y < 0) { currentVelocity.y *= -1; }
                break;
            case Directions.South:
                if (currentVelocity.y > 0) { currentVelocity.y *= -1; }
                break;
            case Directions.West:
                if (currentVelocity.x > 0) { currentVelocity.x *= -1; }
                break;
            case Directions.East:
                if (currentVelocity.x < 0) { currentVelocity.x *= -1; }
                break;
            default:
                break;
        }

        return currentVelocity;
    }
}
