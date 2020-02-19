using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public GameObject startGameButton1;
    public GameObject startGameButton2;
    public GameObject settingsButton;
    public GameObject quitButton;
    public GameObject returnButton;
    public List<GameObject> buttonsSelectionList;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    public static int selectedNumber = 0;
    public static string currentMenu = "mainMenu";
   private Transform ScoresPanel;
    // Start is called before the first frame update
    public void Start(){
        buttonsSelectionList.Add(startGameButton1);
        buttonsSelectionList.Add(startGameButton2);
        buttonsSelectionList.Add(settingsButton);
        buttonsSelectionList.Add(quitButton);
        buttonsSelectionList.Add(scoresButton);
       EventSystem.current.SetSelectedGameObject(buttonsSelectionList[selectedNumber]);
    }

    public void Update(){
        if(currentMenu == "mainMenu"){
            int oldNumber = selectedNumber;
            if(Input.GetKeyDown ("s")){
            selectedNumber++; 
            }
            if(Input.GetKeyDown ("z")){
            selectedNumber--; 
            }


            if(selectedNumber > 4){
                selectedNumber = 0;
            }
            if(selectedNumber < 0){
                selectedNumber = 4;
            }
            Debug.Log(EventSystem.current.currentSelectedGameObject);
            if (oldNumber != selectedNumber || !this.buttonsSelectionList.Contains(EventSystem.current.currentSelectedGameObject)){
            EventSystem.current.SetSelectedGameObject(this.buttonsSelectionList[selectedNumber]);
            }
        }
        else if (currentMenu == "settings"){
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
        else if (currentMenu == "scores"){
            EventSystem.current.SetSelectedGameObject(menuButton);
        }
    }

    public void StartGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuButtons.SetActive(false);
        mainMenuTitle.SetActive(false);
        scoresButton.SetActive(false);
        menuButton.SetActive(false);
        currentMenu = "settings";
    }

    public void CloseScores(){
        mainMenuButtons.SetActive(true);
        mainMenuTitle.SetActive(true);
        scoresButton.SetActive(true);
        scores.SetActive(false);
        currentMenu = "mainMenu";
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuButtons.SetActive(true);
        mainMenuTitle.SetActive(true);
        scoresButton.SetActive(true);
        menuButton.SetActive(false);
        currentMenu = "mainMenu";
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
        currentMenu = "scores";

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

    