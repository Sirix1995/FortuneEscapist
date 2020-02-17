using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainMenuButtons;
    public GameObject mainMenuTitle;
    public GameObject scores;
    private Transform entryContainer;
    private Transform entryTemplate;

   private Transform ScoresPanel;
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

    public void Scores(){
        mainMenuButtons.SetActive(false);
        scores.SetActive(true);
        mainMenuTitle.SetActive(false);


        entryContainer = scores.transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 80f;
        for (int i = 0; i < 10; i++){
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            int score = Random.Range(0, 10000);

            entryTransform.Find("posText").GetComponent<TextAlignment>().text = rank.ToString();
            entryTransform.Find("nameText").GetComponent<TextAlignment>().text = score.ToString();
            entryTransform.Find("posText").GetComponent<TextAlignment>().text = "AAA";
        }


    }
}
