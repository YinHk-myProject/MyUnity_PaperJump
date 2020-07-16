using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AudioSource bgm;
   


    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(BGM());  
    }
    
    
    public void StopBGM()
    {
      bgm.Stop();    
    }
       

    private void PlayAudio()
    {
      bgm.Play();
    }

    IEnumerator BGM()
    {
      yield return new WaitForSeconds(0.5f);
      PlayAudio();
    }
}
