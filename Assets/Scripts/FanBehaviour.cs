using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    public enum States { On, Off }
    public States currentState;
    public float xForce;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (currentState == States.On)
        {
            if (col.gameObject.tag == "Water" || col.gameObject.tag == "Ice" || col.gameObject.tag == "Steam" || col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, 0), ForceMode2D.Impulse);
                if (col.gameObject.tag =="Player")
                {
                    Debug.LogWarning("Pushing player");
                }
            }
        }
    }
}
