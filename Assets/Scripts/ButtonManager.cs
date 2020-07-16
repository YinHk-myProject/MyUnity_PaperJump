using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{

    public GameObject  stopButton;
    public GameObject resumeButton;
    public GameObject menu;
    public GameObject canvas2;
    public GameObject joystick,shootingButton;
    public bool gamePause;
    public AudioSource clip;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.SetActive(false); 
        stopButton.SetActive(true);
        menu.SetActive(false); 
        canvas2.SetActive(false);
        gamePause = false;
    }

   public void  PauseGame()
   {
        Time.timeScale = 0f;
        resumeButton.SetActive(true);
        stopButton.SetActive(false);
        menu.SetActive(true); 
        canvas2.SetActive(true);
        joystick.SetActive(false);
        shootingButton.SetActive(false);
        gamePause = true;
       
   }

   public void ResumeGame()
   {
         Time.timeScale = 1f;
         resumeButton.SetActive(false);
         stopButton.SetActive(true);
         menu.SetActive(false); 
         canvas2.SetActive(false);
         joystick.SetActive(true);
         shootingButton.SetActive(true);
         gamePause = false;
        
   }

   public void PlayAudio()
   {
       clip.Play();
   }
}
