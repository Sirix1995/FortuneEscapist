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
    public GameObject scoresButton;
    public GameObject menuButton;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

   private Transform ScoresPanel;
    // Start is called before the first frame update
    public void Start(){
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuButtons.SetActive(false);
        mainMenuTitle.SetActive(false);
        scoresButton.SetActive(false);
        menuButton.SetActive(false);
    }

    public void CloseScores(){
        mainMenuButtons.SetActive(true);
        mainMenuTitle.SetActive(true);
        scoresButton.SetActive(true);
        scores.SetActive(false);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuButtons.SetActive(true);
        mainMenuTitle.SetActive(true);
        scoresButton.SetActive(true);
        menuButton.SetActive(false);
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
        scoresButton.SetActive(false);
        menuButton.SetActive(true);
        

        entryContainer = scores.transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        //AddHighscoreEntry(10000000, "mok");
        for(int i = 0; i < highscores.highscoreEntryList.Count; i++){
            for(int j = i + 1; j < highscores.highscoreEntryList.Count; j++){
                if(highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score){
                    
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList){
            Debug.Log(highscoreEntry.score);
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList){
        
        float templateHeight = 80f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            int score = highscoreEntry.score;
            string name = highscoreEntry.name;
            entryTransform.Find("posText").GetComponent<UnityEngine.UI.Text>().text = rank.ToString();
            entryTransform.Find("scoreText").GetComponent<UnityEngine.UI.Text>().text = score.ToString();
            entryTransform.Find("nameText").GetComponent<UnityEngine.UI.Text>().text = name;

            transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name){
        HighscoreEntry highscoreEntry = new HighscoreEntry {score = score, name = name};
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);
        string jsonScore = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", jsonScore);
        PlayerPrefs.Save();
    }
    private class Highscores{
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry {
        public int score;
        public string name;
    }
}

    