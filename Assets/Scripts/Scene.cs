using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
   
   public void LoadScene()
   {
       SceneManager.LoadScene("Main_Scene");
   }

   public void GameOverScene()
   {
        SceneManager.LoadScene("GameOver");
   }

   public void MenuScene()
   {
        SceneManager.LoadScene("Menu");
   }

}
