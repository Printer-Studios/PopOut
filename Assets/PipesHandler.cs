using UnityEngine;
using UnityEngine.UIElements;

public class PipesHandler : MonoBehaviour
{
    public PipesHandler otherEndPipe;
    public GameObject pos;
    public enum Directions { Nothing, North, South, West, East }
    public Directions directionToSpawn;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GetHit");
        if (col.gameObject.tag == "Water" || col.gameObject.tag == "Ice" || col.gameObject.tag == "Steam")
        {
            if (col.IsTouching(pos.GetComponent<Collider2D>()))
            {
                SpawnObject(col.gameObject, otherEndPipe.pos.transform, otherEndPipe.directionToSpawn);
            }
        }
    }

    void SpawnObject(GameObject toSpawn, Transform pos, Directions exitDirection)
    {
        GameObject obj = Instantiate(toSpawn, AdjustPosition(toSpawn, pos, exitDirection), Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(SetVelocity(toSpawn, exitDirection));
        Destroy(toSpawn);
    }

    Vector2 AdjustPosition(GameObject obj, Transform pos, Directions exitDirection)
    {
        Vector2 whereToSpawn;
        whereToSpawn = obj.transform.position;
        //pos = otherEndPipe.pos.transform;
        switch (exitDirection)
        {
            case Directions.North:
                whereToSpawn = new Vector2(whereToSpawn.x, whereToSpawn.y + 1);
                break;
            case Directions.South:
                whereToSpawn = new Vector2(whereToSpawn.x, whereToSpawn.y -1);
                break;
            case Directions.West:
                whereToSpawn = new Vector2(whereToSpawn.x - 1, whereToSpawn.y);
                break;
            case Directions.East:
                whereToSpawn = new Vector2(whereToSpawn.x + 1, whereToSpawn.y);
                break;
            default:
                break;
        }

        return whereToSpawn;
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

        return currentVelocity.normalized;
    }
}
