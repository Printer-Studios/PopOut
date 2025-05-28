using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class OysterBehaviour : MonoBehaviour
{
    public enum State { Open, Preclosed, Closed }
    public State state;
    public float shakeWidth = 1, timeOfClosing, timeOfShake, timeOfOpening;
    public Vector3 originalPosition;
    public Collider2D killTrigger;
    public GameObject topCollider;
    public Sprite openSprite, closedSprite;

    public float closingCooldown, openingCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Open:
                GetComponent<SpriteRenderer>().sprite = openSprite;
                GetComponent<SpriteRenderer>().sortingOrder = -1;
                topCollider.SetActive(false);
                break;

            case State.Preclosed:
                originalPosition = transform.position;
                ShakeAndCloseTransition();
                break;

            case State.Closed:
                GetComponent<SpriteRenderer>().sprite = closedSprite;
                GetComponent<SpriteRenderer>().sortingOrder = 10;
                Open();
                topCollider.SetActive(true);
                break;
        }
    }

    public void ShakeAndCloseTransition()
    {
        if (timeOfClosing == 0)
        {
            timeOfClosing = Time.time;
        }
        if (timeOfShake == 0)
        {
            timeOfShake = Time.time;
        }

        if (Time.time - timeOfClosing > closingCooldown)
        {
            state = State.Closed;
            transform.position = originalPosition;
            timeOfClosing = 0;
        }

        if (Time.time - timeOfShake > 0.02)
        {
            transform.position = new Vector3(transform.position.x + shakeWidth, transform.position.y, transform.position.z);
            shakeWidth *= -1;
            timeOfShake = 0;
        }

    }

    void Open()
    {

        if (timeOfOpening == 0)
        {
            timeOfOpening = Time.time;
        }
        if (timeOfShake == 0)
        {
            timeOfShake = Time.time;
        }

        if (Time.time - timeOfShake > 0.02 && Time.time - timeOfOpening > openingCooldown / 2)
        {
            transform.position = new Vector3(transform.position.x + shakeWidth, transform.position.y, transform.position.z);
            shakeWidth *= -1;
            timeOfShake = 0;
        }
        if (Time.time - timeOfOpening > openingCooldown)
        {
            state = State.Open;
            timeOfOpening = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && state == State.Open)
        {
            state = State.Preclosed;
        }

        if (collision.gameObject.tag == "Player" && state == State.Closed && collision.IsTouching(killTrigger))
        {
            collision.gameObject.GetComponent<HealthBehaviour>().GetHit(collision.gameObject.GetComponent<HealthBehaviour>().currentHealth);
        }
    }
}
