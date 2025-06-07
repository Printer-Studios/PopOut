using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logoFadeScript : MonoBehaviour
{
    float a,s;
    public Image logo;
    // Update is called once per frame
    private void Start()
    {
        a = 0;
        s = 7;
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, a);
        
    }
    void Update()
    {
        a += Time.deltaTime / 4;
        if (a < 1)
        {
            s += Time.deltaTime / 3;
        }
        logo.transform.localScale = new Vector3(s,s,s);
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, a);
        if(a > 1.5)
        {
            Debug.Log("menu");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
