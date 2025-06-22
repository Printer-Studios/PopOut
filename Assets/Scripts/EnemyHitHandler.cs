using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class EnemyHitHandler : MonoBehaviour
{
    public enum Hit { Nothing, Jump, Box}
    public Hit[] hitTypes;
    public EnemyShotHandler shotHandler;
    public GameObject boxPF;
    public float deathTimer;
    public Sprite deathSprite;
    public bool respawns;
    public EnemyRespawn enemyRespawn;


    public Collider2D weakspot;

    public void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < hitTypes.Length; i++)
        {
            if (hitTypes[i] == Hit.Box && col.gameObject.name == "Box" && col.gameObject.GetComponent<Rigidbody2D>().linearVelocityY < 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (!respawns)
        {
            Destroy(gameObject);
        }
        else
        {
            enemyRespawn.Deactivate();
        }
    }

    public IEnumerator TimerDeath()
    {
        Debug.Log("muele");
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        yield return new WaitForSeconds(deathTimer);
        Die();
    }

}
