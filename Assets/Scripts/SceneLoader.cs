using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
   public GameObject loadingScreen;
   public Image loadingBar;
   public TextMeshProUGUI percentage;

   public void Loading(int sceneIndex)
   {
      StartCoroutine(LoadAsynchronously(sceneIndex));
      loadingScreen.SetActive(true);
   }

   IEnumerator LoadAsynchronously (int sceneIndex)
   {
       AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

       

       while (!operation.isDone)
       {
          float progress = Mathf.Clamp01(operation.progress/.9f);
          loadingBar.fillAmount = progress;
          percentage.text = (Mathf.RoundToInt(progress*100)).ToString() + "%";
          yield return null;
       }
   }

}
