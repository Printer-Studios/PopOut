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
    public float alpha;
    const float maxAlpha = 1;
    public GameObject gameOverPanel;


    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = ("Hearts: " + currentHealth.ToString());

        if (isInvincible)
        {
            Flicker();
            alpha = Mathf.Abs(Mathf.Sin(Time.time * 10f));
        }
        else
        {
            ResetColor();
            alpha = maxAlpha;
        }   
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
            Debug.Log("as sido jiteao");
            currentHealth--;
            StartCoroutine(IFrames());
            if (currentHealth <= 0)
            {
                //GameOver
                Debug.Log("Game Over");
                Time.timeScale = 0;//pause game
                PanelOpener();
            }
        }
    }

    public IEnumerator IFrames()
    {
        Debug.Log("comienza la corrutina");
        isInvincible = true;
        //Thread.Sleep(secondsIFrames * 1000);
        yield return new WaitForSeconds(secondsIFrames);
        isInvincible = false;
        Debug.Log("acabe la corrutina");
    }

    public void Flicker()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, alpha);
    }

    public void ResetColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, maxAlpha);
    }

    private void PanelOpener()
    {
        Debug.Log("panel");
        gameOverPanel.SetActive(true);
    }
}
