using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public bool buttonIsPressed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "floor")
        //{
            buttonIsPressed = true;
            Debug.Log(buttonIsPressed);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "floor")
        //{
            buttonIsPressed = false;
        //}
    }


}
