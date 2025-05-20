using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool buttonIsPressed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonIsPressed = true;
        Debug.Log(buttonIsPressed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonIsPressed = false;
        Debug.Log(buttonIsPressed);
    }


}
