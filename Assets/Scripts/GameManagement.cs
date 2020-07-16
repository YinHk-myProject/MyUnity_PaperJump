using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManagement : MonoBehaviour
{
  
    public GameObject  stopButton;
    public GameObject resumeButton;
    public bool gameOver,gamePause;
    
    
    // Start is called before the first frame update
    void Start()
    {  
        gameOver = false;
        resumeButton.SetActive(false); 
        stopButton.SetActive(true);
        gamePause = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        
       if (gameOver)
       {
          Invoke("LoadScene", 1f);
        }
       
      



    }

    private void LoadScene()
    {
       
        SceneManager.LoadScene("GameOver");
    }
   
   public void  PauseGame()
   {
        Time.timeScale = 0f;
        resumeButton.SetActive(true);
        stopButton.SetActive(false);
        gamePause = true;
       
   }
   
   public void ResumeGame()
   {
         Time.timeScale = 1f;
         resumeButton.SetActive(false);
         stopButton.SetActive(true);
         gamePause = false;
        
   }
}
