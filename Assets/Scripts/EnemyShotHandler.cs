using UnityEngine;
using System.Collections;


public class EnemyShotHandler : MonoBehaviour
{
    public enum Shot { Upside, Die, Nothing}
    public Shot shotType;
    public bool isWeak;


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 4 && col.attachedRigidbody.linearVelocity.magnitude > 5) //If enemy is hit with water
        {
            if (shotType == Shot.Upside)
            {
                UpsideDown();
            }
            else if (shotType == Shot.Die)
            {
                Die();
            }
            Destroy(col.gameObject); //Destroy water
        }
    }

    void UpsideDown()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        isWeak = true;
        StartCoroutine(GetUp());
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(5f);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        isWeak = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
