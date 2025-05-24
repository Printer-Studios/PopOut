using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public ButtonHandler button;
    public GameObject door;
    // Update is called once per frame
    void Update()
    {
        opnenClose();
    }

    void opnenClose()
    {
        door.SetActive(!button.buttonIsPressed);
    }
}
