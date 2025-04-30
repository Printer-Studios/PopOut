using System;
using System.Threading;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class HealthBehaviour : MonoBehaviour
{
    public int maxHealth, currentHealth, secondsIFrames;
    public bool isInvincible;
    public TMP_Text healthText;



    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = ("Hearts: " + currentHealth.ToString());
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            GetHit();
        }
    }

    public void GetHit()
    {
        if (!isInvincible)
        {
            if (currentHealth <= 0)
            {
                //GameOver
                Debug.Log("Game Over");
            }
            else
            {
                currentHealth--;
                StartCoroutine(IFrames());
            }
        }
    }

    public IEnumerator IFrames()
    {
        isInvincible = true;
        //Thread.Sleep(secondsIFrames * 1000);
        Flicker();
        yield return new WaitForSeconds(secondsIFrames);
        isInvincible = false;
    }

    public void Flicker()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, Mathf.PingPong(0.4f, 1));
    }
}
