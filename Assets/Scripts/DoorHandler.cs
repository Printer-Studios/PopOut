using System.Runtime.InteropServices;
using UnityEngine;
 
public class DoorHandler : MonoBehaviour
{
    public ButtonHandler button;
    public GameObject door;
    public bool reverse = false;
    // Update is called once per frame
    void Update()
    {
        openClose(reverse);
    }

    void openClose(bool reverse)
    {
        if(!reverse) door.SetActive(!button.buttonIsPressed);
        else door.SetActive(button.buttonIsPressed);
    }
}
