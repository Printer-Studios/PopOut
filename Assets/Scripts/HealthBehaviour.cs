using System;
using System.Threading;
using UnityEngine;
using System.Collections;


public class HealthBehaviour : MonoBehaviour
{
    public int maxHealth, currentHealth, secondsIFrames;
    public bool isInvincible;


    public void Start()
    {
        currentHealth = maxHealth;
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
        yield return new WaitForSeconds(secondsIFrames);
        isInvincible = false;
    }
}
