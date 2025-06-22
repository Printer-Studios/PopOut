using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject sistemEvent;
    [SerializeField] private PoiSO[] poiSOs;

    public void EndGame()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
        sistemEvent.SetActive(!sistemEvent.activeSelf);
    }

    public void ResetPlayerPrefs()
    {
        for (int i = 1; i < poiSOs.Length; i++) //This is to avoid having random levels already unlocked.
        {
            poiSOs[i].isUnlocked = false;
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
