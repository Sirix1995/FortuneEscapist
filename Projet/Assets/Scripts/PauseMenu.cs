using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public List<GameObject> buttonsSelectionList;
    public GameObject resumeButton;
    public GameObject menuButton;
    public GameObject quitButton;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUi;
    public static int selectedNumber = 0;
    // Update is called once per frame
    void Start(){
        buttonsSelectionList.Add(resumeButton);
        buttonsSelectionList.Add(menuButton);
        buttonsSelectionList.Add(quitButton);
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume(); 
            }else
            {
                Pause();
            }
        }
        else if(GameIsPaused){
            int oldNumber = selectedNumber;
            if(Input.GetKeyDown ("s")){
            selectedNumber++; 
            }
            if(Input.GetKeyDown ("z")){
            selectedNumber--; 
            }


            if(selectedNumber > 2){
                selectedNumber = 0;
            }
            if(selectedNumber < 0){
                selectedNumber = 2;
            }
            Debug.Log(EventSystem.current.currentSelectedGameObject);
            if (oldNumber != selectedNumber || !this.buttonsSelectionList.Contains(EventSystem.current.currentSelectedGameObject)){
            EventSystem.current.SetSelectedGameObject(this.buttonsSelectionList[selectedNumber]);
            }
        }
    }

    public void Resume(){
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Pause(){
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Debug.Log("Quitting Game...");
    }
}
