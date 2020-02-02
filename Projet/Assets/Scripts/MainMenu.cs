using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainMenuButtons;
    public GameObject mainMenuTitle;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuButtons.SetActive(false);
        mainMenuTitle.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuButtons.SetActive(true);
        mainMenuTitle.SetActive(true);
    }
    // Update is called once per frame
    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
