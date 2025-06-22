using UnityEngine;

public class PipesHandler : MonoBehaviour
{
    public PipesHandler otherEndPipe;
    public GameObject pos;
    public int amountAway = 1;
    public float divideAmountForce;
    public enum Directions { Nothing, North, South, West, East }
    public Directions directionToSpawn;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GetHit");
        if (col.gameObject.tag == "Water" || col.gameObject.tag == "Ice" || col.gameObject.tag == "Steam")
        {
            if (col.IsTouching(pos.GetComponent<Collider2D>()))
            {
                SpawnObject(col.gameObject, otherEndPipe.pos.transform.position, otherEndPipe.directionToSpawn);
            }
        }
    }

    void SpawnObject(GameObject toSpawn, Vector3 pos, Directions exitDirection)
    {
        toSpawn.transform.position = pos;
        //GameObject obj = Instantiate(toSpawn, AdjustPosition(toSpawn, pos, exitDirection), Quaternion.identity);
        //obj.transform.localScale = toSpawn.transform.localScale;
        toSpawn.GetComponent<Rigidbody2D>().AddForce(SetVelocity(toSpawn, exitDirection) - GetVelocity(toSpawn), ForceMode2D.Impulse); // Subtract the current force because if not it would double it
        Debug.Log(SetVelocity(toSpawn, exitDirection));
        Debug.Log("Position Spawned: " + AdjustPosition(toSpawn, pos, exitDirection));
        //Destroy(toSpawn);
    }

    Vector2 AdjustPosition(GameObject toSpawn, Vector3 pos, Directions exitDirection)
    {
        pos = otherEndPipe.pos.transform.position;
        switch (exitDirection)
        {
            case Directions.North:
                toSpawn.transform.position = new Vector2(pos.x, pos.y + amountAway);
                break;
            case Directions.South:
                toSpawn.transform.position = new Vector2(pos.x, pos.y - amountAway);
                break;
            case Directions.West:
                toSpawn.transform.position = new Vector2(pos.x - amountAway, pos.y);
                break;
            case Directions.East:
                toSpawn.transform.position = new Vector2(pos.x + amountAway, pos.y);
                break;
            default:
                break;
        }

        return toSpawn.transform.position;
    }

    Vector2 GetVelocity(GameObject obj)
    {
        return new Vector2(obj.GetComponent<Rigidbody2D>().linearVelocityX, obj.GetComponent<Rigidbody2D>().linearVelocityY);
    }

    Vector2 SetVelocity(GameObject obj, Directions exitDirection)
    {
        Vector2 currentVelocity = GetVelocity(obj);
        currentVelocity.x = Mathf.Abs(currentVelocity.x);
        currentVelocity.y = Mathf.Abs(currentVelocity.y);

        switch (exitDirection)
        {
            case Directions.North:
                currentVelocity.y += currentVelocity.x;
                currentVelocity.x *= 0;
                break;
            case Directions.South:
                currentVelocity.y += currentVelocity.x;
                currentVelocity.x *= 0;
                currentVelocity.y *= -1;
                break;
            case Directions.West:
                currentVelocity.x += currentVelocity.y;
                currentVelocity.y *= 0;
                currentVelocity.x *= -1;
                break;
            case Directions.East:
                currentVelocity.x += currentVelocity.y;
                currentVelocity.y *= 0;
                break;
            default:
                break;
        }

        return currentVelocity / divideAmountForce;
    }
}


//switch (exitDirection)
//{
//    case Directions.North:
//        if (currentVelocity.y < 0) { currentVelocity.y *= -1; }
//        currentVelocity.y += Mathf.Abs(currentVelocity.x);
//        if (currentVelocity.y < 1) { currentVelocity.y = 1; }
//        break;
//    case Directions.South:
//        if (currentVelocity.y > 0) { currentVelocity.y *= -1; }
//        currentVelocity.x *= 0;
//        if (currentVelocity.y > -1) { currentVelocity.y = -1; }
//        break;
//    case Directions.West:
//        if (currentVelocity.x > 0) { currentVelocity.x *= -1; }
//        currentVelocity.y *= 0;
//        break;
//    case Directions.East:
//        if (currentVelocity.x < 0) { currentVelocity.x *= -1; }
//        currentVelocity.y *= 0;
//        break;
//    default:
//        break;
//}
