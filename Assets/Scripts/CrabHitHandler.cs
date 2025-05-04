using System.Collections;
using UnityEngine;

public class CrabHitHandler : MonoBehaviour
{
    public Collider2D weakspot;
    public bool canBeHit = false;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 4 && col.attachedRigidbody.linearVelocity.magnitude > 5)
        {
            CrabHit();
        }

        if (col.gameObject.tag == "Player" && canBeHit == true && weakspot.IsTouching(col))
        {
            Die();
        }
    }

    void CrabHit()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        canBeHit = true;
        StartCoroutine(GetUp());
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(5f);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        canBeHit = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
